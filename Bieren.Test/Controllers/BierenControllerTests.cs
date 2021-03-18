using Bieren.MVC.Controllers;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Bieren.Test
{
    public class BierenControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfBier()
        {
            // Arrange
            var mockRepo = new Mock<IRepositoryManager>();
            mockRepo.Setup(repo => repo.Bier.GetAllAsync())
                .ReturnsAsync(GetTestSessions());
            var controller = new BierenController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IList<Bier>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count);
        }
        #region snippet_GetTestSessions
        private List<Bier> GetTestSessions()
        {
            var sessions = new List<Bier>();
            sessions.Add(new Bier()
            {
                BierNr = 1,
                Naam = "Test Bier 1",
                Alcohol = 5.5

            });
            sessions.Add(new Bier()
            {
                BierNr = 2,
                Naam = "Test Bier 2",
                Alcohol = 6.5
            });
            return sessions;
        }
        #endregion
    }
}
