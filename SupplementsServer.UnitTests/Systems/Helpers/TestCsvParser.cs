using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using SupplementsServer.API.Helpers.CsvParser;
using Xunit;

namespace SupplementsServer.UnitTests.Systems.Helpers; 

public class TestCsvParser {
    private string pathToTestFile = "../../../../data.csv";
    
    [Fact]
    public async Task? Parse_Returns_CsvResultList() {
        CsvParser parser = new CsvParser(pathToTestFile);
        var result = await parser.Parse();
        result.Should().BeOfType<List<CsvResult>>();
    }

    [Fact]
    public async Task? Parse_Returns_NotEmptyResults_OnValidFile() {
        StreamReader sr = new StreamReader(pathToTestFile);

        CsvParser parser = new CsvParser(pathToTestFile);
        var results = await parser.Parse();

        results.Count.Should().BeGreaterThan(0);
        
        results[0].Should().BeOfType<CsvResult>();
        results[0].GetKeys().Count.Should().BeGreaterThan(0);
        results[0].GetValue(results[0].GetKeys()[0]).Should().NotBeNull();
    }
    
    
}