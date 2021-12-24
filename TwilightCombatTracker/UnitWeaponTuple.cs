using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightCombatTracker
{
    public class UnitEquipmentTuple
    {
        public Unit Unit { get; set; }
        public Equipment Equipment { get; set; }

        public override string ToString()
        {
            return $"{Unit} ({Equipment})";
        }

        public override bool Equals(object obj)
        {
            return obj is UnitEquipmentTuple &&
                ((UnitEquipmentTuple) obj).Unit.Equals(Unit) &&
                ((UnitEquipmentTuple) obj).Equipment.Equals(Equipment);
        }
    }
}
