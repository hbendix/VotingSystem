using System;

namespace VotingSystemEntities
{
    public class AreaViewModel
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public bool Dormant { get; set; }

        public static AreaViewModel ToViewModel(Area area)
        {
            return new AreaViewModel
            {
                Id = area.Id,
                AreaName = area.AreaName,
                Dormant = area.Dormant
            };
        }

        public static Area ToDataModel(AreaViewModel area)
        {
            return new Area
            {
                Id = area.Id,
                AreaName = area.AreaName,
                Dormant = area.Dormant
            };
        }
    }
}
