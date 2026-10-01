/* Represents a single dining participant. Properties include string Name, int 
 * Weight (for unequal cost shares, e.g., double share = 2), and bool Included 
 * (whether they are active in this split).*/

namespace LunchSplit
{
    public class Attendee
    {
        public string Name { get; }
        public int Weight { get; }
        public bool Included { get; }

        public Attendee(string name, int weight, bool included)
        {
            Name = name;
            Weight = weight;
            Included = included;
        }
    }
}
