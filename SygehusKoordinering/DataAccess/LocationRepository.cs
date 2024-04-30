using SygehusKoordinering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.DataAccess
{
    public class LocationRepository : Repository, IEnumerable<Locations>
    {
        private List<Locations> list = new List<Locations>();

        public IEnumerator<Locations> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class AfdelingRepository : Repository, IEnumerable<Afdeling>
    {
        private List<Afdeling> list = new List<Afdeling>();

        public IEnumerator<Afdeling> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
