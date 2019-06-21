using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemRepositoryAccess.Interfaces;

namespace VotingSystemRepositoryAccess
{
    public class AreaRepository : IAreaRepository
    {
        private readonly VotingDbContext _context;

        public AreaRepository (VotingDbContext context)
        {
            _context = context;
        }
        public async Task AddArea(Area area)
        {
            _context.Add(area);

            await _context.SaveChangesAsync();
        }

        public Area GetArea(int areaId)
        {
            return _context.Areas
                .Where(x => x.Id == areaId)
                .FirstOrDefault();
        }

        public ICollection<Area> GetAllAreas()
        {
            return _context.Areas.Where(x => x.Dormant == false).ToList();
        }

        public async Task UpdateArea(Area area)
        {
            _context.Update(area);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteArea(int areaId)
        {
            var _toDelete = _context.Areas
                .Where(x => x.Id == areaId)
                .FirstOrDefault();

            _toDelete.Dormant = true;
            _context.Update(_toDelete);

            await _context.SaveChangesAsync();
        }
    }
}
