using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TODOAPI.Controllers;
using TODOBusiness.Interfaces;
using Xunit;

namespace TodoAPI.test.TODOControllerTest
{
    public class TODOControllerDeleteTODOTest
    {
        [Fact]
        public void DeleteTODOCorrectFlow()
        {
            var mockBusiness = new Mock<ITodoBusiness>();
            mockBusiness.Setup(x => x.GetTodoById(It.IsAny<int>()))
                        .Returns(this.GetTODOMock());

            mockBusiness.Setup(x => x.DeleteTODO(It.IsAny<TODO>()))
                        .Returns(this.GetTODOMock());

            TODOController todoController = new TODOController(mockBusiness.Object);
            var result = todoController.DeleteTODO(1);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<APIResponse>(viewResult.Value);
            Assert.Equal("OK", response.HttpResponse);
        }

        [Fact]
        public void DeleteTODOIncorrectFlow()
        {
            var mockBusiness = new Mock<ITodoBusiness>();
            mockBusiness.Setup(x => x.GetTodoById(It.IsAny<int>()))
                        .Returns(this.GetTODOMock());

            mockBusiness.Setup(x => x.DeleteTODO(It.IsAny<TODO>()))
                        .Throws(new Exception());

            TODOController todoController = new TODOController(mockBusiness.Object);
            var result = todoController.DeleteTODO(1);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<APIResponse>(viewResult.Value);
            Assert.Equal("InternalServerError", response.HttpResponse);
        }

        [Fact]
        public void DeleteTODONullObject()
        {
            var mockBusiness = new Mock<ITodoBusiness>();
            mockBusiness.Setup(x => x.DeleteTODO(It.IsAny<TODO>()))
                        .Returns((TODO)null);

            TODOController todoController = new TODOController(mockBusiness.Object);
            var result = todoController.DeleteTODO(1);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<APIResponse>(viewResult.Value);
            Assert.Equal("NotFound", response.HttpResponse);
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
