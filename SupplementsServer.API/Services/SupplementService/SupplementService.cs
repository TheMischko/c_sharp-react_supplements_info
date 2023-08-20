using System.Globalization;
using Microsoft.VisualBasic.FileIO;
using SupplementsServer.API.Models;

namespace SupplementsServer.API.Services; 

public class SupplementService:ISupplementService {
    private const string SOURCE_FILE = "../data.csv";
    public async Task<List<Supplement>> GetAllSupplements() {
        TextFieldParser textParser = new TextFieldParser(SOURCE_FILE);
        textParser.TextFieldType = FieldType.Delimited;
        textParser.SetDelimiters(",");
        int id = 0;
        List<Supplement> supplements = new List<Supplement>();
        string[]? headers = textParser.ReadFields();
        while (!textParser.EndOfData) {
            string[]? fields = textParser.ReadFields();
            if(fields == null) continue;
            try {
                Supplement supplement = new () {
                    Id = id,
                    Name = fields[0],
                    AltName = fields[1],
                    EvidenceLevelScore = float.Parse(fields[2], CultureInfo.InvariantCulture),
                    ClaimedImprovement = fields[3],
                    Category = fields[4],
                    TestedExercise = fields[5],
                    HasOTW = String.IsNullOrEmpty(fields[6]),
                    Popularity = int.Parse(fields[7]),
                    NumStudies = int.Parse(fields[8]),
                    NumCitations = int.Parse(fields[9])
                };
                supplements.Add(supplement);
            }
            catch (Exception ex) {
                continue;
            }

            id++;
        }

        return supplements;
        
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
            }
        };
    }
}