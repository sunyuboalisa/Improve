using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Improve.Module.Plan.Models
{
    public class Week : IEnumerable<DayOfWeek>
    {
        public IEnumerator<DayOfWeek> GetEnumerator()
        {
            for (int i = 0; i < 7; i++)
            {
                //yield return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), i.ToString());
                yield return (DayOfWeek)i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
