using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;
using SupplementsServer.API.Controllers;
using SupplementsServer.API.Models;
using SupplementsServer.API.Services;
using SupplementsServer.UnitTests.Fixtures;
using Xunit;

namespace SupplementsServer.UnitTests.Systems.Controllers;

public class TestSupplementsController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStausCode200()
    {
        // Arrange
        var mockSupplementService = new Mock<ISupplementService>();
        mockSupplementService
            .Setup(service => service.GetAllSupplements())
            .ReturnsAsync(SupplementsFixture.GetTestSupplements());
        SupplementsController supplementsController = new SupplementsController(mockSupplementService.Object);
        // Act
        var result = (OkObjectResult)await supplementsController.Get();
        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task? Get_OnSuccess_InvokesSupplementServiceOnce() {
        // Arrange
        var mockSupplementService = new Mock<ISupplementService>();
        mockSupplementService
            .Setup(service => service.GetAllSupplements())
            .ReturnsAsync(new List<Supplement>());
        
        SupplementsController supplementsController = new SupplementsController(mockSupplementService.Object);
        
        // Act
        var result = (ActionResult)await supplementsController.Get();
        
        // Assert
        mockSupplementService.Verify(
            service => service.GetAllSupplements(), 
            Times.Once());
         
    }
    
    [Fact]
    public async Task? Get_OnSuccess_ReturnsListOfUsers() {
        // Arrange
        var mockSupplementService = new Mock<ISupplementService>();
        mockSupplementService
            .Setup(service => service.GetAllSupplements())
            .ReturnsAsync(SupplementsFixture.GetTestSupplements());
        
        SupplementsController supplementsController = new SupplementsController(mockSupplementService.Object);
        
        // Act
        var result = (OkObjectResult)await supplementsController.Get();
        
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var resultObj = (OkObjectResult) result;
        resultObj.Value.Should().BeOfType<List<Supplement>>();

    }
    
    [Fact]
    public async Task? Get_OnNoUsersFound_Return404() {
        // Arrange
        var mockSupplementService = new Mock<ISupplementService>();
        mockSupplementService
            .Setup(service => service.GetAllSupplements())
            .ReturnsAsync(new List<Supplement>());
        
        SupplementsController supplementsController = new SupplementsController(mockSupplementService.Object);
        
        // Act
        var result = (NotFoundResult)await supplementsController.Get();
        
        // Assert
        result.Should().BeOfType<NotFoundResult>();
        NotFoundResult resultObj = (NotFoundResult) result;
        resultObj.StatusCode.Should().Be(404);
    }
    
    [Fact]
    public async Task? Find_OnNotFound_Return404() {
        // Arrange
        var mockSupplementService = new Mock<ISupplementService>();
        mockSupplementService
            .Setup(service => service.GetAllSupplements())
            .ReturnsAsync(new List<Supplement>());
        
        SupplementsController supplementsController = new SupplementsController(mockSupplementService.Object);
        
        // Act
        var result = (NotFoundResult)await supplementsController.GetFind("test");
        
        // Assert
        result.Should().BeOfType<NotFoundResult>();
        NotFoundResult resultObj = (NotFoundResult) result;
        resultObj.StatusCode.Should().Be(404);
    }
    
    [Fact]
    public async Task? Find_OnFound_ReturnSupplement() {
        // Arrange
        var mockSupplementService = new Mock<ISupplementService>();
        mockSupplementService
            .Setup(service => service.GetAllSupplements())
            .ReturnsAsync(SupplementsFixture.GetTestSupplements());
        
        SupplementsController supplementsController = new SupplementsController(mockSupplementService.Object);
        
        // Act
        var result = (OkObjectResult)await supplementsController.GetFind("test");
        
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        OkObjectResult resultObj = (OkObjectResult) result;
        resultObj.StatusCode.Should().Be(200);
    }
}