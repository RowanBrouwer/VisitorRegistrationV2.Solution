using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Data.Services.Visitors
{
    public interface IVisitors
    {
        /// <summary>
        /// Gets all visitors from the database orderd by their ArrivalTime.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Visitor>> GetListOfVisitors();

        /// <summary>
        /// Adds new visitor to the database
        /// </summary>
        /// <param name="newVisitor">Is the new visitor that needs to be added.</param>
        /// <returns></returns>
        public Task<Visitor> AddVisitor(Visitor newVisitor);

        /// <summary>
        /// Deletes visitor from the database.
        /// </summary>
        /// <param name="id">Id of the visitor to delete.</param>
        /// <returns></returns>
        public Task DeleteVisitor(int id);

        /// <summary>
        /// Updates visitor on the database.
        /// </summary>
        /// <param name="updatedVisitor">Is the updated visitor info.</param>
        /// <returns></returns>
        public Task UpdateVisitor(Visitor updatedVisitor);

        /// <summary>
        /// Gets specific visitor based on their Id from the database.
        /// </summary>
        /// <param name="id">Is the Id of the visitor requested.</param>
        /// <returns></returns>
        public Task<Visitor> GetVisitorById(int id);

        /// <summary>
        /// Gets list of visitors based on full name and searchTerm.
        /// </summary>
        /// <param name="SearchTerm">String to filter the Fullname</param>
        /// <returns></returns>
        public Task<List<Visitor>> SearchVisitorsByName(string SearchTerm);
    }
}
