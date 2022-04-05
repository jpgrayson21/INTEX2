using System;
using System.Linq;

namespace INTEX.Models
{
    public class EFCrashRepository : ICrashRepository
    {
        private CrashDbContext _context { get; set; }

        public EFCrashRepository(CrashDbContext temp)
        {
            _context = temp;
        }

        public IQueryable<Crash> Utah_Crashes => _context.Utah_Crashes;

        public void AddCrash(Crash c)
        {
            throw new NotImplementedException();
        }

        public void EditCrash(Crash c)
        {
            throw new NotImplementedException();
        }

        public void RemoveCrash(Crash c)
        {
            throw new NotImplementedException();
        }
    }
}
