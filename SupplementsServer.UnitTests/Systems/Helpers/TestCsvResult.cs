using System.Threading.Tasks;
using FluentAssertions;
using SupplementsServer.API.Helpers.CsvParser;
using Xunit;

namespace SupplementsServer.UnitTests.Systems.Helpers; 

public class TestCsvResult {
    [Fact]
    public async Task? GetValue_Returns_NullOnNonExistingKey() {
        CsvResult csvResult = new CsvResult();
        var val = csvResult.GetValue("test");
        val.Should().Be(null);
    }

    [Fact]
    public async Task? GetValue_Returns_ObjectIfKeyExists() {
        CsvResult csvResult = new CsvResult();
        csvResult.AddValue("testKey", "testValue");
        var result = csvResult.GetValue("testKey");
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task? GetValue_Returns_StringIfIAddString() {
        CsvResult csvResult = new CsvResult();
        string testValue = "testValue";
        csvResult.AddValue("testKey", testValue);
        var result = (string) csvResult.GetValue("testKey");
        result.Should().BeOfType<string>();
        result.Should().Be(testValue);
    }
    
    [Fact]
    public async Task? GetValue_Returns_CsvResultIfIAddCsvResult() {
        CsvResult csvResult = new CsvResult();
        CsvResult testValue = new CsvResult();
        csvResult.AddValue("testKey", testValue);
        var result = (CsvResult) csvResult.GetValue("testKey");
        result.Should().BeOfType<CsvResult>();
    }

    [Fact]
    public async Task? GetKeys_Returns_KeysOfAddedValues() {
        string[] keys = new[] {"1", "2", "3", "test"};
        CsvResult csvResult = new CsvResult();
        foreach (string key in keys) {
            csvResult.AddValue(key, $"val-{key}");
        }

        var resultKeys = csvResult.GetKeys();
        foreach (string key in keys) {
            resultKeys.Contains(key).Should().BeTrue();
        }
    }

}