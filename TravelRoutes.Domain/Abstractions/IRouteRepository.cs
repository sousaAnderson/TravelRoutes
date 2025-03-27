using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRoutes.Domain.Entities;

namespace TravelRoutes.Domain.Abstractions
{
    public interface IRouteRepository 
    {
        Task<int> Add(Route route);
        Task<bool> Update(Route route);
        Task<bool> Delete(int id);
        Task<IEnumerable<Route>> GetAll();
        Task<Route> GetRoute(int id);
    }
}
