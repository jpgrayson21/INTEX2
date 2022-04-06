using System;
namespace INTEX.Models
{
    public class Filter
    {
        public string MAIN_ROAD_NAME { get; set; }
        public string CRASH_DATETIME { get; set; }
        public bool CRASH_SEVERITY_1 { get; set; }
        public bool CRASH_SEVERITY_2 { get; set; }
        public bool CRASH_SEVERITY_3 { get; set; }
        public bool CRASH_SEVERITY_4 { get; set; }
        public bool CRASH_SEVERITY_5 { get; set; }
        public bool WORK_ZONE_RELATED { get; set; }
        public bool PEDESTRIAN_INVOLVED { get; set; }
        public bool BICYCLIST_INVOLVED { get; set; }
        public bool MOTORCYCLE_INVOLVED { get; set; }
        public bool IMPROPER_RESTRAINT { get; set; }
        public bool UNRESTRAINED { get; set; }
        public bool DUI { get; set; }
        public bool INTERSECTION_RELATED { get; set; }
        public bool WILD_ANIMAL_RELATED { get; set; }
        public bool DOMESTIC_ANIMAL_RELATED { get; set; }
        public bool OVERTURN_ROLLOVER { get; set; } 
        public bool COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; } 
        public bool TEENAGE_DRIVER_INVOLVED { get; set; }
        public bool OLDER_DRIVER_INVOLVED { get; set; }
        public bool NIGHT_DARK_CONDITION { get; set; }
        public bool SINGLE_VEHICLE { get; set; }
        public bool DISTRACTED_DRIVING { get; set; }
        public bool DROWSY_DRIVING { get; set; }
        public bool ROADWAY_DEPARTURE { get; set; }
    }
}
