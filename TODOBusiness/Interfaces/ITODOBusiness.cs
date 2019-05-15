using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace TODOBusiness.Interfaces
{
    /// <summary>
    /// Interface tha define the business layer methods 
    /// </summary>
    public interface ITodoBusiness
    {
        /// <summary>
        /// Method to call the create method in repository layer
        /// </summary>
        /// <param name="todo">Object that contains the TODO information</param>
        /// <returns>TODO created Object</returns>
        TODO CreateTODO(TODO todo);

        /// <summary>
        /// Method to call the update method in repository layer
        /// </summary>
        /// <param name="todo">Object that contains the TODO information</param>
        /// <returns>TODO Updated Object</returns>
        TODO UpdateTODO(TODO todo);

        /// <summary>
        /// Method to call the delete method in repository layer
        /// </summary>
        /// <param name="IdTodo">Id number of TODO object to delete</param>
        /// <returns></returns>
        TODO DeleteTODO(int idTodo);

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
