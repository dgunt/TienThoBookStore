using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Infrastructure.Contexts;

namespace TienThoBookStore.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly TienThoBookStoreDbContext _context;
        public UnitOfWork(TienThoBookStoreDbContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
            _context.Dispose();
        }

        public async Task<int> SaveChangeAsync()
        {
            //throw new NotImplementedException();
            return await _context.SaveChangesAsync();
        }
    }
}
