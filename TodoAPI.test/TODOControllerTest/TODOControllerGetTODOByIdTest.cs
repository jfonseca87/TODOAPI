using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TODOAPI.Controllers;
using TODOBusiness.Interfaces;
using Xunit;

namespace TodoAPI.test.TODOControllerTest
{
    public class TODOControllerGetTODOByIdTest
    {
        [Fact]
        public void GetTodoByIdCorrectFlow()
        {
            var mockBusiness = new Mock<ITodoBusiness>();
            mockBusiness.Setup(x => x.GetTodoById(It.IsAny<int>()))
                        .Returns(this.GetTODOMock());

            TODOController todoController = new TODOController(mockBusiness.Object);
            var result = todoController.GetTodoById(1);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<APIResponse>(viewResult.Value);
            Assert.Equal("OK", response.HttpResponse);
        }

        [Fact]
        public void GetTodoByIdInCorrectFlow()
        {
            var mockBusiness = new Mock<ITodoBusiness>();
            mockBusiness.Setup(x => x.GetTodoById(It.IsAny<int>()))
                        .Throws(new Exception());

            TODOController todoController = new TODOController(mockBusiness.Object);
            var result = todoController.GetTodoById(1);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<APIResponse>(viewResult.Value);
            Assert.Equal("InternalServerError", response.HttpResponse);
        }

        [Fact]
        public void GetTodoByIdNullResponse()
        {
            var mockBusiness = new Mock<ITodoBusiness>();
            mockBusiness.Setup(x => x.GetTodoById(It.IsAny<int>()))
                        .Returns((TODO)null);

            TODOController todoController = new TODOController(mockBusiness.Object);
            var result = todoController.GetTodoById(1);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<APIResponse>(viewResult.Value);
            Assert.Equal("NotFound", response.HttpResponse);
        }

        private TODO GetTODOMock()
        {
            return new TODO
            {
                IdTODO = 1,
                Title = "Test",
                Description = "Test",
                Done = false,
                CreationDate = DateTime.Now,
                LastModificationDate = DateTime.Now,
                State = true
            };
        }
    }
}
