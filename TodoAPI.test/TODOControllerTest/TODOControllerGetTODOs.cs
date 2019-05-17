using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TODOAPI.Controllers;
using TODOBusiness.Interfaces;
using Xunit;

namespace TodoAPI.test.TODOControllerTest
{    
    public class TODOControllerGetTODOs
    {
        [Fact]
        public void GetTODOsCorrectFlow()
        {
            var mockBusiness = new Mock<ITodoBusiness>();
            mockBusiness.Setup(x => x.ListTODOs()).Returns(this.GetTODOs());

            TODOController todoController = new TODOController(mockBusiness.Object);
            var result = todoController.GetTodos();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<APIResponse>(viewResult.Value);
            Assert.Equal("OK", response.HttpResponse);
        }

        private IEnumerable<TODO> GetTODOs()
        {
            return new List<TODO>
            {
                new TODO
                {
                    IdTODO = 1,
                    Title = "Test",
                    Description = "Test",
                    Done = false,
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    State = true
                },
                new TODO
                {
                    IdTODO = 2,
                    Title = "Test",
                    Description = "Test",
                    Done = false,
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now,
                    State = true
                }
            };
        }
    }
}
