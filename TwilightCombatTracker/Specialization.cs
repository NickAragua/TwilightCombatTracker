using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwilightCombatTracker
{
    public class Specialization
    {
        public HashSet<Tag> Tags { get; protected set; }
        public int Modifier { get; set; }

        public Specialization()
        {
            Tags = new HashSet<Tag>();
        }

        public Specialization(List<Tag> tags, int mod)
        {
            Tags = new HashSet<Tag>();

            foreach(Tag tag in tags)
            {
                Tags.Add(tag);
            }

            Modifier = mod;
        }

        public override bool Equals(object obj)
        {
            Specialization other = obj as Specialization;
            return other != null &&
                other.Tags.SetEquals(Tags);
        }

        public override int GetHashCode()
        {
            return Tags.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Modifier > 0 ? "+" : "");
            sb.Append(Modifier);
            sb.Append(" vs (");

            foreach(Tag tag in Tags) {
                sb.Append(tag.ToString());
                sb.Append(", ");
            }

            if (Tags.Count > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }

            sb.Append(")");

            return sb.ToString();
        }
    }
}
