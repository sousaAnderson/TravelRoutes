using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using TravelRoutes.Domain.Abstractions;
using TravelRoutes.Domain.Entities;

namespace TravelRoutes.Infrastructure.Repository
{
    public class RouteRepository : IRouteRepository
    {
        private readonly IConfiguration _configuration;
        public RouteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> Add(Route entity)
        {
            using var connection = CreateConnection();
            var sql = $@"INSERT INTO Route (Origin, Destination, Price)
                    VALUES (@Origin, @Destination, @Price)";
            return await connection.ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM Route WHERE Id = @Id";

            var result = await connection.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }

        public async Task<IEnumerable<Route>> GetAll()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Route";
            return await connection.QueryAsync<Route>(sql);               
        }

        public async Task<Route> GetRoute(int id)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Route WHERE Id = @Id";
           return await connection.QueryFirstOrDefaultAsync<Route>(sql, new { Id = id });
                
        }

        public async Task<bool> Update(Route entity)
        {
            using var connection = CreateConnection();
            var sql = "UPDATE Route SET Origin = @Origin, Destination = @Destination, Price = @Price  WHERE Id = @Id";
            var result = await connection.ExecuteAsync(sql, entity);
            return result > 0;
        }
    }
}
