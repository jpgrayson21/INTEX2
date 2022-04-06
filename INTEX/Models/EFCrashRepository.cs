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
        public IQueryable<Severity> Severity => _context.Severity;

        public void AddCrash(Crash c)
        {
            c = UpperStrings(c);
            _context.Utah_Crashes.Add(c);
            _context.SaveChanges();
        }

        public void EditCrash(Crash c)
        {
            c = UpperStrings(c);
            _context.Utah_Crashes.Update(c);
            _context.SaveChanges();
        }

        public void RemoveCrash(Crash c)
        {
            _context.Utah_Crashes.Remove(c);
            _context.SaveChanges();
        }

        public Crash UpperStrings(Crash c)
        {
            if (c.ROUTE != null)
            {
                c.ROUTE = c.ROUTE.ToUpper();
            }
            c.MAIN_ROAD_NAME = c.MAIN_ROAD_NAME.ToUpper();
            c.CITY = c.CITY.ToUpper();
            c.COUNTY_NAME = c.COUNTY_NAME.ToUpper();

            return c;
        }
    }
}
