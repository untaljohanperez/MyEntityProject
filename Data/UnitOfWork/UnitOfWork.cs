using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using MyEntity.Data.Context;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyEntityContext Context;

        public IBillRepository Bills { get; private set; }

        public UnitOfWork(MyEntityContext context)
        {
            Context = context;
            Bills = new BillRepository(context);
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
