using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemEntities.Enums;
using VotingSystemRepositoryAccess.Interfaces;

namespace UnitTests.FakeRepositories
{
    public class FakeAreaRepository: IAreaRepository
    {
        public IList<Area> _fakeRepository = new List<Area>(GenerateFakeArea());

        public static Area[] GenerateFakeArea()
        {
            Area newArea = new Area
            {
                AreaName = "Reddich"
            };

            newArea.Id = 123456;
            return new Area[] { newArea };
        }

        public Area GenerateAdditionalArea()
        {
            Area updatedArea = new Area
            {
                AreaName = "Portsmouth"
            };

            updatedArea.Id = 654321;
            return updatedArea;
        }

        public Area GenerateUpdatedArea()
        {
            Area updatedArea = new Area
            {
                AreaName = "Sheffield"
            };

            updatedArea.Id = 123456;
            return updatedArea;
        }

        public Area GetArea(int areaId)
        {
            return _fakeRepository.Where(x => x.Id.Equals(areaId)).Single();
        }

        public async Task AddArea(Area area)
        {
            _fakeRepository.Add(area);
        }

        public async Task UpdateArea(Area updatedArea)
        {
            int indexOfAreaToUpdate = _fakeRepository.IndexOf(GetArea(updatedArea.Id));
            _fakeRepository[indexOfAreaToUpdate] = updatedArea;
        }

        public async Task DeleteArea(int areaId)
        {
            Area areaToDelete = GetArea(areaId);
            areaToDelete.Dormant = true;
        }

        public ICollection<Area> GetAllAreas()
        {
            return _fakeRepository.Where(x => x.Dormant == false).ToList();
        }
    }
}
