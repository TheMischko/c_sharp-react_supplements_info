using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using SupplementsServer.API.Models;
using SupplementsServer.API.Services;
using Xunit;

namespace SupplementsServer.UnitTests.Systems.Services; 

public class TestSupplementService {
    private const string TEST_DATA_PATH = "../../../../data.csv";
    [Fact]
    public async Task? GetAllSupplements_OnSuccess_ReturnsSupplementsList() {
        //Arrange
        SupplementService supplementService = new SupplementService(TEST_DATA_PATH);
        // Act
        List<Supplement> supplements = await supplementService.GetAllSupplements();
        //Assert
        supplements.Should().BeOfType<List<Supplement>>();
    }
    
    [Fact]
    public async Task? GetAllSupplements_OnReturn_ContainsAtLeastOneElement() {
        //Arrange
        SupplementService supplementService = new SupplementService(TEST_DATA_PATH);
        // Act
        List<Supplement> supplements = await supplementService.GetAllSupplements();
        //Assert
        supplements.Count.Should().BeGreaterThan(0);
    }
    
    
}