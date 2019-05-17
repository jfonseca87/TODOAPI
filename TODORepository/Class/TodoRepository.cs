using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using TODORepository.Interfaces;

namespace TODORepository.Class
{
    /// <summary>
    /// Class to manage the database operations
    /// </summary>
    public class TodoRepository : ITodoRepository
    {
        private readonly TODOContext db;

        public TodoRepository(TODOContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Method that create the TODO in the DataBase
        /// </summary>
        /// <param name="todo">Object that contains the TODO information</param>
        /// <returns>TODO created Object</returns>
        public TODO CreateTODO(TODO todo)
        {
            this.db.Add(todo);
            this.db.SaveChanges();
            return todo;
        }

        /// <summary>
        /// Method that update a existence TODO
        /// </summary>
        /// <param name="todo">Object that contains the TODO information</param>
        /// <returns>TODO Updated Object</returns>
        public TODO DeleteTODO(TODO todo)
        {
            todo.State = false;
            this.db.Entry(todo).State = EntityState.Modified;
            this.db.SaveChanges();
            return todo;
        }

        /// <summary>
        /// Method that delete the TODO from the DataBase
        /// </summary>
        /// <param name="IdTodo">Id number of TODO object to delete</param>
        /// <returns></returns>
        public TODO GetTodoById(int IdTodo)
        {
            return this.db.TODO.FirstOrDefault(x => x.IdTODO == IdTodo);
        }

        /// <summary>
        /// Method tha obtains All TODOs information from database
        /// </summary>
        /// <returns>Collection of TODOs</returns>
        public IEnumerable<TODO> ListTODOs()
        {
            return this.db.TODO;
        }

        /// <summary>
        /// Method that obtain the TODO filtering by Id
        /// </summary>
        /// <param name="IdTodo">Id number of TODO object to search</param>
        /// <returns>TODO Object result</returns>
        public TODO UpdateTODO(TODO todo)
        {
            TODO todoToModify = this.GetTodoById(todo.IdTODO);

            todoToModify.Title = todo.Title;
            todoToModify.Description = todo.Description;
            todoToModify.Done = todo.Done;
            todoToModify.LastModificationDate = DateTime.Now;

            this.db.Entry(todoToModify).State = EntityState.Modified;
            this.db.SaveChanges();
            return todoToModify;
        }
    }
}
