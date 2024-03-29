﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightCombatTracker
{
    public class Engagement
    {
        public const int NO_SUPPORT = 0;

        public UnitEquipmentTuple Attacker { get; set; }
        public UnitEquipmentTuple Defender { get; set; }
        public Dictionary<UnitEquipmentTuple, int> SupportingAttackers { get; set; }
        public Dictionary<UnitEquipmentTuple, int> SupportingDefenders { get; set; }
        public int supportDivider = NO_SUPPORT;
        public bool resolved = false;

        public bool IsSupportingEngagement
        {
            get
            {
                return supportDivider != NO_SUPPORT;
            }
        }

        public Engagement(UnitEquipmentTuple attacker, UnitEquipmentTuple defender, int supportDivider)
        {
            Attacker = attacker;
            Defender = defender;
            SupportingAttackers = new Dictionary<UnitEquipmentTuple, int>();
            SupportingDefenders = new Dictionary<UnitEquipmentTuple, int>();
            this.supportDivider = supportDivider;
        }

        public void Flip()
        {
            UnitEquipmentTuple hold = Attacker;
            Attacker = Defender;
            Defender = hold;
        }

        public void ForceNonSupporting()
        {
            supportDivider = NO_SUPPORT;
        }

        public override string ToString()
        {
            string resolvedString = resolved ? "(*)" : "";
            string support = IsSupportingEngagement ? " (supporting)" : "";
            return $"{resolvedString}{Attacker}{support} vs {Defender}";
        }

        public override bool Equals(object obj)
        {
            Engagement other = obj as Engagement;
            
            return other != null &&
                other.Attacker.Equals(Attacker) &&
                other.Defender.Equals(Defender);
        }

        public override int GetHashCode()
        {
            return $"{Attacker.Unit.Name}{Attacker.Equipment.Name}{Defender.Unit.Name}{Defender.Equipment.Name}".GetHashCode();
        }

        public string DetailedBreakdown()
        {
            String attackerCrit = Attacker.Unit.Tags.Contains(Tag.InitCrit) ? "Attacker has initiative crit.\r\n" : "";
            String attackerCritFail = Attacker.Unit.Tags.Contains(Tag.InitCritFail) ? "Attacker has initiative crit fail.\r\n" : "";
            String defenderCrit = Defender.Unit.Tags.Contains(Tag.InitCrit) ? "Defender has initiative crit.\r\n" : "";
            String defenderCritFail = Defender.Unit.Tags.Contains(Tag.InitCrit) ? "Defender has initiative crit fail.\r\n" : "";

            String attacker = Attacker.Unit.getApplicableModifierString(Defender.Unit, Attacker.Equipment, supportDivider, Defender.Equipment);

            // if it is a supporting engagement, we do not display the defenders mods/etc
            String defender = IsSupportingEngagement ?
                "" :
                $"{Defender.Unit.getApplicableModifierString(Attacker.Unit, Defender.Equipment, 1, Attacker.Equipment)}";

            String vsString = IsSupportingEngagement ? "" : " vs\r\n1d100";

            StringBuilder attackerSupportAccumulator = new StringBuilder();
            foreach (UnitEquipmentTuple attackSupporter in SupportingAttackers.Keys)
            {
                attackerSupportAccumulator.Append($" + {SupportingAttackers[attackSupporter]} from {attackSupporter.Unit.Name} ");
            }

            StringBuilder defenderSupportAccumulator = new StringBuilder();
            foreach (UnitEquipmentTuple defendSupporter in SupportingDefenders.Keys)
            {
                defenderSupportAccumulator.Append($" + {SupportingDefenders[defendSupporter]} from {defendSupporter.Unit.Name} ");
            }

            return $"{attackerCrit}{attackerCritFail}{defenderCrit}{defenderCritFail}{Attacker.Unit} vs {Defender.Unit}\r\n1d100{attacker}{attackerSupportAccumulator}{vsString}{defender}{defenderSupportAccumulator}";
        }

        public string ExecuteSupport(Random random, out int supportResult)
        {
            resolved = true;

            StringBuilder result = new StringBuilder();

            // store the original roll, the original + mod, and then the original + mod / support
            int attackerRoll = random.Next(1, 100);
            int attackerPreSupportResult = attackerRoll + Attacker.Unit.getApplicableModifier(Defender.Unit, Attacker.Equipment);

            supportResult = attackerPreSupportResult / (supportDivider == 0 ? 1 : supportDivider);

            result.AppendLine(DetailedBreakdown());

            string critFlag = attackerRoll == 1 || attackerRoll == 100 ? "(!)" : "";
            string stunText = "";

            // shitty support roll will stun the unit
            if (attackerRoll == 1 || supportResult < 1)
            {
                Attacker.Unit.Tags.Add(Tag.StunPending);
                stunText = $"; {Attacker.Unit} stunned;";
            }

            // great support roll will stun the target
            if (attackerRoll == 100)
            {
                Defender.Unit.Tags.Add(Tag.StunPending);
                stunText = $"; {Defender.Unit} stunned;";
            }

            result.AppendLine($"Roll: {attackerRoll}{critFlag} -> {attackerPreSupportResult} -> {supportResult}{stunText}");

            // update XP: shooting at someone's back is not really a learning experience
            Attacker.Unit.GunneryXP++;
            Attacker.Unit.DrivingXP++;

            return result.ToString();
        }

        public string Execute(Random random)
        {
            StringBuilder result = new StringBuilder();

            resolved = true;

            int attackerRoll = random.Next(1, 100);
            int attackerPreSupportResult = attackerRoll + Attacker.Unit.getApplicableModifier(Defender.Unit, Attacker.Equipment, Defender.Equipment);
            int attackerPostSupportResult = attackerPreSupportResult;
            foreach (int supportingAttack in SupportingAttackers.Values)
            {
                attackerPostSupportResult += supportingAttack;
            }

            int defenderRoll = random.Next(1, 100);
            int defenderPreSupportResult = defenderRoll + Defender.Unit.getApplicableModifier(Attacker.Unit, Defender.Equipment, Attacker.Equipment);
            int defenderPostSupportResult = defenderPreSupportResult;

            foreach (int supportingAttack in SupportingDefenders.Values)
            {
                defenderPostSupportResult += supportingAttack;
            }
            
            result.AppendLine(DetailedBreakdown());
            result.AppendLine($"{attackerRoll} -> {attackerPreSupportResult} -> {attackerPostSupportResult} vs");
            result.AppendLine($"{defenderRoll} -> {defenderPreSupportResult} -> {defenderPostSupportResult}");

            bool attackerCrit = attackerRoll == 100 || attackerPostSupportResult > 100;

            // work out stuns: natural 1s, natural 100s, or if the roll goes "out of bounds" after modifiers
            if (attackerRoll == 1 || attackerPostSupportResult < 1 || defenderRoll == 100 || defenderPostSupportResult > 100)
            {
                Attacker.Unit.Tags.Add(Tag.StunPending);
                result.AppendLine($"{Attacker.Unit} stunned!");
            }

            if (defenderRoll == 1 || defenderPostSupportResult < 1 || attackerCrit)
            {
                Defender.Unit.Tags.Add(Tag.StunPending);
                result.AppendLine($"{Defender.Unit} stunned!");
            }

            // apply damage
            int damage = Math.Abs(attackerPostSupportResult - defenderPostSupportResult);
            // attacker wins
            if (attackerPostSupportResult > defenderPostSupportResult)
            {
                int pdCount = getPointDefenseCount(Defender, SupportingDefenders.Keys.ToList());
                int damageDiscount = getPointDefenseDamageDiscount(Attacker, attackerPreSupportResult - defenderPreSupportResult,
                    SupportingAttackers, pdCount, result);
                damageDiscount += getNoDamageDamageDiscount(Attacker, attackerPostSupportResult - defenderPreSupportResult,
                    SupportingAttackers, Defender.Unit, result);
                int actualDamage = Math.Max(damage - damageDiscount, 0);

                ProcessDamage(Attacker.Unit, Attacker.Equipment, Defender.Unit, actualDamage, result, random, attackerCrit);
            }
            // defender wins
            else if (attackerPostSupportResult < defenderPostSupportResult &&
                DefenderCanDamageAttacker(result))
            {
                int pdCount = getPointDefenseCount(Attacker, SupportingAttackers.Keys.ToList());
                int damageDiscount = getPointDefenseDamageDiscount(Defender, defenderPreSupportResult - attackerPreSupportResult,
                    SupportingDefenders, pdCount, result);
                damageDiscount += getNoDamageDamageDiscount(Defender, defenderPreSupportResult - attackerPreSupportResult,
                    SupportingDefenders, Defender.Unit, result);
                int actualDamage = Math.Max(damage - damageDiscount, 0);

                ProcessDamage(Defender.Unit, Defender.Equipment, Attacker.Unit, actualDamage, result, random, attackerCrit);
            }
            // tie
            else
            {
                result.AppendLine("Tied; No damage.");
            }

            // clear up transient tags
            Attacker.Unit.Tags.Remove(Tag.TunedUp);
            Defender.Unit.Tags.Remove(Tag.TunedUp);

            // update XP; init loser gets more: we learn more from our failures
            Attacker.Unit.DrivingXP++;
            Defender.Unit.DrivingXP += 2;

            result.AppendLine();
            result.AppendLine("---");
            return result.ToString();
        }

        private void ProcessDamage(Unit shooter, Equipment shooterWeapon, Unit victim, int damage, StringBuilder result, 
            Random random, bool attackerCrit)
        {
            // update XP; we learn more from failures
            shooter.GunneryXP++;
            victim.GunneryXP += 2;

            if (ProcessDefense(Tag.Hologram, victim, result, random) ||
                ProcessDefense(Tag.MechShield, victim, result, random))
            {
                return;
            }

            if (ProcessSniperFire(result))
            {
                return;
            }

            if (victim.Tags.Contains(Tag.Aircraft) && !shooterWeapon.Effects.ContainsKey(Tag.Aircraft))
            {
                result.AppendLine("Aircraft can only be targeted by anti-air weapons.");
                return;
            }

            if (damage <= 0)
            {
                result.AppendLine("Damage reduced to 0;");
                return;
            }

            // if a unit is bunkered up, the bunker absorbs the damage instead
            Boolean victimInBunker = victim.Bunker != null && !shooterWeapon.Effects.ContainsKey(Tag.BunkerClearance);
            Boolean bunkerClearance = victim.Bunker != null && shooterWeapon.Effects.ContainsKey(Tag.BunkerClearance);

            Unit effectiveVictim = victimInBunker ? victim.Bunker : victim;

            if (victimInBunker)
            {
                result.AppendLine($"{victim.Name} is sheltered in {effectiveVictim.Name};");
            }

            // special case - someone's using bunker clearance weapon vs target
            if (bunkerClearance)
            {
                result.AppendLine($"Bunker-clearance weapon employed; target eliminated");
                victim.Health = 0;
                victim.Tags.Add(Tag.DestructionPending);
            }
            // special case - someone's using a sniper weapon vs infantry
            else if (shooterWeapon.Effects.ContainsKey(Tag.SniperLogic) && 
                (victim.HasTag(Tag.Armored) || !victim.HasTag(Tag.FootInfantry)))
            {
                if (attackerCrit)
                {
                    result.AppendLine("Operator(s) sniped; non-infantry unit eliminated");
                    victim.Health = 0;
                    victim.Tags.Add(Tag.DestructionPending);
                }
                else
                {
                    result.AppendLine("Sniper fire has no effect against non-infantry target");
                }
            }
            // normal case - do damage
            else
            {
                effectiveVictim.Health -= damage;

                result.Append($"{effectiveVictim.Name} takes {damage} damage; {effectiveVictim.Health} remaining;");

                if (effectiveVictim.Health <= 0)
                {
                    effectiveVictim.Tags.Add(Tag.DestructionPending);
                    result.Append($"Destroyed!");
                }
            }

            // ablative plating degrades when you are hit
            if (effectiveVictim.Tags.Contains(Tag.AblativePlating))
            {
                result.Append($"Ablative Plating Degraded.");
                effectiveVictim.Tags.Remove(Tag.AblativePlating);
            }
        }

        /// <summary>
        /// Do a defense check for the given unit; append the results to the given stringbuilder.
        /// Needs an RNG as well.
        /// </summary>
        private bool ProcessDefense(Tag tag, Unit victim, StringBuilder result, Random random)
        {
            if (victim.Tags.Contains(tag) ||
                victim.Bunker?.Tags?.Contains(tag) == true)
            {
                int holoRoll = random.Next(1, 101);
                result.AppendLine($"{tag} roll: {holoRoll}");
                if (holoRoll <= 50)
                {
                    result.AppendLine($"Target is using {tag}; defensive measure hit and disappears; damage negated");
                    victim.Tags.Remove(tag);
                    victim.Bunker?.Tags?.Remove(tag);
                    return true;
                } 
                else
                {
                    result.AppendLine($"Target is using {tag}; actual target hit.");
                }
            }

            return false;
        }

        private bool ProcessSniperFire(StringBuilder result)
        {
            // rules: engagement attacker has to be the sniper; engagement cannot have any other hostiles in it
            if (Attacker.Equipment.Effects.ContainsKey(Tag.SniperLogic) &&
                Defender.Unit.HasTag(Tag.FootInfantry) &&
                !Defender.Unit.HasTag(Tag.Armored) &&
                SupportingDefenders.Count == 0)
            {
                result.AppendLine("Sniper automatically destroys unarmored infantry target.");
                Defender.Unit.Health = 0;
                Defender.Unit.Tags.Add(Tag.DestructionPending);
                return true;
            }
            else if (Attacker.Equipment.Effects.ContainsKey(Tag.SniperLogic))
            {
                result.AppendLine("Defender supported, armored or not infantry; engaging as normal.");
            }

            return false;
        }

        private bool DefenderCanDamageAttacker(StringBuilder result)
        {
            if (Attacker.Equipment.Effects.ContainsKey(Tag.Artillery) &&
                Attacker.Unit.HasTag(Tag.LongRange) &&
                !Defender.Equipment.Effects.ContainsKey(Tag.Artillery))
            {
                result.AppendLine("Defender cannot inflict damage on artillery at long range.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get a count of point defense systems in the given unit group
        /// </summary>
        private int getPointDefenseCount(UnitEquipmentTuple primaryUnit, List<UnitEquipmentTuple> units)
        {
            int pointDefenseCount = primaryUnit.Unit.Tags.Contains(Tag.PointDefenseLaser) ? 1 : 0;

            foreach (var unit in units)
            {
                pointDefenseCount += unit.Unit.Tags.Contains(Tag.PointDefenseLaser) ? 1 : 0;
            }

            return pointDefenseCount;
        }

        /// <summary>
        /// Given a primary unit, primary unit roll, supporting attack resolutions and a count of active PD systems
        /// Give me how much damage we're losing here.
        /// </summary>
        private int getPointDefenseDamageDiscount(UnitEquipmentTuple primaryUnit, int primaryUnitRollDiff,
            Dictionary<UnitEquipmentTuple, int> supportingUnits, int pdCount, StringBuilder result)
        {
            if (pdCount == 0)
            {
                return 0;
            }

            int pdUsed = 0;
            int damageDiscount = 0;

            if (primaryUnitRollDiff > 0 &&
                primaryUnit.Equipment.Effects.ContainsKey(Tag.PointDefenseUseful))
            {
                pdUsed++;
                damageDiscount = primaryUnitRollDiff;
            }

            foreach (UnitEquipmentTuple unit in supportingUnits.Keys)
            {
                if (pdUsed >= pdCount)
                {
                    break;
                }

                if (unit.Equipment.Effects.ContainsKey(Tag.PointDefenseUseful))
                {
                    pdUsed++;
                    damageDiscount += supportingUnits[unit];
                }
            }

            if (damageDiscount > 0)
            {
                result.AppendLine($"{damageDiscount} damage negated by point defense!");
            }

            return damageDiscount;
        }

        /// <summary>
        /// Given a primary unit, primary unit roll, supporting attack resolutions,
        /// Give me how much damage we're losing here due to an attacker being unarmed
        /// </summary>
        private int getNoDamageDamageDiscount(UnitEquipmentTuple primaryUnit, int primaryUnitRollDiff,
            Dictionary<UnitEquipmentTuple, int> supportingUnits, Unit defender, StringBuilder result)
        {
            int damageDiscount = 0;

            string noDamageReason = primaryUnit.Equipment.getNoDamageReason(primaryUnit.Unit, defender);

            if (primaryUnitRollDiff > 0 && !string.IsNullOrEmpty(noDamageReason))
            {
                damageDiscount = primaryUnitRollDiff;
                result.AppendLine($"{primaryUnit} {noDamageReason}, subtracting {primaryUnitRollDiff} from damage");
            }

            foreach (UnitEquipmentTuple unit in supportingUnits.Keys)
            {
                noDamageReason = unit.Equipment.getNoDamageReason(unit.Unit, defender);

                if (!string.IsNullOrEmpty(noDamageReason))
                {
                    damageDiscount += supportingUnits[unit];
                }
            }

            if (damageDiscount > 0)
            {
                result.AppendLine($"{damageDiscount} total damage negated!");
            }

            return damageDiscount;
        }
    }
}
