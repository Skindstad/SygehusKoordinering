using SygehusKoordinering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.DataAccess
{
    public class BestiltRepository : Repository, IEnumerable<Bestilt>
    {
        private List<Bestilt> list = new List<Bestilt>();

        public IEnumerator<Bestilt> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class PrioritetRepository : Repository, IEnumerable<Prioritet>
    {
        private List<Prioritet> list = new List<Prioritet>();

        public IEnumerator<Prioritet> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class ProeveRepository : Repository, IEnumerable<Proeve>
    {
        private List<Proeve> list = new List<Proeve>();

        public IEnumerator<Proeve> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class SaerligeForholdRepository : Repository, IEnumerable<SaerligeForhold>
    {
        private List<SaerligeForhold> list = new List<SaerligeForhold>();

        public IEnumerator<SaerligeForhold> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
