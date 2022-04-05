using System;
using System.Linq;

namespace INTEX.Models
{
    public interface ICrashRepository
    {
        IQueryable<Crash> Utah_Crashes { get; }
        IQueryable<Severity> Severity { get; }

        public void AddCrash(Crash c);
        public void EditCrash(Crash c);
        public void RemoveCrash(Crash c);
    }
}
