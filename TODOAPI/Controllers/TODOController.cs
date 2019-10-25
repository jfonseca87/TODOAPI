using System;
using System.Collections.Generic;
using System.Net;
using Domain;
using Microsoft.AspNetCore.Mvc;
using TODOBusiness.Interfaces;
using System.Linq;

namespace TODOAPI.Controllers
{
    [ApiController]
    public class TODOController : ControllerBase
    {
        private readonly ITodoBusiness todoBusiness;
        private APIResponse response;

        public TODOController(ITodoBusiness todoBusiness)
        {
            this.todoBusiness = todoBusiness;
        }

        [HttpGet("api/TODO")]
        public IActionResult GetTodos()
        {
            try
            {
                IEnumerable<TODO> todos = this.todoBusiness.ListTODOs().ToList();

                response = new APIResponse
                {
                    HttpResponseNumber = (int)HttpStatusCode.OK,
                    HttpResponse = HttpStatusCode.OK.ToString(),
                    SuccessfullResponse = todos
                };
            }
            catch (Exception ex)
            {
                response = new APIResponse
                {
                    HttpResponseNumber = (int)HttpStatusCode.InternalServerError,
                    HttpResponse = HttpStatusCode.InternalServerError.ToString(),
                    ErrorResponse = ex.Message.ToString()
                };
            }

            return Ok(response);
        }

        [HttpGet("api/TODO/{idTodo}")]
        public IActionResult GetTodoById(int idTodo)
        {
            try
            {
                TODO todo = this.todoBusiness.GetTodoById(idTodo);

                if (todo == null)
                {
                    response = new APIResponse
                    {
                        HttpResponseNumber = (int)HttpStatusCode.NotFound,
                        HttpResponse = HttpStatusCode.NotFound.ToString(),
                        ErrorResponse = "The TODO hasn't found"
                    };
                }
                else
                {
                    response = new APIResponse
                    {
                        HttpResponseNumber = (int)HttpStatusCode.OK,
                        HttpResponse = HttpStatusCode.OK.ToString(),
                        SuccessfullResponse = todo
                    };
                }
                
            }
            catch (Exception ex)
            {
                response = new APIResponse
                {
                    HttpResponseNumber = (int)HttpStatusCode.InternalServerError,
                    HttpResponse = HttpStatusCode.InternalServerError.ToString(),
                    ErrorResponse = ex.Message.ToString()
                };
            }

            return Ok(response);
        }

        [HttpPost("api/TODO")]
        public IActionResult CreateTODO(TODO todo)
        {
            try
            {
                if (todo == null)
                {
                    throw new ArgumentNullException("The todo is null", nameof(todo));
                }

                this.todoBusiness.CreateTODO(todo);

                response = new APIResponse
                {
                    HttpResponseNumber = (int)HttpStatusCode.Created,
                    HttpResponse = HttpStatusCode.Created.ToString(),
                    SuccessfullResponse = todo
                };
            }
            catch (Exception ex)
            {
                response = new APIResponse
                {
                    HttpResponseNumber = (int)HttpStatusCode.InternalServerError,
                    HttpResponse = HttpStatusCode.InternalServerError.ToString(),
                    ErrorResponse = ex.Message.ToString()
                };
            }

            return Ok(response);
        }

        [HttpPut("api/TODO")]
        public IActionResult UpdateTODO(TODO todo)
        {
            try
            {
                TODO todoToModify = this.todoBusiness.GetTodoById(todo != null ? todo.IdTODO : 0);

                if (todoToModify == null)
                {
                    response = new APIResponse
                    {
                        HttpResponseNumber = (int)HttpStatusCode.NotFound,
                        HttpResponse = HttpStatusCode.NotFound.ToString(),
                        ErrorResponse = "The TODO hasn't found"
                    };
                }
                else
                {
                    todoToModify.Title = todo.Title;
                    todoToModify.Description = todo.Description;
                    todoToModify.Done = todo.Done;
                    todoToModify.LastModificationDate = DateTime.Now;

                    this.todoBusiness.UpdateTODO(todoToModify);

                    response = new APIResponse
                    {
                        HttpResponseNumber = (int)HttpStatusCode.OK,
                        HttpResponse = HttpStatusCode.OK.ToString(),
                        SuccessfullResponse = todo
                    };
                }
            }
            catch (Exception ex)
            {
                response = new APIResponse
                {
                    HttpResponseNumber = (int)HttpStatusCode.InternalServerError,
                    HttpResponse = HttpStatusCode.InternalServerError.ToString(),
                    ErrorResponse = ex.Message.ToString()
                };
            }

            return Ok(response);
        }

        [HttpDelete("api/TODO")]
        public IActionResult DeleteTODO(int idTodo)
        {
            try
            {
                TODO todoDeleted = this.todoBusiness.GetTodoById(idTodo);

                if (todoDeleted == null)
                {
                    response = new APIResponse
                    {
                        HttpResponseNumber = (int)HttpStatusCode.NotFound,
                        HttpResponse = HttpStatusCode.NotFound.ToString(),
                        ErrorResponse = "The TODO hasn't found"
                    };
                }
                else
                {
                    this.todoBusiness.DeleteTODO(todoDeleted);

                    response = new APIResponse
                    {
                        HttpResponseNumber = (int)HttpStatusCode.OK,
                        HttpResponse = HttpStatusCode.OK.ToString(),
                        SuccessfullResponse = todoDeleted
                    };
                }
            }
            catch (Exception ex)
            {
                response = new APIResponse
                {
                    HttpResponseNumber = (int)HttpStatusCode.InternalServerError,
                    HttpResponse = HttpStatusCode.InternalServerError.ToString(),
                    ErrorResponse = ex.Message.ToString()
                };
            }

            return Ok(response);
        }
    }
}
