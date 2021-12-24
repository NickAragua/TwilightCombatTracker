using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightCombatTracker
{
    public class Unit
    {
        // A unit has a name;
        // A list of weapons
        // A list of tags (e.g. VEHICLE, FAST, AIR, FOOT)
        // desired functionality: add new unit
        // desired functionality: select unit on left side, select unit on right side, have combat odds display
        // desired functionality: roll dice, assign damage; display dice results in pasteable log on right
        // desired functionality: add global modifiers per side

        // equipment is a name and list of TAG -> Effect (e.g. VEHICLE = +5)

        public string Name { get; set; }
        
        public int Health { get; set; }

        public int Speed { get; set; }
        public HashSet<Equipment> Weapons { get; set; }

        /// <summary>
        /// These are tags that are intrinsic to the unit, such as "dug in", 
        /// "infantry", "vehicle", "stunned"
        /// </summary>
        public HashSet<Tag> Tags { get; set; }

        /// <summary>
        /// These are tags that represent external environment, such as 
        /// "rocky terrain", "local commander", "comms"
        /// </summary>
        public HashSet<Tag> ExternalTags { get; set; }

        public int DrivingXP { get; set; }

        public int GunneryXP { get; set; }

        public int? CurrentInit { get; set; }

        public Unit Bunker { get; set; }

        public Unit()
        {
            Tags = new HashSet<Tag>();
            Weapons = new HashSet<Equipment>();
            ExternalTags = new HashSet<Tag>();
        }

        public String getApplicableModifierString(Unit other, Equipment specificWeapon, int supportDivider)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(FixedCombatModString());

            if (specificWeapon != null)
            {
                sb.Append(specificWeapon.getModifierString(other));
            } 
            else
            {
                foreach (Equipment weapon in Weapons)
                {
                    sb.Append(weapon.getModifierString(other));
                }
            }

            if (supportDivider > 1 && !other.Tags.Contains(Tag.WeakRearArmor))
            {
                sb.Append($" / {supportDivider} (support)");
            }

            return sb.ToString();
        }

        public int getApplicableModifier(Unit other, Equipment specificWeapon)
        {
            int mod = 0;

            mod += FixedCombatMod();

            if (specificWeapon != null)
            {
                mod += specificWeapon.getModifier(other);
            }
            else
            {
                foreach (Equipment weapon in Weapons)
                {
                    mod += weapon.getModifier(other);
                }
            }

            return mod;
        }

        public string InitString()
        {
            return $"{Speed}d100{FixedInitModString()}";
        }

        private void FixedCommonModString(StringBuilder sb)
        {
            if (Tags.Contains(Tag.FootInfantry))
            {
                if (ExternalTags.Contains(Tag.UrbanTerrain))
                {
                    sb.Append(" + 10 (infantry in urban terrain)");
                }
                else if (ExternalTags.Contains(Tag.RockyTerrain))
                {
                    sb.Append(" + 5 (infantry in rocky terrain)");
                }
            }

            if (ExternalTags.Contains(Tag.Commander))
            {
                sb.Append(" +5 (commander)");
            }

            if (ExternalTags.Contains(Tag.CommsBonus))
            {
                sb.Append(" + 5 (comms)");
            }

            if (ExternalTags.Contains(Tag.RemoteCommander))
            {
                sb.Append(" -5 (remote commander)");
            }

            if (ExternalTags.Contains(Tag.LocalCommander))
            {
                sb.Append(" +10 (local commander)");
            }

            if (Tags.Contains(Tag.DugIn))
            {
                sb.Append(" +10 (dug in)");
            }

            if (Tags.Contains(Tag.TunedUp))
            {
                sb.Append(" +5 (tuned up)");
            }
        }

        private int FixedCommonMod()
        {
            int mod = 0;

            if (Tags.Contains(Tag.FootInfantry))
            {
                if (ExternalTags.Contains(Tag.UrbanTerrain))
                {
                    mod += 10;
                }
                else if (ExternalTags.Contains(Tag.RockyTerrain))
                {
                    mod += 5;
                }
            }

            if (ExternalTags.Contains(Tag.Commander))
            {
                mod += 5;
            }

            if (ExternalTags.Contains(Tag.CommsBonus))
            {
                mod += 5;
            }

            if (ExternalTags.Contains(Tag.RemoteCommander))
            {
                mod -= 5;
            }

            if (ExternalTags.Contains(Tag.LocalCommander))
            {
                mod += 10;
            }

            if (Tags.Contains(Tag.DugIn))
            {
                mod += 10;
            }

            if (Tags.Contains(Tag.TunedUp))
            {
                mod += 5;
            }

            return mod;
        }

        public string FixedCombatModString()
        {
            StringBuilder sb = new StringBuilder();

            FixedCommonModString(sb);

            if (Tags.Contains(Tag.EliteGunner))
            {
                sb.Append(" +10 (elite)");
            }

            if (Tags.Contains(Tag.VeteranGunner))
            {
                sb.Append(" +5 (veteran)");
            }

            return sb.ToString();
        }

        public int FixedCombatMod()
        {
            int mod = 0;

            mod += FixedCommonMod();

            if (Tags.Contains(Tag.EliteGunner))
            {
                mod += 10;
            }

            if (Tags.Contains(Tag.VeteranGunner))
            {
                mod += 5;
            }

            return mod;
        }

        public string FixedInitModString()
        {
            StringBuilder sb = new StringBuilder();

            FixedCommonModString(sb);

            if (Tags.Contains(Tag.EliteDriver))
            {
                sb.Append(" +10 (elite)");
            }

            if (Tags.Contains(Tag.VeteranDriver))
            {
                sb.Append(" +5 (veteran)");
            }



            return sb.ToString();
        }

        public int FixedInitMod()
        {
            int mod = 0;

            mod += FixedCommonMod();

            if (Tags.Contains(Tag.EliteDriver))
            {
                mod += 10;
            }

            if (Tags.Contains(Tag.VeteranDriver))
            {
                mod += 5;
            }

            return mod;
        }

        public override string ToString()
        {
            String engagedString = Tags.Contains(Tag.EngagedThisTurn) ? "*" : "";
            String initString = CurrentInit > 0 ? $"{engagedString}{CurrentInit}:" : "";
            String healthString = $"({Health}%) ";
            String stunString = Tags.Contains(Tag.Stunned) ? "(stunned)" : "";
            String destroyedString = Tags.Contains(Tag.Destroyed) ? "(destroyed)" : "";
            String withdrawnString = Tags.Contains(Tag.Withdrawn) ? "(withdrawn)" : "";
            String withdrawingString = Tags.Contains(Tag.Withdrawing) ? "(withdrawing)" : "";
            return $"{initString} {healthString}{stunString}{destroyedString}{withdrawnString}{withdrawingString}{Name}";
        }

        public override bool Equals(object obj)
        {
            return obj is Unit &&
                ((Unit) obj).Name.Equals(Name);
        }
    }
}
