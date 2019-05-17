﻿using System;
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

        [HttpGet]
        [Route("api/TODO")]
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

        [HttpGet]
        [Route("api/TODO/{idTodo}")]
        public IActionResult GetTodoById(int idTodo)
        {
            try
            {
                TODO todo = this.todoBusiness.GetTodoById(idTodo);

                if(todo == null)
                {
                    response = new APIResponse
                    {
                        HttpResponseNumber = (int)HttpStatusCode.NotFound,
                        HttpResponse = HttpStatusCode.NotFound.ToString(),
                        ErrorResponse = "The TODO hasn't found"
                    };
                }

                response = new APIResponse
                {
                    HttpResponseNumber = (int)HttpStatusCode.OK,
                    HttpResponse = HttpStatusCode.OK.ToString(),
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

        [HttpPost]
        [Route("api/TODO")]
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

        [HttpPut]
        [Route("api/TODO")]
        public IActionResult UpdateTODO(TODO todo)
        {
            try
            {
                if (todo == null)
                {
                    throw new ArgumentNullException("The todo is null", nameof(todo));
                }

                this.todoBusiness.UpdateTODO(todo);

                response = new APIResponse
                {
                    HttpResponseNumber = (int)HttpStatusCode.OK,
                    HttpResponse = HttpStatusCode.OK.ToString(),
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

        [HttpDelete]
        [Route("api/TODO")]
        public IActionResult DeleteTODO(int idTodo)
        {
            try
            {
                TODO todoDeleted = this.todoBusiness.GetTodoById(idTodo);
                this.todoBusiness.DeleteTODO(todoDeleted);

                response = new APIResponse
                {
                    HttpResponseNumber = (int)HttpStatusCode.OK,
                    HttpResponse = HttpStatusCode.OK.ToString(),
                    SuccessfullResponse = todoDeleted
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
    }
}
