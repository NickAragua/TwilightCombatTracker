using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightCombatTracker
{
    public enum TagType
    {
        // a type of unit, e.g. infantry, aircraft
        UnitTypeTag,
        // a property of the unit, e.g. armored, veteran
        UnitPropertyTag,
        // a property of a piece of equipment, e.g. laser, bunker clearance
        EquipmentPropertyTag,
        // transient information about a unit: do we have a stun or a kill pending, etc
        TransientUnitPropertyTag,
        // global property not directly associated with a particular unit
        EnvironmentTag,
        // faction: GDI, Nod, neutral (?)
        UnitOwnerTag
    }

    public enum Tag
    {
        [Category("UnitTypeTag")]
        Vehicle = 0,
        [Category("UnitTypeTag")]
        FootInfantry = 1,
        [Category("UnitTypeTag")]
        Aircraft = 2,
        [Category("UnitTypeTag")]
        Structure = 3,
        [Category("UnitPropertyTag")]
        FastSpeed = 4,
        [Category("UnitPropertyTag")]
        Armored = 5,
        [Category("UnitPropertyTag")]
        VeteranDriver = 6,
        [Category("UnitPropertyTag")]
        EliteDriver = 7,
        [Category("EquipmentPropertyTag")]
        Laser = 8,
        [Category("UnitPropertyTag")]
        AblativePlating = 9,
        [Category("UnitPropertyTag")]
        WeakRearArmor = 10,
        [Category("UnitPropertyTag")]
        NoneOfTheAbove = 11,
        [Category("TransientUnitPropertyTag")]
        InitCrit = 12,
        [Category("TransientUnitPropertyTag")]
        InitCritFail = 13,
        [Category("TransientUnitPropertyTag")]
        StunPending = 14,
        [Category("TransientUnitPropertyTag")]
        Withdrawing = 15,
        [Category("UnitStatusTag")]
        Withdrawn = 16,
        [Category("UnitStatusTag")]
        Destroyed = 17,
        [Category("UnitStatusTag")]
        Stunned = 18,
        [Category("UnitStatusTag")]
        TunedUp = 19,
        [Category("UnitStatusTag")]
        DugIn = 20,
        [Category("EnvironmentTag")]
        RockyTerrain = 21,
        [Category("EnvironmentTag")]
        UrbanTerrain = 22,
        [Category("EnvironmentTag")]
        Commander = 23,
        [Category("EnvironmentTag")]
        CommsBonus = 24,
        [Category("EnvironmentTag")]
        RemoteCommander = 25,
        [Category("UnitPropertyTag")]
        VeteranGunner = 27,
        [Category("UnitPropertyTag")]
        EliteGunner = 28,
        [Category("TransientUnitPropertyTag")]
        DestructionPending = 29,
        [Category("TransientUnitPropertyTag")]
        EngagedThisTurn = 30,
        [Category("EquipmentPropertyTag")]
        BunkerClearance = 31,
        [Category("UnitPropertyTag")]
        Bunker = 32,
        [Category("UnitOwnerTag")]
        GDI = 33,
        [Category("UnitOwnerTag")]
        Nod = 34,
        [Category("EnvironmentTag")]
        LocalCommander = 35,
        [Category("EnvironmentTag")]
        Sandbags = 36,
        [Category("EquipmentPropertyTag")]
        NoDamage = 37,
        [Category("UnitPropertyTag")]
        Hologram = 38,
        [Category("UnitPropertyTag")]
        Visceroid = 39
    }

    public static class TagProperties {
        public static int CompareTag(Tag first, Tag second)
        {
            return first.ToString().CompareTo(second.ToString());
        }

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}
