using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRoutes.Domain.Entities;

namespace TravelRoutes.Application.Interfaces
{
    public interface IRouteService
    {
        Task<IEnumerable<Route>> GetAll();
        Task<Route> GetRoute(int id);
        Task<int> Add(Route entity);
        Task<bool> Delete(int id);
        Task<bool> Update(Route entity);
        Task<string> GetBestRoute(string origin, string destination);
    }
}
