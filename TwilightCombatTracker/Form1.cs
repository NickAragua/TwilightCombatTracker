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
            btnClearLog.Click += ClearLog;
            btnDeleteBlue.Click += DeleteUnitHandler;
            btnDeleteRed.Click += DeleteUnitHandler;
            btnDefectBlue.Click += DefectHandler;
            btnDefectRed.Click += DefectHandler;

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

            SetIndividualUnitButtonState(lstBlueFor.SelectedItem != null, true);
            SetIndividualUnitButtonState(lstOpfor.SelectedItem != null, false);

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

            if (selectedBox.SelectedItem == null)
            {
                return;
            }
            
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
                btnFlipEngagement.Enabled = !selectedEngagement.resolved;
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
                bool bluFor = sender == btnLoadBlueUnits;

                using (Stream fileStream = openFileDialog.OpenFile())
                {
                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        combatManager.DeserializeUnits(bluFor, sr.ReadToEnd(), chkClearOnLoad.Checked);
                    }
                }

                PopulateBluForList();
                PopulateOpforList();

                SetIndividualUnitButtonState(false, bluFor);
            }
        }

        private void GlobalFlagHandler(object sender, EventArgs e)
        {
            frmGlobalFlags globalFlagForm = new frmGlobalFlags(this);
            globalFlagForm.ShowDialog();
        }

        private void DeleteUnitHandler(object sender, EventArgs e)
        {
            bool bluFor = sender == btnDeleteBlue;
            Unit unit = bluFor ? ((UnitEquipmentTuple) lstBlueFor.SelectedItem).Unit : ((UnitEquipmentTuple)lstOpfor.SelectedItem).Unit;
            combatManager.DeleteUnit(unit, bluFor);
            
            if (bluFor)
            {
                PopulateBluForList();
            } 
            else
            {
                PopulateOpforList();
            }

            SetIndividualUnitButtonState(false, bluFor);
        }

        private void DefectHandler(object sender, EventArgs e)
        {
            bool bluFor = sender == btnDefectBlue;
            Unit unit = bluFor ? ((UnitEquipmentTuple)lstBlueFor.SelectedItem).Unit : ((UnitEquipmentTuple)lstOpfor.SelectedItem).Unit;
            combatManager.DeleteUnit(unit, bluFor);
            combatManager.AddUnit(unit, !bluFor);

            PopulateBluForList();
            PopulateOpforList();

            SetIndividualUnitButtonState(false, bluFor);
        }

        private void SetIndividualUnitButtonState(bool enabled, bool bluFor)
        {
            if (bluFor)
            {
                btnDeleteBlue.Enabled = enabled;
                btnEditBluforUnit.Enabled = enabled;
                btnDefectBlue.Enabled = enabled;
            }
            else
            {
                btnDeleteRed.Enabled = enabled;
                btnEditOpforUnit.Enabled = enabled;
                btnDefectRed.Enabled = enabled;
            }
        }

        private void EndRoundHandler(Object sender, EventArgs e)
        {
            combatManager.EndOfRound(chkAutoWithdraw.Checked);
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

        public Unit GetUnit(bool blufor, bool next)
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

            return ((UnitEquipmentTuple) list?.SelectedItem)?.Unit;
        }

        private void ClearLog(Object sender, EventArgs e)
        {
            txtAccumulator.Clear();
        }
    }
}
