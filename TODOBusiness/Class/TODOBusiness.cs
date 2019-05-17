using System.Collections.Generic;
using Domain;
using TODOBusiness.Interfaces;
using TODORepository.Interfaces;

namespace TODOBusiness.Class
{
    public class TodoBusiness : ITodoBusiness
    {
        private readonly ITodoRepository todoRepository;

        public TodoBusiness(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public TODO CreateTODO(TODO todo)
        {
            return this.todoRepository.CreateTODO(todo);
        }

        public TODO DeleteTODO(TODO todo)
        {
            return this.todoRepository.DeleteTODO(todo);
        }

        public TODO GetTodoById(int idTodo)
        {
            return this.todoRepository.GetTodoById(idTodo);
        }

        public IEnumerable<TODO> ListTODOs()
        {
            return this.todoRepository.ListTODOs();
        }

        public TODO UpdateTODO(TODO todo)
        {
            return this.todoRepository.UpdateTODO(todo);
        }
    }
}
