using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibClass
{
    public class ComparerPlace : IComparer<Place>
    {
        public int Compare(Place? place, Place? place1)
        {
            if (place == null || place1 == null)
                throw new ArgumentNullException("Некорректное значение");
            return place.Name.CompareTo(place1.Name);
        }
    }
}
