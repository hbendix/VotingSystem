using System;
using System.Collections.Generic;
using System.Text;
using VotingSystemEntities;
using VotingSystemEntities.ViewModels;

namespace VotingSystemServices.Interfaces
{
    public interface ICountingServices
    {
        ResultsViewModel GetElectionResult(int electionId);
    }
}
