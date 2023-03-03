using Tutorial.Api.Controllers;
using Xunit;
using Tutorial.Api.Services;
using Moq;
using Tutorial.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace XUnit.Tests
{
    public class TutorialControllerTests
    {
        [Theory(DisplayName = "Get search by title return tutorials")]
        [InlineData(null)]
        [InlineData("Hello")]
        public async Task Get_ReturnTutorials(string value)
        {
            //  Arranage
            string queryTitle = value;
            string id = "63730beabd3cb05f2331be45";
            string title = "Hello";
            string description = "xxx";
            bool published = true;

            List<Tutorial.Api.Models.Tutorial> tutorials = new List<Tutorial.Api.Models.Tutorial>();
            
            tutorials.Add(new Tutorial.Api.Models.Tutorial{
                Id = "63730beabd3cb05f2331be45",
                Title = "Hello",
                Description = "xxx",
                Published = true,
            });

            //MOCK SERVICE
            var mockService = new Mock<ITutorialService>();
            mockService.Setup(service => service.GetAsync()).ReturnsAsync(tutorials);
            mockService.Setup(service => service.GetAyncByTitle(queryTitle)).ReturnsAsync(tutorials);

            //MOCK LOGGER
            var mockLogger = new Mock<ILogger<TutorialController>>();
        
            var controller = new TutorialController(mockLogger.Object,mockService.Object);

            // Act
            var result = await controller.Get(queryTitle);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            List<Tutorial.Api.Models.Tutorial> resultTutorials = Assert.IsType<List<Tutorial.Api.Models.Tutorial>>(okResult.Value);
            mockService.Verify();

            Assert.Equal(title,resultTutorials.LastOrDefault().Title);
            Assert.Equal(id,resultTutorials.LastOrDefault().Id);
            Assert.Equal(description,resultTutorials.LastOrDefault().Description);
            Assert.Equal(published,resultTutorials.LastOrDefault().Published);
        }

        [Fact]
        public async Task GetTutorialByIdReturnTutorial()
        {
            //  Arranage
            string id = "63730beabd3cb05f2331be45";
            string title = "Hello";
            string description = "xxx";
            bool published = true;

            Tutorial.Api.Models.Tutorial tutorial= new Tutorial.Api.Models.Tutorial{
                Id = "63730beabd3cb05f2331be45",
                Title = "Hello",
                Description = "xxx",
                Published = true
            };

            //MOCK SERVICE
            var mockService = new Mock<ITutorialService>();
            mockService.Setup(service => service.GetAsync(id)).ReturnsAsync(tutorial);
            //MOCK LOGGER
            var mockLogger = new Mock<ILogger<TutorialController>>();


            var controller = new TutorialController(mockLogger.Object,mockService.Object);

            // Act
            var result = await controller.GetTutorialById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Tutorial.Api.Models.Tutorial resultTutorial = Assert.IsType<Tutorial.Api.Models.Tutorial>(okResult.Value);
            mockService.Verify();

            Assert.Equal(title,resultTutorial.Title);
            Assert.Equal(id,resultTutorial.Id);
            Assert.Equal(description,resultTutorial.Description);
            Assert.Equal(published,resultTutorial.Published);   
        }

        [Fact]
        public async Task AddTutorialReturnSuccess()
        {
            //  Arranage
            Tutorial.Api.Models.Tutorial tutorial= new Tutorial.Api.Models.Tutorial{
                Title = "Hello",
                Description = "xxx"
            };

            //MOCK SERVICE
            var mockService = new Mock<ITutorialService>();
            mockService.Setup(service => service.CreateAsync(tutorial));
            //MOCK LOGGER
            var mockLogger = new Mock<ILogger<TutorialController>>();


            var controller = new TutorialController(mockLogger.Object,mockService.Object);

            // Act
            IActionResult result = await controller.AddTutorial(tutorial);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var response = new ReturnMessage { Code="200" , Message = "Inserted a single document Success"};

            ReturnMessage result_resp = Assert.IsType<ReturnMessage>(okResult.Value);
            mockService.Verify();

            Assert.Equal(response.Code,result_resp.Code);
            Assert.Equal(response.Message,result_resp.Message);
        }

        [Fact]
        public async Task UpdateTutorialReturnSuccess()
        {
            //  Arranage
            string id = "63730beabd3cb05f2331be45";
            
            Tutorial.Api.Models.Tutorial tutorial= new Tutorial.Api.Models.Tutorial{
                Id = id,
                Title = "Hello",
                Description = "xxx",
                Published = true,
            };

            //MOCK SERVICE
            var mockService = new Mock<ITutorialService>();
            mockService.Setup(service => service.GetAsync(id)).ReturnsAsync(tutorial);
            mockService.Setup(service => service.UpdateAsync(id,tutorial));

            //MOCK LOGGER
            var mockLogger = new Mock<ILogger<TutorialController>>();

            var controller = new TutorialController(mockLogger.Object,mockService.Object);

            // Act
            IActionResult result = await controller.UpdateTutorial(id,tutorial);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var response = new ReturnMessage { Code="200" , Message = "Updated a single document Success"};

            ReturnMessage result_resp = Assert.IsType<ReturnMessage>(okResult.Value);
            mockService.Verify();

            Assert.Equal(response.Code,result_resp.Code);
            Assert.Equal(response.Message,result_resp.Message);
        }

        [Fact]
        public async Task UpdateTutorialReturnNotFound()
        {
            //  Arranage
            string id = "63730beabd3cb05f2331be45";
            
            Tutorial.Api.Models.Tutorial tutorial= new Tutorial.Api.Models.Tutorial{
                Id = "63730beabd3cb05f2331be44",
                Title = "Hello",
                Description = "xxx",
                Published = true,
            };

            //MOCK SERVICE
            var mockService = new Mock<ITutorialService>();
            mockService.Setup(service => service.GetAsync(id));
            mockService.Setup(service => service.UpdateAsync(id,tutorial));

            //MOCK LOGGER
            var mockLogger = new Mock<ILogger<TutorialController>>();

            var controller = new TutorialController(mockLogger.Object,mockService.Object);

            // Act
            IActionResult result = await controller.UpdateTutorial(id,tutorial);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAll_ReturnSuccess()
        {
            //  Arranage

            //MOCK SERVICE
            var mockService = new Mock<ITutorialService>();
            mockService.Setup(service => service.RemoveAsync());
            //MOCK LOGGER
            var mockLogger = new Mock<ILogger<TutorialController>>();

            var controller = new TutorialController(mockLogger.Object,mockService.Object);

            // Act
            var result = await controller.DeleteAll();

             // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var response = new ReturnMessage { Code="200" , Message = "All deleted"};

            ReturnMessage result_resp = Assert.IsType<ReturnMessage>(okResult.Value);
            mockService.Verify();

            Assert.Equal(response.Code,result_resp.Code);
            Assert.Equal(response.Message,result_resp.Message);
        }

        [Fact]
        public async Task DeleteByIdReturnSuccess()
        {
            //  Arranage
            string id = "63730beabd3cb05f2331be45";
            
            Tutorial.Api.Models.Tutorial tutorial= new Tutorial.Api.Models.Tutorial{
                Id = id,
                Title = "Hello",
                Description = "xxx",
                Published = true,
            };

            //MOCK SERVICE
            var mockService = new Mock<ITutorialService>();
            mockService.Setup(service => service.GetAsync(id)).ReturnsAsync(tutorial);
            mockService.Setup(service => service.RemoveAsync(id));

            //MOCK LOGGER
            var mockLogger = new Mock<ILogger<TutorialController>>();

            var controller = new TutorialController(mockLogger.Object,mockService.Object);

            // Act
            IActionResult result = await controller.DeleteById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var response = new ReturnMessage { Code="200" , Message = "Deleted id "+id};

            ReturnMessage result_resp = Assert.IsType<ReturnMessage>(okResult.Value);
            mockService.Verify();

            Assert.Equal(response.Code,result_resp.Code);
            Assert.Equal(response.Message,result_resp.Message);
        }
    }
}