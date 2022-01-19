using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwilightCombatTracker
{
    public partial class frmAddUnit : Form
    {
        private const string NO_UNIT = "None";

        bool bluFor;
        frmCombatTracker parent;
        Unit currentUnit;

        public frmAddUnit(Unit unit, bool blufor, frmCombatTracker parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.bluFor = blufor;
            this.Text = $"Edit {unit.Name}";
            btnAddUnit.Text = "Save";
            
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
            this.Text = bluFor ? "Add Blufor Unit" : "Add Opfor Unit";

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

            List<Equipment> equipment = new List<Equipment>();
            foreach (Equipment eq in Equipment.EquipmentDatabase)
            {
                equipment.Add(eq);
            }

            equipment.Sort();
            lstUnitEquipment.Items.Clear();
            lstUnitEquipment.DataSource = equipment;
            lstUnitEquipment.SelectedItems.Clear();


            btnAddUnit.Click += ClickHandler;
            btnNext.Click += DiffUnitHandler;
            btnPrev.Click += DiffUnitHandler;
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
        }

        public void ClickHandler(object sender, EventArgs e)
        {
            Unit u = currentUnit == null ? new Unit() : currentUnit;
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

            //Close();
        }

        public void DiffUnitHandler(object sender, EventArgs e)
        {
            currentUnit = parent.GetUnit(bluFor, sender == btnNext);

            PopulateEntryFields();
        }
    }
}
