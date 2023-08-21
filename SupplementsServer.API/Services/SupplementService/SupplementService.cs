using System.Globalization;
using Microsoft.VisualBasic.FileIO;
using SupplementsServer.API.Helpers.CsvParser;
using SupplementsServer.API.Models;

namespace SupplementsServer.API.Services; 

public class SupplementService:ISupplementService {
    private string source_file;

    public SupplementService(string source_file) {
        this.source_file = source_file;
    }
    public async Task<List<Supplement>> GetAllSupplements() {
        List<Supplement> supplements = new List<Supplement>();

        CsvParser csvParser = new CsvParser(source_file);
        List<CsvResult> results = await csvParser.Parse();

        for (int i = 0; i < results.Count; i++) {
            Supplement newSupplement = mapCsvResultToSupplement(results[i], i);
            supplements.Add(newSupplement);
        }

        return supplements;
    }

    private Supplement mapCsvResultToSupplement(CsvResult result, int index) {
        return new Supplement() {
            Id = index,
            Name = (string)result.GetValue("supplement"),
            AltName = (string)result.GetValue("alt name"),
            EvidenceLevelScore = float.Parse((string)result.GetValue("evidence level - score. 0 = no evidence, 1,2 = slight, 3 = conflicting , 4 = promising, 5 = good, 6 = strong"), CultureInfo.InvariantCulture),
            ClaimedImprovement = (string)result.GetValue("Claimed improved aspect of fitness"),
            Category = (string)result.GetValue("fitness category"),
            TestedExercise = (string)result.GetValue("sport or exercise type tested"),
            HasOTW = String.IsNullOrEmpty((string)result.GetValue("OTW")),
            Popularity = int.Parse((string)result.GetValue("popularity")),
            NumStudies = int.Parse((string)result.GetValue("number of studies examined")),
            NumCitations = int.Parse((string)result.GetValue("number of citations"))
        };
    }
}