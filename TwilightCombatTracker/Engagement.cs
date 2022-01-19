using System;
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

            String attacker = Attacker.Unit.getApplicableModifierString(Defender.Unit, Attacker.Equipment, supportDivider);

            // if it is a supporting engagement, we do not display the defenders mods/etc
            String defender = IsSupportingEngagement ?
                "" :
                $"{Defender.Unit.getApplicableModifierString(Attacker.Unit, Defender.Equipment, 1)}";

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

            /*int actualAttackerMod = Attacker.Unit.getApplicableModifier(Defender.Unit, Attacker.Equipment);
            int actualDefenderMod = Defender.Unit.getApplicableModifier(Attacker.Unit, Defender.Equipment);
            string supportText = IsSupportingEngagement ? $" / {supportDivider} (support)" : "";

            //\r\ndebug: actual mods {actualAttackerMod}{supportText} vs {actualDefenderMod}*/
            return $"{attackerCrit}{attackerCritFail}{defenderCrit}{defenderCritFail}{Attacker.Unit} vs {Defender.Unit}\r\n1d100{attacker}{attackerSupportAccumulator}{vsString}{defender}{defenderSupportAccumulator}";
        }

        public string ExecuteSupport(Random random, out int supportResult)
        {
            resolved = true;

            StringBuilder result = new StringBuilder();

            // store the original roll, the original + mod, and then the original + mod / support
            int attackerRoll = random.Next(1, 100);
            int attackerPreSupportResult = attackerRoll + Attacker.Unit.getApplicableModifier(Defender.Unit, Attacker.Equipment);

            if (!Defender.Unit.Tags.Contains(Tag.WeakRearArmor))
            {
                supportResult = attackerPreSupportResult / supportDivider;
            } 
            else
            {
                supportResult = attackerPreSupportResult;
            }

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
            int attackerPreSupportResult = attackerRoll + Attacker.Unit.getApplicableModifier(Defender.Unit, Attacker.Equipment);
            int attackerPostSupportResult = attackerPreSupportResult;
            foreach (int supportingAttack in SupportingAttackers.Values)
            {
                attackerPostSupportResult += supportingAttack;
            }

            int defenderRoll = random.Next(1, 100);
            int defenderPreSupportResult = defenderRoll + Defender.Unit.getApplicableModifier(Attacker.Unit, Defender.Equipment);
            int defenderPostSupportResult = defenderPreSupportResult;
            foreach (int supportingAttack in SupportingDefenders.Values)
            {
                defenderPostSupportResult += supportingAttack;
            }

            result.AppendLine(DetailedBreakdown());
            result.AppendLine($"{attackerRoll} -> {attackerPreSupportResult} -> {attackerPostSupportResult} vs");
            result.AppendLine($"{defenderRoll} -> {defenderPreSupportResult} -> {defenderPostSupportResult}");

            // work out stuns: natural 1s, natural 100s, or if the roll goes "out of bounds" after modifiers
            if (attackerRoll == 1 || attackerPostSupportResult < 1 || defenderRoll == 100 || defenderPostSupportResult > 100)
            {
                Attacker.Unit.Tags.Add(Tag.StunPending);
                result.AppendLine($"{Attacker.Unit} stunned!");
            }

            if (defenderRoll == 1 || defenderPostSupportResult < 1 || attackerRoll == 100 || attackerPostSupportResult > 100)
            {
                Defender.Unit.Tags.Add(Tag.StunPending);
                result.AppendLine($"{Defender.Unit} stunned!");
            }

            // apply damage
            int damage = Math.Abs(attackerPostSupportResult - defenderPostSupportResult);
            // attacker wins
            if (attackerPostSupportResult > defenderPostSupportResult)
            {
                ProcessDamage(Attacker.Unit, Attacker.Equipment, Defender.Unit, damage, result, random);
            }
            // defender wins
            else if (attackerPostSupportResult < defenderPostSupportResult)
            {
                ProcessDamage(Defender.Unit, Defender.Equipment, Attacker.Unit, damage, result, random);
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

        private void ProcessDamage(Unit shooter, Equipment shooterWeapon, Unit victim, int damage, StringBuilder result, Random random)
        {
            // update XP; we learn more from failures
            shooter.GunneryXP++;
            victim.GunneryXP += 2;

            if (victim.Tags.Contains(Tag.Hologram) ||
                victim.Bunker?.Tags?.Contains(Tag.Hologram) == true)
            {
                int holoRoll = random.Next(1, 101);
                if (holoRoll <= 50)
                {
                    result.AppendLine("Target is using hologram; hologram hit and disappears; damage negated");
                    victim.Tags.Remove(Tag.Hologram);
                    victim.Bunker?.Tags?.Remove(Tag.Hologram);
                    return;
                }
            }

            // if a unit is bunkered up, the bunker absorbs the damage instead
            Boolean victimInBunker = victim.Bunker != null && !shooterWeapon.Effects.ContainsKey(Tag.BunkerClearance);
            Boolean bunkerClearance = victim.Bunker != null && shooterWeapon.Effects.ContainsKey(Tag.BunkerClearance);

            Unit effectiveVictim = victimInBunker ? victim.Bunker : victim;
            if (victimInBunker)
            {
                result.AppendLine($"{victim.Name} is sheltered in {effectiveVictim.Name};");
            }

            if (shooter.Tags.Contains(Tag.Stunned))
            {
                result.AppendLine($"{shooter.Name} stunned; no damage inflicted");
                return;
            }
            else if (shooterWeapon.Effects.ContainsKey(Tag.NoDamage))
            {
                result.AppendLine($"{shooter.Name} is unarmed; no damage inflicted");
            }
            else if (shooterWeapon.Effects.ContainsKey(Tag.Laser) && effectiveVictim.Tags.Contains(Tag.AblativePlating))
            {
                result.AppendLine($"Ablative plating on {effectiveVictim.Name} absorbs {damage}.");
            }
            else if (bunkerClearance)
            {
                result.AppendLine($"Bunker-clearance weapon employed; target eliminated");
                victim.Health = 0;
                victim.Tags.Add(Tag.DestructionPending);
            }
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
    }
}
