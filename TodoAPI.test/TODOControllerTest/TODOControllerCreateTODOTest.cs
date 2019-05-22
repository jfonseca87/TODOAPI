using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TODOAPI.Controllers;
using TODOBusiness.Interfaces;
using Xunit;

namespace TodoAPI.test.TODOControllerTest
{
    public class TODOControllerCreateTODOTest
    {
        [Fact]
        public void CreateTODOCorrectFlow()
        {
            var mockBusiness = new Mock<ITodoBusiness>();
            mockBusiness.Setup(x => x.CreateTODO(It.IsAny<TODO>()))
                        .Returns(this.GetTODOMock());

            TODOController todoController = new TODOController(mockBusiness.Object);
            var result = todoController.CreateTODO(this.GetTODOMock());

            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<APIResponse>(viewResult.Value);
            Assert.Equal("Created", response.HttpResponse);
        }

        [Fact]
        public void CreateTODOIncorrectFlow()
        {
            var mockBusiness = new Mock<ITodoBusiness>();
            mockBusiness.Setup(x => x.CreateTODO(It.IsAny<TODO>()))
                        .Throws(new Exception());

            TODOController todoController = new TODOController(mockBusiness.Object);
            var result = todoController.CreateTODO(this.GetTODOMock());

            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<APIResponse>(viewResult.Value);
            Assert.Equal("InternalServerError", response.HttpResponse);
        }

        [Fact]
        public void CreateTODONullParameter()
        {
            var mockBusiness = new Mock<ITodoBusiness>();
            mockBusiness.Setup(x => x.CreateTODO(It.IsAny<TODO>()))
                        .Throws(new Exception());

            TODOController todoController = new TODOController(mockBusiness.Object);
            var result = todoController.CreateTODO(null);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<APIResponse>(viewResult.Value);
            Assert.Equal("InternalServerError", response.HttpResponse);
        }

        public TODO GetTODOMock()
        {
            return new TODO
            {
                IdTODO = 1,
                Title = "Test",
                Description = "Test",
                Done = false,
                CreationDate = DateTime.Now,
                LastModificationDate = DateTime.Now,
                State = false
            };
        }
    }
}
