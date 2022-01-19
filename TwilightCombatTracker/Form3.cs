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
    public partial class frmGlobalFlags : Form
    {
        frmCombatTracker parent;

        public frmGlobalFlags(frmCombatTracker parent)
        {
            InitializeComponent();

            this.parent = parent;
            List<Tag> qualifyingTagsLeft = new List<Tag>();
            List<Tag> qualifyingTagsRight = new List<Tag>();

            foreach (Tag tag in Enum.GetValues(typeof(Tag))) 
            {
                var category = tag.GetAttributeOfType<CategoryAttribute>();
                if (category.Category.Equals(TagType.EnvironmentTag.ToString()))
                {
                    qualifyingTagsLeft.Add(tag);
                    qualifyingTagsRight.Add(tag);
                }
            }

            qualifyingTagsLeft.Sort();
            qualifyingTagsLeft.Sort();

            lstBluModifiers.DataSource = qualifyingTagsLeft;
            lstOpModifiers.DataSource = qualifyingTagsRight;
            lstBluModifiers.SelectedItems.Clear();
            lstOpModifiers.SelectedItems.Clear();

            foreach (Tag tag in parent.combatManager.globalBluTags) 
            {
                lstBluModifiers.SelectedItems.Add(tag);
            }

            foreach (Tag tag in parent.combatManager.globalRedTags)
            {
                lstOpModifiers.SelectedItems.Add(tag);
            }

            btnOk.Click += OkClickHandler;
        }

        public void OkClickHandler(object sender, EventArgs e)
        {
            parent.combatManager.globalBluTags.Clear();
            foreach (Tag tag in lstBluModifiers.SelectedItems)
            {
                parent.combatManager.globalBluTags.Add(tag);
            }

            parent.combatManager.globalRedTags.Clear();
            foreach (Tag tag in lstOpModifiers.SelectedItems)
            {
                parent.combatManager.globalRedTags.Add(tag);
            }

            parent.combatManager.ReapplyGlobalModifiers();
            
            Close();
        }
    }
}
