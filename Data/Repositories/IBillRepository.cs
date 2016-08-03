using MyEntity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IBillRepository : IRepository<Bill>
    {
        IEnumerable<Bill> FindByCustomer(string customer);
    }
}
