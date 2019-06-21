using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystemEntities;

namespace VotingSystemServices.Interfaces
{
    public interface IAreaServices
    {
        Area GetAreaFromAddress(string address);
        ICollection<AreaViewModel> GetAllAreas();
        Task AddArea(AreaViewModel area);
        Task UpdateArea(AreaViewModel area);
        Task DeleteArea(int areaId);
    }
}
