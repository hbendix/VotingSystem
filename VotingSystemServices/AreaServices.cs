using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemRepositoryAccess.Interfaces;
using VotingSystemServices.Interfaces;

namespace VotingSystemServices
{
    public class AreaServices : IAreaServices
    {
        private IAreaRepository _areaRepository;

        public AreaServices(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task AddArea(AreaViewModel area)
        {
            await _areaRepository.AddArea(AreaViewModel.ToDataModel(area));
        }

        public async Task DeleteArea(int areaId)
        {
            await _areaRepository.DeleteArea(areaId);
        }

        public ICollection<AreaViewModel> GetAllAreas()
        {
            ICollection<Area> areas = _areaRepository.GetAllAreas();
            return areas.Select(x => AreaViewModel.ToViewModel(x)).ToList();
        }

        public Area GetAreaFromAddress(string address)
        {
            return _areaRepository.GetArea(1);
        }

        public async Task UpdateArea(AreaViewModel area)
        {
            await _areaRepository.UpdateArea(AreaViewModel.ToDataModel(area));
        }
    }
}
