using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Training.Entities.Models;
using Training.Repository.NUnit;
using Training.Service;
using Training.Service.Exceptions;
using Match = Training.Entities.Models.Match;

namespace Training.Tests.ServiceTests
{
    [TestFixture]

    public class MatchServiceTests : TestBase
    {
        private MatchService service;

        [SetUp]
        public override void Initialize()
        {
            base.Initialize();

            service = new MatchService(MockUnitOfWork.Object);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTeamsByLeague_Should_Throw_ArguementException_If_LeagueId_Does_Not_Exist()
        {
            // Arrange
            const int leagueId = 9;

            var mockTeamData = new List<Team> { new Team { Id = 1, LeagueId = 2 } };
            var mockLeagueData = new List<League> { new League { Id = 2, IsDeleted = false } };

            SetupMockRepository(mockTeamData);
            SetupMockRepository(mockLeagueData);

            // Act
            service.GetTeamsByLeague(leagueId);
        }

        [Test]
        public void GetTeamsByLeague_Should_Return_Correct_Teams_For_LeagueId()
        {
            // Arrange
            const int leagueId = 2;

            var mockTeamData = new List<Team>
            {
                new Team { Id = 1, LeagueId = 1 },
                new Team { Id = 2, LeagueId = 1 },
                new Team { Id = 3, LeagueId = 2 },
                new Team { Id = 4, LeagueId = 2 },
                new Team { Id = 5, LeagueId = 2 },
                new Team { Id = 6, LeagueId = 3 }
            };
            var mockLeagueData = new List<League>
            {
                new League { Id = 1, IsDeleted = false },
                new League { Id = 2, IsDeleted = false },
                new League { Id = 3, IsDeleted = false }
            };

            SetupMockRepository(mockTeamData);
            SetupMockRepository(mockLeagueData);

            // Act
            var actual = service.GetTeamsByLeague(leagueId).ToList();

            // Assert
            Assert.AreEqual(3, actual.Count, "actual.Count failed.");
            Assert.IsTrue(actual[0].LeagueId == 2, "actual[0] failed.");
            Assert.IsTrue(actual[1].LeagueId == 2, "actual[1] failed.");
            Assert.IsTrue(actual[2].LeagueId == 2, "actual[2] failed.");
        }

        [Test]
        public void GetTeamsByLeague_Should_Return_Empty_List_If_No_Teams_Associated_To_Existing_League()
        {
            // Arrange
            const int leagueId = 2;

            var mockTeamData = new List<Team>
            {
                new Team { Id = 1, LeagueId = 1 },
                new Team { Id = 2, LeagueId = 3 }
            };
            var mockLeagueData = new List<League>
            {
                new League { Id = 1, IsDeleted = false },
                new League { Id = 2, IsDeleted = false },
                new League { Id = 3, IsDeleted = false }
            };

            SetupMockRepository(mockTeamData);
            SetupMockRepository(mockLeagueData);

            // Act
            var actual = service.GetTeamsByLeague(leagueId);

            // Assert
            Assert.IsEmpty(actual, "actual is not empty, should be empty.");
            Assert.IsNotNull(actual, "actual is null, should not be null.");
        }

        [Test]
        public void InsertMatch_Should_Insert_And_SaveChanges_Only_Once()
        {
            // Arrange
            var mockLeagueData = new List<League> { new League { Id = 1, IsDeleted = false } };
            var mockTeamData = new List<Team>
            {
                new Team { Id = 1, LeagueId = 1 },
                new Team { Id = 2, LeagueId = 1 }
            };
            var match = new Match { HomeTeamId = 1, AwayTeamId = 2, HomeScore = 1, AwayScore = 0, MatchDateTime = new DateTime(2015, 10, 22, 09, 00, 00), LeagueId = 1 };

            // Act
            SetupMockRepository(mockLeagueData);
            SetupMockRepository(mockTeamData);
            SetupMockRepository<Match>(); // Need to set up the empty "table" of Match to insert into, else the insert will fail as there is no table to insert the row in.

            var actual = service.InsertMatch(match); // Insert (Add) following from the table creation above.

            // Assert
            MockSet<Match>().Verify(x => x.Add(match), Times.Once());
            // Can use VerifyInsert<Match>(Times.Once());

            // MockSet<Entities.Models.Match>().Verify(x => x.Add(It.IsAny<Match>()), Times.Once());
            // Using the commented code line above verifys whether ANY Match object has been added to the context but NOT specifically the Match object called 'match' as instantiated above, hence why I have chosen to use the uncommented out line.

            MockUnitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            // VerifySaveChanges(Times.Once());
            // Commented out code line above is part of TestBase - same as above but extracted out for ease of re-use. 
        }

        [Test]
        [ExpectedException(typeof(DuplicateMatchException))]
        public void InsertMatch_Should_Throw_DuplicateMatchException_If_Duplicate_Match_Already_Exists()
        {
            // Arrange
            var mockLeagueData = new List<League> { new League { Id = 1, IsDeleted = false } };
            var mockTeamData = new List<Team>
            {
                new Team { Id = 1, LeagueId = 1 },
                new Team { Id = 2, LeagueId = 1 }
            };
            var mockMatchData = new List<Match> { new Match { Id = 1, HomeTeamId = 1, HomeScore = 1, AwayTeamId = 2, AwayScore = 2, MatchDateTime = new DateTime(2015, 10, 22, 09, 00, 00) } };
            var duplicateMatchData = new Match { Id = 1, HomeTeamId = 1, HomeScore = 1, AwayTeamId = 2, AwayScore = 2, MatchDateTime = new DateTime(2015, 10, 22, 09, 00, 00) };

            SetupMockRepository(mockLeagueData);
            SetupMockRepository(mockTeamData);
            SetupMockRepository(mockMatchData);

            // Act
            var actual = service.InsertMatch(duplicateMatchData);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateMatch_Should_Throw_ArguementException_If_MatchId_Does_Not_Exist()
        {
            // Arrange
            var mockLeagueData = new List<League> { new League { Id = 1, IsDeleted = false } };
            var mockTeamData = new List<Team>
            {
                new Team { Id = 1, LeagueId = 1 },
                new Team { Id = 2, LeagueId = 1 }
            };
            var mockMatchData = new List<Match> { new Match { Id = 1, HomeTeamId = 1, HomeScore = 1, AwayTeamId = 2, AwayScore = 2, MatchDateTime = new DateTime(2015, 10, 26, 09, 00, 00), LeagueId = 1 } };
            var match = new Match { Id = 2, HomeTeamId = 2, HomeScore = 2, AwayTeamId = 1, AwayScore = 1, MatchDateTime = new DateTime(2015, 10, 26, 19, 00, 00), LeagueId = 1 };

            SetupMockRepository(mockLeagueData);
            SetupMockRepository(mockTeamData);
            SetupMockRepository(mockMatchData);

            // Act
            var actual = service.UpdateMatch(match);
        }

        // Update Match results should call save changes and update?
        [Test]
        public void UpdateMatch_Should_Update_Correct_Match_And_Call_SaveChanges_Only_Once()
        {
            // Arrange
            var mockLeagueData = new List<League>
            {
                new League { Id = 1, IsDeleted = false },
                new League { Id = 2, IsDeleted = false }
            };
            var mockTeamData = new List<Team>
            {
                new Team { Id = 1, LeagueId = 1 },
                new Team { Id = 2, LeagueId = 1 },
                new Team { Id = 3, LeagueId = 2 },
                new Team { Id = 4, LeagueId = 2 }
            };
            var mockMatchData = new List<Match> { new Match { Id = 1, HomeTeamId = 1, HomeScore = 1, AwayTeamId = 2, AwayScore = 2, MatchDateTime = new DateTime(2015, 10, 26, 09, 00, 00), LeagueId = 1 } };
            var match = new Match { Id = 1, HomeTeamId = 3, HomeScore = 4, AwayTeamId = 4, AwayScore = 5, MatchDateTime = new DateTime(2014, 09, 13, 08, 45, 30), LeagueId = 2 };

            SetupMockRepository(mockLeagueData);
            SetupMockRepository(mockTeamData);
            SetupMockRepository(mockMatchData);

            // Act
            var actual = service.UpdateMatch(match);

            // Assert 
            MockSet<Match>().Verify(x => x.Attach(It.IsAny<Match>()), Times.Once());
            // Use .Attach for update verification.

            MockUnitOfWork.Verify(x => x.SaveChanges(), Times.Once);

            // I want to call the object back from the mock database to confirm that the data has been updated.
            var updatedMatch = MockUnitOfWork.Object.Repository<Match>().Query(x => x.Id == 1).Select().First();

            Assert.AreEqual(3, updatedMatch.HomeTeamId);
            Assert.AreEqual(4, updatedMatch.HomeScore);
            Assert.AreEqual(4, updatedMatch.AwayTeamId);
            Assert.AreEqual(5, updatedMatch.AwayScore);
            Assert.AreEqual(new DateTime(2014, 09, 13, 08, 45, 30), updatedMatch.MatchDateTime);
            Assert.AreEqual(2, updatedMatch.LeagueId);
        }

        // TODO: List
        // Delete match deletes match
    }
}
