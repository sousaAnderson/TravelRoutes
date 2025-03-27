using System;
using System.Collections.Generic;
using TravelRoutes.Application.Interfaces;
using TravelRoutes.Domain.Abstractions;
using TravelRoutes.Domain.Entities;

namespace TravelRoutes.Application.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _repository;
        private Dictionary<string, List<(string, decimal)>> routesList = new();
        public RouteService(IRouteRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Add(Route entity)
        {
            return await _repository.Add(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<Route>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<string> GetBestRoute(string origin, string destination)
        {
            var routes = await _repository.GetAll();
            string bestRoute = "";
            foreach (var route in routes)
            {
                AddEdge(route.Origin, route.Destination, route.Price);
            }

            var (price, routeAndConecctions) = FindRoute(origin, destination);

            if (price != -1)
                bestRoute =  $"{string.Join(" - ", routeAndConecctions)} ao custo de R$ {price}";
            else
                bestRoute = $"Não há caminho de {origin} para {destination}.";

            return bestRoute;
        }

        public async Task<Route> GetRoute(int id)
        {
            return await _repository.GetRoute(id);
        }

        public async Task<bool> Update(Route entity)
        {
            return await _repository.Update(entity);
        }

        private void AddEdge(string origin, string destination, decimal cost)
        {
            if (!routesList.ContainsKey(origin))
                routesList[origin] = new List<(string, decimal)>();
            if (!routesList.ContainsKey(destination))
                routesList[destination] = new List<(string, decimal)>();

            routesList[origin].Add((destination, cost));
        }

        private (decimal cost, List<string> route) FindRoute(string origin, string destination)
        {
            var dist = new Dictionary<string, decimal>();
            var prev = new Dictionary<string, string>();
            var pq = new SortedSet<(decimal cost, string node)>(Comparer<(decimal, string)>.Create((a, b) =>
                a.Item1 == b.Item1 ? a.Item2.CompareTo(b.Item2) : a.Item1.CompareTo(b.Item1)));            

            foreach (var node in routesList.Keys)
                dist[node] = decimal.MaxValue;

            dist[origin] = 0;
            pq.Add((0, origin));

            while (pq.Any())
            {
                var (currentCost, currentNode) = pq.First();
                pq.Remove(pq.First());

                if (currentNode == destination)
                    break;

                if (!routesList.ContainsKey(currentNode)) continue;

                foreach (var (neighbor, cost) in routesList[currentNode])
                {
                    decimal newCost = currentCost + cost;
                    if (newCost < dist[neighbor])
                    {
                        pq.Remove((dist[neighbor], neighbor)); // Remove o antigo valor (se existir)
                        dist[neighbor] = newCost;
                        prev[neighbor] = currentNode;
                        pq.Add((newCost, neighbor));
                    }
                }
            }

            var route = new List<string>();
            for (var dest = destination; dest != null; dest = prev.GetValueOrDefault(dest))
                route.Add(dest);

            route.Reverse();
            return (dist[destination] == decimal.MaxValue ? -1 : dist[destination], route);
        }
    }
}
