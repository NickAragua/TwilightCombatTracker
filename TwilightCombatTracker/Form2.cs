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

            txtUnitName.Text = unit.Name;
            numSpeed.Value = unit.Speed;
            numHealth.Value = unit.Health;

            foreach (Tag tag in unit.Tags)
            {
                lstUnitTags.SelectedItems.Add(tag);
            }

            foreach (Equipment equipment in unit.Weapons)
            {
                lstUnitEquipment.SelectedItems.Add(equipment);
            }
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
            foreach (Tag tag in Enum.GetValues(typeof(Tag)))
            {
                lstUnitTags.Items.Add(tag);
            }

            foreach (Equipment equipment in Equipment.EquipmentDatabase)
            {
                lstUnitEquipment.Items.Add(equipment);
            }

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

            btnAddUnit.Click += ClickHandler;
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

            if (cboBunker.SelectedItem == null || ((UnitEquipmentTuple) cboBunker.SelectedItem).Unit.Name == NO_UNIT)
            {
                u.Bunker = null;
            }
            else
            {
                u.Bunker = ((UnitEquipmentTuple) cboBunker.SelectedItem).Unit;
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

            Close();
        }
    }
}
