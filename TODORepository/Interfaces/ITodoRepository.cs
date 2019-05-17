using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace TODORepository.Interfaces
{
    /// <summary>
    /// Interface tha define the database layer methods to realice maintanance to TODO DB
    /// </summary>
    public interface ITodoRepository
    {
        /// <summary>
        /// Method that create the TODO in the DataBase
        /// </summary>
        /// <param name="todo">Object that contains the TODO information</param>
        /// <returns>TODO created Object</returns>
        TODO CreateTODO(TODO todo);

        /// <summary>
        /// Method that update a existence TODO
        /// </summary>
        /// <param name="todo">Object that contains the TODO information</param>
        /// <returns>TODO Updated Object</returns>
        TODO UpdateTODO(TODO todo);

        /// <summary>
        /// Method that delete the TODO from the DataBase
        /// </summary>
        /// <param name="todo">TODO object to delete</param>
        /// <returns></returns>
        TODO DeleteTODO(TODO todo);

        /// <summary>
        /// Method tha obtains All TODOs information from database
        /// </summary>
        /// <returns>Collection of TODOs</returns>
        IEnumerable<TODO> ListTODOs();

        /// <summary>
        /// Method that obtain the TODO filtering by Id
        /// </summary>
        /// <param name="IdTodo">Id number of TODO object to search</param>
        /// <returns>TODO Object result</returns>
        TODO GetTodoById(int idTodo);
    }
}
