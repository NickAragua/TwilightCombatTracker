using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwilightCombatTracker
{
    public class CombatManager
    {
        private List<Unit> BluForUnits = new List<Unit>();
        private List<Unit> OpForUnits = new List<Unit>();
        public List<Engagement> currentEngagements = new List<Engagement>();

        // the key is the hashcode of a Unit
        private Dictionary<int, int> unitEngagementCount = new Dictionary<int, int>();
        private Random random = new Random();
        public HashSet<Tag> globalBluTags = new HashSet<Tag>();
        public HashSet<Tag> globalRedTags = new HashSet<Tag>();
        private Dictionary<string, Unit> unitNameLookup = new Dictionary<string, Unit>();

        public List<Unit> JointList
        {
            get
            {
                List<Unit> jointList = new List<Unit>();
                jointList.AddRange(BluForUnits);
                jointList.AddRange(OpForUnits);
                return jointList;
            }
        }
        public CombatManager()
        {
        }

        public void AddUnit(Unit u, bool bluFor)
        {
            if (bluFor)
            {
                u.Tags.Remove(Tag.Nod);
                u.Tags.Add(Tag.GDI);
                BluForUnits.Add(u);
            }
            else
            {
                u.Tags.Remove(Tag.GDI);
                u.Tags.Add(Tag.Nod);
                OpForUnits.Add(u);
            }

            ApplyGlobalModifiers(u);
        }

        public void ReapplyGlobalModifiers()
        {
            foreach (Unit u in JointList)
            {
                ApplyGlobalModifiers(u);
            }
        }

        public void ApplyGlobalModifiers(Unit u)
        {
            u.ExternalTags.Clear();
            if (u.Tags.Contains(Tag.GDI))
            {
                foreach (Tag tag in globalBluTags)
                {
                    u.ExternalTags.Add(tag);
                }
            }

            if (u.Tags.Contains(Tag.Nod))
            {
                foreach (Tag tag in globalRedTags)
                {
                    u.ExternalTags.Add(tag);
                }
            }
        }

        public List<UnitEquipmentTuple> GetUnitsForCombatDisplay(bool blufor)
        {
            List<Unit> forceList = blufor ? BluForUnits : OpForUnits;
            List<UnitEquipmentTuple> unitEquipmentList = new List<UnitEquipmentTuple>();

            foreach (Unit unit in forceList)
            {
                foreach (Equipment weapon in unit.Weapons)
                {
                    UnitEquipmentTuple unitEquipmentTuple = new UnitEquipmentTuple();
                    unitEquipmentTuple.Unit = unit;
                    unitEquipmentTuple.Equipment = weapon;
                    unitEquipmentList.Add(unitEquipmentTuple);
                }
            }

            return unitEquipmentList.OrderBy(x => x.Unit.CurrentInit).ThenBy(x => x.Unit.Name).ToList();
        }

        public bool CanUnitAttack(UnitEquipmentTuple unit)
        {
            bool unitEngaged = unitEngagementCount.ContainsKey(unit.GetHashCode()) &&
                unitEngagementCount[unit.GetHashCode()] > 0;

            // a unit cannot attack if it is already engaged or
            // if it's destroyed/withdrawn/stunned/withdrawing
            return !unitEngaged && unit.Unit.IsActive() && 
                !unit.Unit.Tags.Contains(Tag.Stunned) && !unit.Unit.Tags.Contains(Tag.Withdrawing);
        }

        public Engagement CreateEngagement(UnitEquipmentTuple attacker, UnitEquipmentTuple defender, bool terminate = false)
        {
            if (!CanUnitAttack(attacker) && !CanUnitAttack(defender))
            {
                return null;
            }

            // if the attacker can't attack or the defender has a higher initiative, set the other unit
            // as the attacker by default
            if ((!CanUnitAttack(attacker) || defender.Unit.CurrentInit > attacker.Unit.CurrentInit) && !terminate)
            {
                // only flip the engagement once. If we can't create one for either side, just don't do it.
                return CreateEngagement(defender, attacker, true);
            }

            int supportDivider = Engagement.NO_SUPPORT;

            if (unitEngagementCount.ContainsKey(defender.GetHashCode()))
            {
                supportDivider = (int)Math.Pow(2, unitEngagementCount[defender.GetHashCode()]);
            }

            if (unitEngagementCount.ContainsKey(attacker.GetHashCode()))
            {
                supportDivider = (int)Math.Pow(2, unitEngagementCount[attacker.GetHashCode()]);
            }

            Engagement e = new Engagement(attacker, defender, supportDivider);
            return e;
        }

        public void CommitEngagement(Engagement engagement)
        {
            currentEngagements.Add(engagement);

            if (!unitEngagementCount.ContainsKey(engagement.Attacker.GetHashCode()))
            {
                unitEngagementCount.Add(engagement.Attacker.GetHashCode(), 0);
            }

            if (!unitEngagementCount.ContainsKey(engagement.Defender.GetHashCode()))
            {
                unitEngagementCount.Add(engagement.Defender.GetHashCode(), 0);
            }

            unitEngagementCount[engagement.Attacker.GetHashCode()]++;
            unitEngagementCount[engagement.Defender.GetHashCode()]++;

            engagement.Attacker.Unit.Tags.Add(Tag.EngagedThisTurn);
            engagement.Defender.Unit.Tags.Add(Tag.EngagedThisTurn);
        }

        public String RollInit()
        {
            StringBuilder accumulator = new StringBuilder();

            List<Unit> jointList = new List<Unit>();
            jointList.AddRange(BluForUnits);
            jointList.AddRange(OpForUnits);

            foreach (Unit unit in jointList)
            {
                int result = 0;
                bool critFail = false;
                bool critSuccess = false;

                if (unit.Tags.Contains(Tag.Destroyed) || unit.Tags.Contains(Tag.Withdrawn))
                {
                    unit.CurrentInit = -1;
                    continue;
                }

                if (unit.Speed <= 0)
                {
                    unit.CurrentInit = 0;
                    continue;
                }

                accumulator.AppendLine($"{unit} {unit.InitString()}");

                for (int x = 0; x < unit.Speed; x++)
                {
                    int dieRoll = random.Next(1, 100);
                    critFail |= dieRoll == 1;
                    critSuccess |= dieRoll == 100;

                    if (critFail)
                    {
                        unit.Tags.Add(Tag.InitCritFail);
                    }

                    if (critSuccess)
                    {
                        unit.Tags.Add(Tag.InitCrit);
                    }

                    if (x > 0 && x != unit.Speed)
                    {
                        accumulator.Append(" + ");
                    }

                    accumulator.Append(dieRoll);

                    if (dieRoll == 1 || dieRoll == 100)
                    {
                        accumulator.Append("(!)");
                    }

                    result += dieRoll;
                }

                accumulator.Append(unit.FixedInitModString());
                accumulator.Append($"; total {result}");
                accumulator.AppendLine();
                accumulator.AppendLine();
                unit.CurrentInit = result;
            }

            return accumulator.ToString();
        }

        public string RunEngagement(Engagement primary)
        {
            StringBuilder accumulator = new StringBuilder();

            // find all other supporting engagements
            foreach (Engagement other in currentEngagements)
            {
                if (other.Equals(primary))
                {
                    continue;
                }

                bool otherSupportsAttacker = other.Defender.Equals(primary.Defender);
                bool otherSupportsDefender = other.Defender.Equals(primary.Attacker);

                if (otherSupportsAttacker || otherSupportsDefender)
                {
                    int result;
                    accumulator.AppendLine(other.ExecuteSupport(random, out result));

                    if (otherSupportsAttacker)
                    {
                        primary.SupportingAttackers.Add(other.Attacker, result);
                    }
                    else if (otherSupportsDefender)
                    {
                        primary.SupportingDefenders.Add(other.Attacker, result);
                    }
                }
            }

            accumulator.AppendLine(primary.Execute(random));

            return accumulator.ToString();
        }

        public string EndOfRound(bool autoWithdraw)
        {
            currentEngagements.Clear();
            unitEngagementCount.Clear();

            List<Unit> jointList = new List<Unit>();
            jointList.AddRange(BluForUnits);
            jointList.AddRange(OpForUnits);

            StringBuilder accumulator = new StringBuilder();

            foreach (Unit unit in jointList)
            {
                // skip units that have been taken out
                if (!unit.IsActive())
                {
                    continue;
                }

                if (unit.Tags.Contains(Tag.DestructionPending))
                {
                    unit.Tags.Remove(Tag.DestructionPending);
                    unit.Tags.Add(Tag.Destroyed);
                    unit.Tags.Remove(Tag.StunPending);
                    unit.Tags.Remove(Tag.Stunned);
                    unit.Tags.Remove(Tag.Withdrawing);
                    unit.Tags.Remove(Tag.Withdrawn);
                    unit.Tags.Remove(Tag.EngagedThisTurn);
                }

                // resolve withdraws before removing stunned tag - stunned units cannot withdraw
                if (unit.Tags.Contains(Tag.Withdrawing) &&
                    !unit.Tags.Contains(Tag.EngagedThisTurn) &&
                    !unit.Tags.Contains(Tag.Stunned))
                {
                    unit.Tags.Remove(Tag.Withdrawing);
                    unit.Tags.Add(Tag.Withdrawn);
                    accumulator.Append($"{unit.Name} withdraws.");
                }

                if (unit.Tags.Contains(Tag.Stunned))
                {
                    unit.Tags.Remove(Tag.Stunned);
                    accumulator.Append($"{unit.Name} stun expires.");
                }

                if (unit.Tags.Contains(Tag.StunPending))
                {
                    unit.Tags.Remove(Tag.StunPending);
                    unit.Tags.Add(Tag.Stunned);
                }
                
                if (autoWithdraw && unit.Health <= 50 && unit.IsActive() && !unit.Tags.Contains(Tag.Structure))
                {
                    unit.Tags.Add(Tag.Withdrawing);
                }

                unit.Tags.Remove(Tag.InitCrit);
                unit.Tags.Remove(Tag.InitCritFail);
                unit.Tags.Remove(Tag.EngagedThisTurn);
                unit.CurrentInit = null;

                // check for bunker destruction/withdrawal, clear it out.
                if (unit.Bunker != null)
                {
                    if (!unit.Bunker.IsActive())
                    {
                        unit.Bunker = null;
                    }
                }
            }

            return accumulator.ToString();
        }

        public void DeleteUnit(Unit unit, bool bluFor)
        {
            if (bluFor)
            {
                BluForUnits.Remove(unit);
            } 
            else
            {
                OpForUnits.Remove(unit);
            }
        }

        public void DeleteEngagement(Engagement eng)
        {
            currentEngagements.Remove(eng);
            eng.Attacker.Unit.Tags.Remove(Tag.EngagedThisTurn);
            eng.Defender.Unit.Tags.Remove(Tag.EngagedThisTurn);
            
            if (unitEngagementCount.ContainsKey(eng.Attacker.GetHashCode()))
            {
                unitEngagementCount[eng.Attacker.GetHashCode()]--;
            }

            if (unitEngagementCount.ContainsKey(eng.Defender.GetHashCode()))
            {
                unitEngagementCount[eng.Defender.GetHashCode()]--;
            }
        }

        public string SerializeUnits(bool bluFor)
        {
            if (bluFor)
            {
                return JsonConvert.SerializeObject(BluForUnits);
            }
            else
            {
                return JsonConvert.SerializeObject(OpForUnits);
            }
        }

        public void DeserializeUnits(bool bluFor, string content, bool clearExisting)
        {
            List<Unit> units = JsonConvert.DeserializeObject<List<Unit>>(content);
            List<Unit> activeUnitList = bluFor ? BluForUnits : OpForUnits;

            if (clearExisting)
            {
                activeUnitList.Clear();
            } 
            
            activeUnitList.AddRange(units);

            foreach (Unit unit in activeUnitList)
            {
                unit.Tags.Add(bluFor ? Tag.GDI : Tag.Nod);

                if (!unitNameLookup.ContainsKey(unit.Name))
                {
                    unitNameLookup.Add(unit.Name, unit);
                }
            }

            foreach (Unit unit in JointList)
            {
                if (unit.Bunker != null && unitNameLookup.ContainsKey(unit.Bunker.Name))
                {
                    unit.Bunker = unitNameLookup[unit.Bunker.Name];
                }
            }

            ReapplyGlobalModifiers();
        }
    }
}
