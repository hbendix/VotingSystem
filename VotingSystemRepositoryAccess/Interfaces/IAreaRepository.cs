using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystemEntities;

namespace VotingSystemRepositoryAccess.Interfaces
{
    public interface IAreaRepository
    {
        Task AddArea(Area area);
        Area GetArea(int areaId);
        ICollection<Area> GetAllAreas();
        Task UpdateArea(Area area);
        Task DeleteArea(int areaId);
    }
}
