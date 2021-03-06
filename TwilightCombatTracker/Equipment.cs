using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightCombatTracker
{
    public class Equipment : IComparable
    {
        public const int NO_EFFECT = -999;

        public static List<Equipment> EquipmentDatabase;

        static Equipment() 
        {
            EquipmentDatabase = new List<Equipment>();

            // Nod Infantry Equipment
            Equipment nodInfantrySmallArms = new Equipment();
            nodInfantrySmallArms.Name = "Nod Rifle";
            nodInfantrySmallArms.Effects.Add(Tag.Vehicle, -5);
            nodInfantrySmallArms.Effects.Add(Tag.Armored, -5);
            nodInfantrySmallArms.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(nodInfantrySmallArms);

            Equipment nodInfantryLaser = new Equipment();
            nodInfantryLaser.Name = "Nod Laser Rifle";
            nodInfantryLaser.Effects.Add(Tag.General, +5);
            nodInfantryLaser.Effects.Add(Tag.Laser, +0);
            EquipmentDatabase.Add(nodInfantryLaser);

            Equipment nodInfantryRockets = new Equipment();
            nodInfantryRockets.Name = "Nod Rockets";
            nodInfantryRockets.Effects.Add(Tag.FootInfantry, -5);
            nodInfantryRockets.Effects.Add(Tag.Aircraft, +5);
            nodInfantryRockets.Effects.Add(Tag.Vehicle, +5);
            nodInfantryRockets.Effects.Add(Tag.Armored, -5);
            nodInfantryRockets.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(nodInfantryRockets);

            Equipment nodInfantryTibRockets = new Equipment();
            nodInfantryTibRockets.Name = "Nod T-Rockets";
            nodInfantryTibRockets.Effects.Add(Tag.Aircraft, +10);
            nodInfantryTibRockets.Effects.Add(Tag.Vehicle, +10);
            nodInfantryTibRockets.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(nodInfantryTibRockets);

            Equipment nodInfantryFlamer = new Equipment();
            nodInfantryFlamer.Name = "Nod Infantry Flamer";
            nodInfantryFlamer.Effects.Add(Tag.FootInfantry, +15);
            nodInfantryFlamer.Effects.Add(Tag.Armored, -5);
            nodInfantryFlamer.Effects.Add(Tag.FastSpeed, -5);
            nodInfantryFlamer.Effects.Add(Tag.BunkerClearance, 0);
            EquipmentDatabase.Add(nodInfantryFlamer);

            Equipment gdiRifles = new Equipment();
            gdiRifles.Name = "GDI Rifles";
            gdiRifles.Effects.Add(Tag.Armored, -5);
            gdiRifles.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(gdiRifles);

            Equipment nodHandLaser = new Equipment();
            nodHandLaser.Name = "Nod Laser Pistol";
            nodHandLaser.Effects.Add(Tag.Laser, 0);
            EquipmentDatabase.Add(nodHandLaser);

            // APC/Heavy Infantry Equipment
            Equipment apcMinigun = new Equipment();
            apcMinigun.Name = "APC Minigun";
            apcMinigun.Effects.Add(Tag.Armored, -5);
            apcMinigun.Effects.Add(Tag.FootInfantry, 10);
            apcMinigun.Effects.Add(Tag.Visceroid, -20);
            apcMinigun.Effects.Add(Tag.Aircraft, -5);
            EquipmentDatabase.Add(apcMinigun);

            Equipment apcRocket = new Equipment();
            apcRocket.Name = "APC Rocket";
            apcRocket.Effects.Add(Tag.FastSpeed, 10);
            apcRocket.Effects.Add(Tag.Aircraft, 10);
            apcRocket.Effects.Add(Tag.FootInfantry, -5);
            apcRocket.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(apcRocket);

            Equipment apcGrenade = new Equipment();
            apcGrenade.Name = "APC Grenade";
            apcGrenade.Effects.Add(Tag.FastSpeed, -5);
            apcGrenade.Effects.Add(Tag.Armored, -5);
            apcGrenade.Effects.Add(Tag.FootInfantry, 15);
            apcGrenade.Effects.Add(Tag.BunkerClearance, 0);
            EquipmentDatabase.Add(apcGrenade);

            Equipment apcLaser = new Equipment();
            apcLaser.Name = "APC Laser";
            apcLaser.Effects.Add(Tag.Laser, 0);
            apcLaser.Effects.Add(Tag.FootInfantry, 15);
            EquipmentDatabase.Add(apcLaser);

            Equipment apcHeavyLaser = new Equipment();
            apcHeavyLaser.Name = "APC Heavy Laser";
            apcHeavyLaser.Effects.Add(Tag.Laser, 0);
            apcHeavyLaser.Effects.Add(Tag.Vehicle, 15);
            EquipmentDatabase.Add(apcHeavyLaser);

            Equipment nodBuggyMG = new Equipment();
            nodBuggyMG.Name = "Buggy MG";
            nodBuggyMG.Effects.Add(Tag.Vehicle, -5);
            nodBuggyMG.Effects.Add(Tag.Armored, -5);
            nodBuggyMG.Effects.Add(Tag.FootInfantry, 0);
            nodBuggyMG.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(nodBuggyMG);

            Equipment nodBuggyLaser = new Equipment();
            nodBuggyLaser.Name = "Buggy Laser";
            nodBuggyLaser.Effects.Add(Tag.General, +5);
            nodBuggyLaser.Effects.Add(Tag.Aircraft, +5);
            nodBuggyLaser.Effects.Add(Tag.Laser, +0);
            EquipmentDatabase.Add(nodBuggyLaser);

            // Scorpion Tank equipment
            Equipment scorpionTurret = new Equipment();
            scorpionTurret.Name = "105mm Cannon";
            scorpionTurret.Effects.Add(Tag.Vehicle, 15);
            scorpionTurret.Effects.Add(Tag.Armored, -15);
            scorpionTurret.Effects.Add(Tag.Visceroid, -20);
            scorpionTurret.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(scorpionTurret);

            Equipment scorpionBiprop = new Equipment();
            scorpionBiprop.Name = "Biprop Cannon";
            scorpionBiprop.Effects.Add(Tag.Vehicle, 15);
            scorpionBiprop.Effects.Add(Tag.Visceroid, -20);
            scorpionBiprop.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(scorpionBiprop);

            Equipment scorpionLaser = new Equipment();
            scorpionLaser.Name = "Laser Cannon";
            scorpionLaser.Effects.Add(Tag.Vehicle, 20);
            scorpionLaser.Effects.Add(Tag.Laser, 0);
            EquipmentDatabase.Add(scorpionLaser);

            Equipment scorpionPlasma = new Equipment();
            scorpionPlasma.Name = "Plasma Cannon";
            scorpionPlasma.Effects.Add(Tag.Vehicle, 25);
            EquipmentDatabase.Add(scorpionPlasma);

            // Pitbull Equipment
            Equipment pitbullMissile = new Equipment();
            pitbullMissile.Name = "Missile Launcher";
            pitbullMissile.Effects.Add(Tag.Aircraft, 10);
            pitbullMissile.Effects.Add(Tag.FastSpeed, 10);
            pitbullMissile.Effects.Add(Tag.FootInfantry, -10);
            pitbullMissile.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(pitbullMissile);

            Equipment pitbullMortar = new Equipment();
            pitbullMortar.Name = "Mortar";
            pitbullMortar.Effects.Add(Tag.NoneOfTheAbove, -10);
            pitbullMortar.Effects.Add(Tag.Structure, 10);
            pitbullMortar.Effects.Add(Tag.FootInfantry, 10);
            pitbullMortar.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(pitbullMortar);

            // Predator Equipment
            Equipment predatorRailgun = new Equipment();
            predatorRailgun.Name = "Railgun";
            predatorRailgun.Effects.Add(Tag.Vehicle, 20);
            predatorRailgun.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(predatorRailgun);

            // aircraft Equipment:
            Equipment orcaRockets = new Equipment();
            orcaRockets.Name = "Orca AtG";
            orcaRockets.Effects.Add(Tag.Vehicle, +15);
            orcaRockets.Effects.Add(Tag.Structure, +15);
            orcaRockets.Effects.Add(Tag.FootInfantry, -15);
            orcaRockets.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(orcaRockets);

            Equipment orcaAAMissiles = new Equipment();
            orcaAAMissiles.Name = "Orca AtA";
            orcaAAMissiles.Effects.Add(Tag.NoneOfTheAbove, NO_EFFECT);
            orcaAAMissiles.Effects.Add(Tag.Aircraft, 15);
            orcaAAMissiles.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(orcaAAMissiles);

            Equipment orcaRailgun = new Equipment();
            orcaRailgun.Name = "Orca Railgun";
            orcaRailgun.Effects.Add(Tag.Visceroid, -20);
            orcaRailgun.Effects.Add(Tag.Aircraft, 0);
            EquipmentDatabase.Add(orcaRailgun);

            Equipment venomMG = new Equipment();
            venomMG.Name = "VenomMG";
            venomMG.Effects.Add(Tag.Visceroid, -20);
            venomMG.Effects.Add(Tag.Vehicle, -5);
            venomMG.Effects.Add(Tag.Armored, -5);
            venomMG.Effects.Add(Tag.Aircraft, 0);
            EquipmentDatabase.Add(venomMG);

            Equipment venomLaser = new Equipment();
            venomLaser.Name = "Venom Laser";
            venomLaser.Effects.Add(Tag.General, +5);
            venomLaser.Effects.Add(Tag.Aircraft, 0);
            venomLaser.Effects.Add(Tag.Laser, 0);
            EquipmentDatabase.Add(venomLaser);

            // special equipment
            Equipment unarmed = new Equipment();
            unarmed.Name = "Unarmed";
            unarmed.Effects.Add(Tag.NoDamage, 0);
            EquipmentDatabase.Add(unarmed);

            Equipment visceroidSpray = new Equipment();
            visceroidSpray.Name = "Visceroid Spray";
            visceroidSpray.Effects.Add(Tag.FootInfantry, +20);
            EquipmentDatabase.Add(visceroidSpray);

            Equipment sniperRifle = new Equipment();
            sniperRifle.Name = "Sniper Rifle";
            sniperRifle.Effects.Add(Tag.SniperLogic, 0); //insta-kill infantry, no damage vs anything else except on crit
            EquipmentDatabase.Add(sniperRifle);

            Equipment basicArtillery = new Equipment();
            basicArtillery.Name = "Artillery";
            basicArtillery.Effects.Add(Tag.Artillery, 0);
            basicArtillery.Effects.Add(Tag.FootInfantry, +25);
            basicArtillery.Effects.Add(Tag.Structure, +25);
            basicArtillery.Effects.Add(Tag.Visceroid, +25);
            basicArtillery.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(basicArtillery);


            // infantry equipment
            /*Equipment nodInfantrySmallArms = new Equipment();
            nodInfantrySmallArms.Name = "Small Arms";
            nodInfantrySmallArms.Effects.Add(Tag.NoneOfTheAbove, -10);
            nodInfantrySmallArms.Effects.Add(Tag.FootInfantry, 0);
            nodInfantrySmallArms.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(nodInfantrySmallArms);

            Equipment standardSmallArms = new Equipment();
            standardSmallArms.Name = "Mixed Small Arms";
            standardSmallArms.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(standardSmallArms);

            Equipment nonCombatantSmallArms = new Equipment();
            nonCombatantSmallArms.Name = "Non-Combatant Small Arms";
            nonCombatantSmallArms.Effects.Add(Tag.NoneOfTheAbove, -20);
            EquipmentDatabase.Add(nonCombatantSmallArms);

            Equipment infantryRockets = new Equipment();
            infantryRockets.Name = "Rockets";
            infantryRockets.Effects.Add(Tag.Vehicle, +15);
            infantryRockets.Effects.Add(Tag.Aircraft, +15);
            infantryRockets.Effects.Add(Tag.FootInfantry, -15);
            infantryRockets.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(infantryRockets);

            Equipment nodInfantryRockets = new Equipment();
            nodInfantryRockets.Name = "Nod Rockets";
            nodInfantryRockets.Effects.Add(Tag.FootInfantry, -10);
            nodInfantryRockets.Effects.Add(Tag.Aircraft, 0);
            nodInfantryRockets.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(nodInfantryRockets);

            Equipment nodTiberiumRockets = new Equipment();
            nodTiberiumRockets.Name = "Nod Rockets";
            nodTiberiumRockets.Effects.Add(Tag.FootInfantry, -5);
            nodTiberiumRockets.Effects.Add(Tag.Aircraft, 5);
            nodTiberiumRockets.Effects.Add(Tag.Vehicle, 10);
            nodTiberiumRockets.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(nodTiberiumRockets);

            Equipment nodInfernoRockets = new Equipment();
            nodInfernoRockets.Name = "Inferno Rockets";
            nodInfernoRockets.Effects.Add(Tag.NoneOfTheAbove, -10);
            nodInfernoRockets.Effects.Add(Tag.Structure, 0);
            nodInfernoRockets.Effects.Add(Tag.FootInfantry, 0);
            nodInfernoRockets.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(nodInfernoRockets);

            Equipment nodBuggyMG = new Equipment();
            nodBuggyMG.Name = "Buggy MG";
            nodBuggyMG.Effects.Add(Tag.NoneOfTheAbove, -10);
            nodBuggyMG.Effects.Add(Tag.FootInfantry, 0);
            nodBuggyMG.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(nodBuggyMG);

            Equipment nodBuggyLaser = new Equipment();
            nodBuggyLaser.Name = "Buggy Laser";
            nodBuggyLaser.Effects.Add(Tag.Laser, 0);
            nodBuggyLaser.Effects.Add(Tag.Aircraft, 0);
            EquipmentDatabase.Add(nodBuggyLaser);

            Equipment infantryGrenades = new Equipment();
            infantryGrenades.Name = "Grenades";
            infantryGrenades.Effects.Add(Tag.FastSpeed, -15);
            infantryGrenades.Effects.Add(Tag.Armored, -15);
            infantryGrenades.Effects.Add(Tag.FootInfantry, 15);
            infantryGrenades.Effects.Add(Tag.BunkerClearance, 0);
            EquipmentDatabase.Add(infantryGrenades);

            Equipment infantryFlamer = new Equipment();
            infantryFlamer.Name = "Infantry Flamer";
            infantryFlamer.Effects.Add(Tag.FootInfantry, +15);
            infantryFlamer.Effects.Add(Tag.BunkerClearance, 0);
            infantryFlamer.Effects.Add(Tag.Visceroid, +15);
            EquipmentDatabase.Add(infantryFlamer);

            Equipment nodHandLaser = new Equipment();
            nodHandLaser.Name = "Laser Pistol";
            nodHandLaser.Effects.Add(Tag.Laser, 0);
            EquipmentDatabase.Add(nodHandLaser);

            Equipment infantryLaser = new Equipment();
            infantryLaser.Name = "Laser Rifle";
            infantryLaser.Effects.Add(Tag.Laser, 0);
            infantryLaser.Effects.Add(Tag.NoneOfTheAbove, +5);
            EquipmentDatabase.Add(infantryLaser);

            Equipment sniperRifle = new Equipment();
            sniperRifle.Name = "Sniper Rifle";
            sniperRifle.Effects.Add(Tag.SniperLogic, 0); //insta-kill infantry, no damage vs anything else except on crit
            EquipmentDatabase.Add(sniperRifle);

            // APC Equipment
            Equipment apcMinigun = new Equipment();
            apcMinigun.Name = "APC Minigun";
            apcMinigun.Effects.Add(Tag.Armored, -5);
            apcMinigun.Effects.Add(Tag.FootInfantry, 10);
            apcMinigun.Effects.Add(Tag.Visceroid, -20);
            apcMinigun.Effects.Add(Tag.Aircraft, -5);
            EquipmentDatabase.Add(apcMinigun);

            Equipment apcRocket = new Equipment();
            apcRocket.Name = "APC Rocket";
            apcRocket.Effects.Add(Tag.FastSpeed, 10);
            apcRocket.Effects.Add(Tag.Aircraft, 10);
            apcRocket.Effects.Add(Tag.FootInfantry, -5);
            apcRocket.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(apcRocket);

            Equipment apcGrenade = new Equipment();
            apcGrenade.Name = "APC Grenade";
            apcGrenade.Effects.Add(Tag.FastSpeed, -15);
            apcGrenade.Effects.Add(Tag.Armored, -15);
            apcGrenade.Effects.Add(Tag.Aircraft, -15);
            apcGrenade.Effects.Add(Tag.FootInfantry, 15);
            EquipmentDatabase.Add(apcGrenade);

            // Pitbull Equipment
            Equipment pitbullMissile = new Equipment();
            pitbullMissile.Name = "Missile Launcher";
            pitbullMissile.Effects.Add(Tag.Aircraft, 10);
            pitbullMissile.Effects.Add(Tag.FastSpeed, 10);
            pitbullMissile.Effects.Add(Tag.FootInfantry, -10);
            pitbullMissile.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(pitbullMissile);

            Equipment pitbullMortar = new Equipment();
            pitbullMortar.Name = "Mortar";
            pitbullMortar.Effects.Add(Tag.NoneOfTheAbove, -10);
            pitbullMortar.Effects.Add(Tag.Structure, 10);
            pitbullMortar.Effects.Add(Tag.FootInfantry, 10);
            EquipmentDatabase.Add(pitbullMortar);

            // Predator Equipment
            Equipment predatorRailgun = new Equipment();
            predatorRailgun.Name = "Railgun";
            predatorRailgun.Effects.Add(Tag.Vehicle, 20);
            predatorRailgun.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(predatorRailgun);

            Equipment dualRailgun = new Equipment();
            dualRailgun.Name = "Dual Railgun";
            dualRailgun.Effects.Add(Tag.Vehicle, 20);
            dualRailgun.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(dualRailgun);

            // Scorpion Equipment
            // gun is effective vs vehicles, but not armored ones
            Equipment scorpionTurret = new Equipment();
            scorpionTurret.Name = "105mm Cannon";
            scorpionTurret.Effects.Add(Tag.Vehicle, 15);
            scorpionTurret.Effects.Add(Tag.Armored, -15);
            scorpionTurret.Effects.Add(Tag.Visceroid, -20);
            scorpionTurret.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(scorpionTurret);

            Equipment scorpionBiprop = new Equipment();
            scorpionBiprop.Name = "Biprop Cannon";
            scorpionBiprop.Effects.Add(Tag.Vehicle, 15);
            scorpionBiprop.Effects.Add(Tag.Visceroid, -20);
            scorpionBiprop.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(scorpionBiprop);

            Equipment scorpionLaser = new Equipment();
            scorpionLaser.Name = "Scorpion Laser";
            scorpionLaser.Effects.Add(Tag.Vehicle, 15);
            scorpionLaser.Effects.Add(Tag.Laser, 0);
            EquipmentDatabase.Add(scorpionLaser);

            // flame tank equipment
            Equipment tankFlamer = new Equipment();
            tankFlamer.Name = "Flamer";
            tankFlamer.Effects.Add(Tag.FootInfantry, 30);
            tankFlamer.Effects.Add(Tag.BunkerClearance, 0);
            tankFlamer.Effects.Add(Tag.Visceroid, 30);
            EquipmentDatabase.Add(tankFlamer);

            // Turret Equipment
            Equipment turretMG = new Equipment();
            turretMG.Name = "Turret MG";
            turretMG.Effects.Add(Tag.FootInfantry, 5);
            turretMG.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(turretMG);

            Equipment turretRailgun = new Equipment();
            turretRailgun.Name = "Turret Railgun";
            turretRailgun.Effects.Add(Tag.Vehicle, 10);
            turretRailgun.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(turretRailgun);

            Equipment harvesterRailgun = new Equipment();
            harvesterRailgun.Name = "Harvester Railgun";
            harvesterRailgun.Effects.Add(Tag.Vehicle, 15);
            harvesterRailgun.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(harvesterRailgun);

            Equipment turretAA = new Equipment();
            turretAA.Name = "Turret AA";
            turretAA.Effects.Add(Tag.NoneOfTheAbove, NO_EFFECT);
            turretAA.Effects.Add(Tag.Aircraft, 10);
            EquipmentDatabase.Add(turretAA);

            Equipment turretCannon = new Equipment();
            turretCannon.Name = "105mm turret";
            turretCannon.Effects.Add(Tag.Vehicle, 5);
            turretCannon.Effects.Add(Tag.FootInfantry, -5);
            turretCannon.Effects.Add(Tag.Visceroid, -20);
            turretCannon.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(turretCannon);

            // aircraft Equipment:
            Equipment orcaRockets = new Equipment();
            orcaRockets.Name = "Orca Rockets";
            orcaRockets.Effects.Add(Tag.Vehicle, +15);
            orcaRockets.Effects.Add(Tag.Structure, +15);
            orcaRockets.Effects.Add(Tag.FootInfantry, -15);
            orcaRockets.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(orcaRockets);

            Equipment orcaAAMissiles = new Equipment();
            orcaAAMissiles.Name = "Orca AA Missiles";
            orcaAAMissiles.Effects.Add(Tag.NoneOfTheAbove, NO_EFFECT);
            orcaAAMissiles.Effects.Add(Tag.Aircraft, 10);
            orcaAAMissiles.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(orcaAAMissiles);

            Equipment orcaRailgun = new Equipment();
            orcaRailgun.Name = "Orca Railgun";
            orcaRailgun.Effects.Add(Tag.Visceroid, -20);
            EquipmentDatabase.Add(orcaRailgun);

            Equipment venomMG = new Equipment();
            venomMG.Name = "VenomMG";
            venomMG.Effects.Add(Tag.Visceroid, -20);
            venomMG.Effects.Add(Tag.Aircraft, 0);
            EquipmentDatabase.Add(venomMG);

            Equipment firehawkAG = new Equipment();
            firehawkAG.Name = "Air-To-Ground";
            firehawkAG.Effects.Add(Tag.Vehicle, 30);
            firehawkAG.Effects.Add(Tag.FootInfantry, 30);
            firehawkAG.Effects.Add(Tag.Visceroid, 30);
            firehawkAG.Effects.Add(Tag.Aircraft, NO_EFFECT);
            EquipmentDatabase.Add(firehawkAG);

            Equipment firehawkAA = new Equipment();
            firehawkAA.Name = "Air-To-Air";
            firehawkAA.Effects.Add(Tag.Vehicle, NO_EFFECT);
            firehawkAA.Effects.Add(Tag.FootInfantry, NO_EFFECT);
            firehawkAA.Effects.Add(Tag.Visceroid, NO_EFFECT);
            firehawkAA.Effects.Add(Tag.Aircraft, 15);
            firehawkAA.Effects.Add(Tag.LongRange, 15);
            EquipmentDatabase.Add(firehawkAA);

            Equipment barghestAA = new Equipment();
            barghestAA.Name = "Plasma Cannons";
            barghestAA.Effects.Add(Tag.Vehicle, 30);
            barghestAA.Effects.Add(Tag.Aircraft, 30);
            barghestAA.Effects.Add(Tag.Visceroid, 30);
            barghestAA.Effects.Add(Tag.FootInfantry, 30);
            barghestAA.Effects.Add(Tag.LongRange, NO_EFFECT);
            EquipmentDatabase.Add(barghestAA);

            // Special case:
            Equipment unarmed = new Equipment();
            unarmed.Name = "Unarmed";
            unarmed.Effects.Add(Tag.NoDamage, 0);
            EquipmentDatabase.Add(unarmed);

            Equipment visceroidSpray = new Equipment();
            visceroidSpray.Name = "Visceroid Spray";
            visceroidSpray.Effects.Add(Tag.FootInfantry, +20);
            EquipmentDatabase.Add(visceroidSpray);

            Equipment basicArtillery = new Equipment();
            basicArtillery.Name = "Artillery";
            basicArtillery.Effects.Add(Tag.Artillery, 0);
            basicArtillery.Effects.Add(Tag.FootInfantry, +25);
            basicArtillery.Effects.Add(Tag.Structure, +25);
            basicArtillery.Effects.Add(Tag.Visceroid, +25);
            basicArtillery.Effects.Add(Tag.PointDefenseUseful, 0);
            EquipmentDatabase.Add(basicArtillery);*/
        }


        public string Name { get; set; }
        public Dictionary<Tag, int> Effects = new Dictionary<Tag, int>();
        public string NegativeEffectName;

        /// <summary>
        /// Returns a sum of the modifiers applicable against the unit
        /// </summary>
        public int getModifier(Unit unit)
        {
            int mod = 0;
            bool modFound = false;

            foreach (Tag tag in unit.Tags) 
            { 
                if (Effects.ContainsKey(tag))
                {
                    mod += Effects[tag];
                    modFound = true;
                }
            }

            if (!modFound && Effects.ContainsKey(Tag.NoneOfTheAbove) && Effects[Tag.NoneOfTheAbove] != NO_EFFECT)
            {
                mod += Effects[Tag.NoneOfTheAbove];
            }

            if (Effects.ContainsKey(Tag.General))
            {
                mod += Effects[Tag.General];
            }

            return mod;
        }

        /// <summary>
        /// Returns a string summary of the modifiers applicable against the unit
        /// </summary>
        public string getModifierString(Unit unit)
        {
            StringBuilder sb = new StringBuilder();
            bool modFound = false;

            bool hasWeakRearArmor = unit.HasTag(Tag.WeakRearArmor);

            foreach (Tag tag in unit.Tags)
            {
                if (Effects.ContainsKey(tag))
                {
                    sb.AppendFormat("{0}{1} ({2} vs {3})", Effects[tag] >= 0 ? "+" : "", Effects[tag], Name, tag);
                    modFound = true;
                }
            }

            // if we no other mods and a "none of the above" tag that's not "NO EFFECT"
            if (!modFound && Effects.ContainsKey(Tag.NoneOfTheAbove) && Effects[Tag.NoneOfTheAbove] != NO_EFFECT)
            {
                sb.AppendFormat("{0}{1} ({2}{3})", Effects[Tag.NoneOfTheAbove] >= 0 ? "+" : "", 
                    Effects[Tag.NoneOfTheAbove], 
                    Name,
                    String.IsNullOrEmpty(NegativeEffectName) ? "" : $" vs {NegativeEffectName}");
            }

            if (Effects.ContainsKey(Tag.General))
            {
                sb.AppendFormat("{0}{1} (general)", Effects[Tag.General] >= 0 ? "+" : "", Effects[Tag.General]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Does this piece of equipment have any modifiers that are applicable to the unit in question.
        /// Useful to determine if "None of the Above" should be applied.
        /// </summary>
        public bool hasAnyApplicableModifiers(Unit unit)
        {
            foreach (Tag tag in unit.Tags)
            {
                if (Effects.ContainsKey(tag))
                {
                    return true;
                }
            }

            return false;
        }

        public string getReadout()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var effect in Effects.Keys)
            {
                sb.AppendLine($"{effect} : {Effects[effect]}");
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Equipment &&
                ((Equipment) obj).Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public int CompareTo(object other)
        {
            if (!(other is Equipment))
            {
                return 0;
            }

            return Name.CompareTo(((Equipment) other).Name);
        }

        public string getNoDamageReason(Unit shooter, Unit target)
        {
            if (Effects.ContainsKey(Tag.NoDamage))
            {
                return "unarmed";
            }

            if (shooter.HasTag(Tag.Stunned))
            {
                return "stunned";
            }

            if (target.HasTag(Tag.AblativePlating) && Effects.ContainsKey(Tag.Laser))
            {
                return "using laser vs ablative plating";
            }

            if (!hasAnyApplicableModifiers(target) &&
                Effects.ContainsKey(Tag.NoneOfTheAbove) &&
                Effects[Tag.NoneOfTheAbove] == Equipment.NO_EFFECT)
            {
                return "using completely ineffective weapon";
            }

            return String.Empty;
        }
    }
}
