#region usings

using Shift.Domain.Core.Interfaces;
using Shift.Infra.Data.Context;

#endregion

namespace Shift.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ShiftContext _context;


        public UnitOfWork(ShiftContext context)
        {
            _context = context;
        }



        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }



        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
