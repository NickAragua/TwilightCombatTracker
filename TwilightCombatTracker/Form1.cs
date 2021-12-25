using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TwilightCombatTracker
{
    public partial class frmCombatTracker : Form
    {
        public CombatManager combatManager = new CombatManager();
        private Engagement selectedEngagement;

        public frmCombatTracker()
        {
            InitializeComponent();

            btnAddBluforUnit.Click += AddUnitClick;
            btnAddOpfor.Click += AddUnitClick;
            lstBlueFor.SelectedIndexChanged += UnitSelectionHandler;
            lstOpfor.SelectedIndexChanged += UnitSelectionHandler;
            btnRollInit.Click += RollInitHandler;
            btnSaveBlueUnits.Click += SaveUnitsHandler;
            btnSaveOpUnits.Click += SaveUnitsHandler;
            btnLoadBlueUnits.Click += LoadUnitsHandler;
            btnLoadOpUnits.Click += LoadUnitsHandler;
            btnCommitEngagement.Click += CommitEngagementHandler;
            lstEngagements.SelectedIndexChanged += SelectEngagementHandler;
            btnRunEngagement.Click += RunEngagementHandler;
            btnEndRound.Click += EndRoundHandler;
            btnManageGlobalModifiers.Click += GlobalFlagHandler;
            btnEditBluforUnit.Click += EditUnitHandler;
            btnEditOpforUnit.Click += EditUnitHandler;
            btnFlipEngagement.Click += FlipEngagementHandler;

            PopulateBluForList();
            PopulateOpforList();
        }

        public void AddUnit(Unit u, bool bluFor)
        {
            combatManager.AddUnit(u, bluFor);

            if (bluFor)
            {
                PopulateBluForList();
            }
            else
            {
                PopulateOpforList();
            }
        }

        public void PopulateBluForList()
        {
            lstBlueFor.Items.Clear();
            Graphics graphics = lstBlueFor.CreateGraphics();
            float maxWidth = 0;

            foreach (UnitEquipmentTuple u in combatManager.GetUnitsForCombatDisplay(true))
            {
                lstBlueFor.Items.Add(u);
                SizeF fontmeasure = graphics.MeasureString(u.ToString(), lstBlueFor.Font);        
                if (fontmeasure.Width > maxWidth)
                {
                    maxWidth = fontmeasure.Width;
                }
            }

            if (maxWidth > 0)
            {
               // lstBlueFor.Width = (int)maxWidth + 1;   
            }
        }

        public void PopulateOpforList()
        {
            lstOpfor.Items.Clear();

            foreach (UnitEquipmentTuple u in combatManager.GetUnitsForCombatDisplay(false))
            {
                lstOpfor.Items.Add(u);
            }
        }

        private void PopulateEngagementList()
        {
            lstEngagements.Items.Clear();

            foreach (Engagement e in combatManager.currentEngagements)
            {
                lstEngagements.Items.Add(e);
            }

            selectedEngagement = null;
        }

        private void UnitSelectionHandler(Object sender, EventArgs e)
        {
            if (lstBlueFor.SelectedItem != null && lstOpfor.SelectedItem != null)
            {
                selectedEngagement = combatManager.CreateEngagement((UnitEquipmentTuple)lstBlueFor.SelectedItem, (UnitEquipmentTuple)lstOpfor.SelectedItem);
                
                if (combatManager.currentEngagements.Contains(selectedEngagement))
                {
                    txtCombatPreview.Text = "This engagement has already been commited";
                    btnCommitEngagement.Enabled = false;
                }
                else if (selectedEngagement != null)
                {
                    txtCombatPreview.Text = selectedEngagement.DetailedBreakdown();
                    btnCommitEngagement.Enabled = true;
                }
                else
                {
                    txtCombatPreview.Text = "";
                    btnCommitEngagement.Enabled = false;
                }

                lstEngagements.ClearSelected();
            } 
            else
            {
                btnCommitEngagement.Enabled = false;
            }

            btnEditBluforUnit.Enabled = lstBlueFor.SelectedItem != null;
            btnEditOpforUnit.Enabled = lstOpfor.SelectedItem != null;
            btnFlipEngagement.Enabled = btnCommitEngagement.Enabled;
        }

        private void AddUnitClick(Object sender, EventArgs e)
        {
            frmAddUnit newForm = new frmAddUnit(sender == btnAddBluforUnit, this);
            newForm.ShowDialog();
        }

        private void EditUnitHandler(Object sender, EventArgs e)
        {
            bool blufor = sender == btnEditBluforUnit;
            ListBox selectedBox = blufor ? lstBlueFor : lstOpfor;
            
            Unit unit = ((UnitEquipmentTuple)selectedBox.SelectedItem).Unit;
            frmAddUnit newForm = new frmAddUnit(unit, blufor, this);
            newForm.ShowDialog();
        }

        private void RollInitHandler(Object sender, EventArgs e)
        {
            String accumulator = combatManager.RollInit();
            PopulateBluForList();
            PopulateOpforList();
            txtAccumulator.Text += accumulator;

            // trick to scroll to the bottom
            txtAccumulator.SelectionStart = txtAccumulator.Text.Length;
            txtAccumulator.ScrollToCaret();
        }

        private void SelectEngagementHandler(Object sender, EventArgs e)
        {
            btnRunEngagement.Enabled = false;

            if (lstEngagements.SelectedItem != null)
            {
                lstBlueFor.ClearSelected();
                lstOpfor.ClearSelected();
                selectedEngagement = (Engagement)lstEngagements.SelectedItem;
                btnRunEngagement.Enabled = selectedEngagement != null && !selectedEngagement.resolved && !selectedEngagement.IsSupportingEngagement;
                txtCombatPreview.Text = selectedEngagement.DetailedBreakdown();
                btnFlipEngagement.Enabled = !selectedEngagement.IsSupportingEngagement;
            }
        }

        private void CommitEngagementHandler(Object sender, EventArgs e)
        {
            combatManager.CommitEngagement(selectedEngagement);
            selectedEngagement = null;
            btnCommitEngagement.Enabled = false;
            PopulateEngagementList();
            PopulateBluForList();
            PopulateOpforList();
        }

        private void RunEngagementHandler(Object sender, EventArgs e)
        {
            btnRunEngagement.Enabled = false;
            txtAccumulator.AppendText(combatManager.RunEngagement(selectedEngagement));
            PopulateEngagementList();
            PopulateBluForList();
            PopulateOpforList();
        }

        private void SaveUnitsHandler(Object sender, EventArgs e)
        {
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream fileStream = saveFileDialog.OpenFile())
                {
                    using (StreamWriter sw = new StreamWriter(fileStream))
                    {
                        sw.Write(combatManager.SerializeUnits(sender == btnSaveBlueUnits));
                    }
                }
            }
        }

        private void LoadUnitsHandler(Object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream fileStream = openFileDialog.OpenFile())
                {
                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        combatManager.DeserializeUnits(sender == btnLoadBlueUnits, sr.ReadToEnd());
                    }
                }

                PopulateBluForList();
                PopulateOpforList();
            }
        }

        private void GlobalFlagHandler(object sender, EventArgs e)
        {
            frmGlobalFlags globalFlagForm = new frmGlobalFlags(this);
            globalFlagForm.ShowDialog();
        }

        private void EndRoundHandler(Object sender, EventArgs e)
        {
            combatManager.EndOfRound();
            PopulateBluForList();
            PopulateOpforList();
            PopulateEngagementList();
        }

        private void FlipEngagementHandler(object sender, EventArgs e)
        {
            selectedEngagement.Flip();
            txtCombatPreview.Text = selectedEngagement.DetailedBreakdown();
            var hold = selectedEngagement;
            PopulateEngagementList();
            selectedEngagement = hold;
        }

        public Unit getUnit(bool blufor, bool next)
        {
            ListBox list = blufor ? lstBlueFor : lstOpfor;
            
            if (list.SelectedIndex >= 0)
            {
                int nextIndex = next ? list.SelectedIndex + 1 : list.SelectedIndex - 1;
                if (nextIndex < 0)
                {
                    nextIndex = list.Items.Count - 1;
                } 
                else if (nextIndex >= list.Items.Count)
                {
                    nextIndex = 0;
                }

                list.SelectedIndex = nextIndex;
            }

            return ((UnitEquipmentTuple) list.SelectedItem).Unit;
        }
    }
}
