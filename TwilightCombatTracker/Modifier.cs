using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightCombatTracker
{
    public class Modifier
    {
        public Modifier(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public string Name;
        public int Value;

        public override string ToString()
        {
            string valueMod = Value >= 0 ? $"+{Value}" : $"{Value}";
            return $"{valueMod} ({Name})";
        }
    }
}
