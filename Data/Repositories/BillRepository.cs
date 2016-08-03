using Data.Repositories;
using MyEntity.Data.Context;
using MyEntity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        public BillRepository(MyEntityContext context) : base(context)
        {

        }
        public IEnumerable<Bill> FindByCustomer(string customer)
        {
            return Context.Set<Bill>().Where(x => x.Customer == customer);
        }

        //public MyEntityContext MyEntityContext
        //{
        //    get { return Context as MyEntityContext; }
        //}
    }
}
