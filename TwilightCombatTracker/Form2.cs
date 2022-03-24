using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TwilightCombatTracker
{
    public partial class frmAddUnit : Form
    {
        private const string NO_UNIT = "None";

        bool bluFor;
        frmCombatTracker parent;
        Unit currentUnit;
        List<Specialization> currentSpecs = new List<Specialization>();

        public frmAddUnit(Unit unit, bool blufor, frmCombatTracker parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.bluFor = blufor;
            this.Text = $"Edit {unit.Name}";
            
            // SetUpUI relies on currentUnit being set
            currentUnit = unit;

            SetUpUI();
            PopulateEntryFields();
        }

        public frmAddUnit(bool bluFor, frmCombatTracker parent)
        {
            InitializeComponent();

            this.bluFor = bluFor;
            this.parent = parent;
            Text = bluFor ? "Add Blufor Unit" : "Add Opfor Unit";

            SetUpUI();
        }

        private void SetUpUI()
        {
            List<Tag> tags = new List<Tag>();

            foreach (Tag tag in Enum.GetValues(typeof(Tag)))
            {
                tags.Add(tag);
            }

            tags.Sort(TagProperties.CompareTag);
            lstUnitTags.Items.Clear();
            lstUnitTags.DataSource = tags;
            lstUnitTags.SelectedItems.Clear();

            lstSpecTags.Items.Clear();
            lstSpecTags.DataSource = new List<Tag>(tags);
            lstSpecTags.SelectedItems.Clear();

            List<Equipment> equipment = new List<Equipment>();
            foreach (Equipment eq in Equipment.EquipmentDatabase)
            {
                equipment.Add(eq);
            }

            equipment.Sort();
            lstUnitEquipment.Items.Clear();
            lstUnitEquipment.DataSource = equipment;
            lstUnitEquipment.SelectedItems.Clear();

            btnSaveUnit.Enabled = currentUnit != null;
            btnAddUnit.Click += ClickHandler;
            btnSaveUnit.Click += ClickHandler;
            btnNext.Click += DiffUnitHandler;
            btnPrev.Click += DiffUnitHandler;
            btnNext.Enabled = currentUnit != null;
            btnPrev.Enabled = currentUnit != null;
            btnAddSpec.Enabled = false;
            btnAddSpec.Click += btnAddSpecHandler;
            btnRemoveSpec.Enabled = false;
            btnRemoveSpec.Click += btnRemoveSpecHandler;

            lstSpecTags.Click += lstSpecTagClickHandler;
            lstSpecs.Click += lstSpecsClickHandler;
        }

        public void PopulateEntryFields()
        {
            txtUnitName.Text = currentUnit.Name ?? "";
            numSpeed.Value = currentUnit?.Speed ?? 1;
            numHealth.Value = currentUnit?.Health ?? 100;
            txtInitXP.Text = currentUnit?.DrivingXP.ToString() ?? "";
            txtCombatXP.Text = currentUnit?.GunneryXP.ToString() ?? "";

            Unit none = new Unit();
            none.Name = NO_UNIT;

            cboBunker.Items.Clear();
            cboBunker.Items.Add(none);

            foreach (UnitEquipmentTuple unit in parent.combatManager.GetUnitsForCombatDisplay(bluFor))
            {
                if (!cboBunker.Items.Contains(unit.Unit) && unit.Unit.Tags.Contains(TwilightCombatTracker.Tag.Bunker))
                {
                    cboBunker.Items.Add(unit.Unit);
                }
            }

            cboBunker.SelectedItem = currentUnit?.Bunker;
            lstUnitTags.SelectedItems.Clear();
            lstUnitEquipment.SelectedItems.Clear();

            foreach (Tag tag in currentUnit.Tags)
            {
                lstUnitTags.SelectedItems.Add(tag);
            }

            foreach (Equipment equipment in currentUnit.Weapons)
            {
                lstUnitEquipment.SelectedItems.Add(equipment);
            }

            currentSpecs = currentUnit.Specializations;
            foreach (Specialization spec in currentUnit.Specializations)
            {
                lstSpecs.Items.Add(spec);
            }
        }

        public void ClickHandler(object sender, EventArgs e)
        {
            bool newUnit = currentUnit == null || sender == btnAddUnit;
            Unit u = newUnit ? new Unit() : currentUnit;
            u.Name = txtUnitName.Text;
            u.Speed = (int) numSpeed.Value;
            u.Health = (int)numHealth.Value;
            u.Weapons.Clear();
            u.Tags.Clear();

            foreach (Tag tag in lstUnitTags.SelectedItems) {
                u.Tags.Add(tag);
            }

            foreach (Equipment equipment in lstUnitEquipment.SelectedItems)
            {
                u.Weapons.Add(equipment);
            }

            if (cboBunker.SelectedItem == null || ((Unit) cboBunker.SelectedItem).Name == NO_UNIT)
            {
                u.Bunker = null;
            }
            else
            {
                u.Bunker = (Unit) cboBunker.SelectedItem;
            }

            if (currentUnit == null)
            {
                parent.AddUnit(u, bluFor);
            }
            else
            {
                parent.PopulateOpforList();
                parent.PopulateBluForList();
            }

            int newCombatXP;
            if (Int32.TryParse(txtCombatXP.Text, out newCombatXP))
            {
                u.GunneryXP = newCombatXP;
            }

            int newInitXP;
            if (Int32.TryParse(txtInitXP.Text, out newInitXP))
            {
                u.DrivingXP = newInitXP;
            }

            u.Specializations = currentSpecs;

            if (!newUnit)
            {
                Close();
            }
        }

        public void DiffUnitHandler(object sender, EventArgs e)
        {
            currentUnit = parent.GetUnit(bluFor, sender == btnNext);
            if (currentUnit != null)
            {
                PopulateEntryFields();
            }
        }

        public void btnAddSpecHandler(object sender, EventArgs e)
        {
            List<Tag> tags = new List<Tag>();

            foreach (Tag tag in lstSpecTags.SelectedItems)
            {
                tags.Add(tag);
            }

            Specialization spec = new Specialization(tags, (int)spnSpecMod.Value);

            currentSpecs.Add(spec);
            lstSpecs.Items.Add(spec);
            lstSpecTags.SelectedItems.Clear();
            btnAddSpec.Enabled = false;
        }

        public void btnRemoveSpecHandler(object sender, EventArgs e)
        {
            currentSpecs.Remove((Specialization) lstSpecs.SelectedItem);
            lstSpecs.Items.RemoveAt(lstSpecs.SelectedIndex);
            lstSpecs.SelectedItems.Clear();
            btnRemoveSpec.Enabled = false;
        }

        public void lstSpecTagClickHandler(object sender, EventArgs e)
        {
            btnAddSpec.Enabled = lstSpecTags.SelectedItems.Count > 0;
        }

        public void lstSpecsClickHandler(object sender, EventArgs e)
        {
            btnRemoveSpec.Enabled = lstSpecs.SelectedItems.Count > 0;
        }
    }
}
