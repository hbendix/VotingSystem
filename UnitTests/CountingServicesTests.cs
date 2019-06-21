using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UnitTests.FakeRepositories;
using VotingSystemEntities;
using VotingSystemEntities.ViewModels;
using VotingSystemServices;
using VotingSystemServices.Interfaces;

namespace UnitTests
{
    [TestClass]
    public class CountingServicesTests
    {
        static FakePartyRepository fakePartyRepository = new FakePartyRepository();
        static FakeElectionRepository fakeElectionRepository = new FakeElectionRepository();
        static FakeVoteRepository fakeVoteRepository = new FakeVoteRepository();
        IVotingServices _voteServices = new VotingServices(fakeVoteRepository);
        ICountingServices _countingServices = new CountingServices(fakeElectionRepository, fakeVoteRepository, new PartyServices(fakePartyRepository));

        public Party GenerateFakeElectionWithVotes()
        {
            Election election = fakeElectionRepository.GenerateAdditionalElection();
            fakeElectionRepository.AddElection(election).Wait();

            Party fakeParty1 = new Party
            {
                PartyName = "Pink Party"
            };

            Party fakeParty2 = new Party
            {
                PartyName = "Orange Party"
            };
            fakeParty2.PartyId = 1;

            fakePartyRepository.AddParty(fakeParty1).Wait();
            fakePartyRepository.AddParty(fakeParty2).Wait();

            Area fakeArea = new Area
            {
                AreaName = "Portsmouth"
            };

            Candidate fakeCandidate1 = new Candidate
            {
                CandidateName = "Jack Smith",
                Party = fakeParty1,
                Area = fakeArea
            };

            Candidate fakeCandidate2 = new Candidate
            {
                CandidateName = "Alton Jones",
                Party = fakeParty2,
                Area = fakeArea
            };
            fakeCandidate2.CandidateId = 1;

            Vote vote1 = new Vote
            {
                AreaId = 0,
                Area = fakeArea,
                CandidateId = 1,
                Candidate = fakeCandidate2,
                ElectionId = 654321,
                Election = election,
                VoteId = 1
            };

            Vote vote2 = new Vote
            {
                AreaId = 0,
                Area = fakeArea,
                CandidateId = 0,
                Candidate = fakeCandidate1,
                ElectionId = 654321,
                Election = election,
                VoteId = 2
            };

            Vote vote3 = new Vote
            {
                AreaId = 0,
                Area = fakeArea,
                CandidateId = 1,
                Candidate = fakeCandidate2,
                ElectionId = 654321,
                Election = election,
                VoteId = 3
            };

            Vote spoiltVote = new Vote
            {
                AreaId = 0,
                Area = fakeArea,
                CandidateId = null,
                Candidate = null,
                ElectionId = 654321,
                Election = election,
                VoteId = 4
            };


            int vote1id = fakeVoteRepository.AddVote(vote1).Result;
            int vote2id = fakeVoteRepository.AddVote(vote2).Result;
            int vote3id = fakeVoteRepository.AddVote(vote3).Result;
            int vote4id = fakeVoteRepository.AddVote(spoiltVote).Result;

            return fakeParty2;
        }

        [TestMethod]
        public void CanGetElectionResult()
        {
            // Prepare
            Party expected = GenerateFakeElectionWithVotes();
            
            // Execute
            ResultsViewModel result = _countingServices.GetElectionResult(654321);

            // Assert
            Assert.AreEqual(result.SpoiltVoteCount, 1);
            result.LeadingParty.Should().BeEquivalentTo(expected);
        }
    }
}
