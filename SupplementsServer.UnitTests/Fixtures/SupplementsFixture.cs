using System.Collections.Generic;
using SupplementsServer.API.Models;

namespace SupplementsServer.UnitTests.Fixtures; 

static class SupplementsFixture {
    public static List<Supplement> GetTestSupplements() {
        return new List<Supplement>() {
            new Supplement() {
                Id = 1,
                Name = "test1",
                AltName = "1test",
                Category = "cat1",
                ClaimedImprovement = "none",
                EvidenceLevelScore = 1,
                HasOTW = true,
                NumCitations = 10,
                NumStudies = 10,
                Popularity = 10
            },
            new Supplement() {
                Id = 2,
                Name = "test2",
                AltName = "2test",
                Category = "cat1",
                ClaimedImprovement = "strength",
                EvidenceLevelScore = 5,
                HasOTW = false,
                NumCitations = 12,
                NumStudies = 3,
                Popularity = 9
            },
            new Supplement() {
                Id = 3,
                Name = "test3",
                AltName = "3test",
                Category = "cat2",
                ClaimedImprovement = "endurance",
                EvidenceLevelScore = 2,
                HasOTW = false,
                NumCitations = 5,
                NumStudies = 2,
                Popularity = 4
            },
        };
    }
}