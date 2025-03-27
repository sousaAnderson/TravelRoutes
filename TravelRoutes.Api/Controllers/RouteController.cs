using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelRoutes.Application.Interfaces;
using TravelRoutes.Domain.Entities;

namespace TravelRoutes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;
        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get que retorna todos as Rotas registradas no sistema.")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(IEnumerable<Domain.Entities.Route>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _routeService.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Erro interno no servidor. Contate o suporte.",
                    Detalhes = ex.Message 
                });
            }            
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get que retorna a Rota do Id informado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(Domain.Entities.Route))]
        public async Task<IActionResult> GetRoute(int id) 
        {
            try
            {
                var response = await _routeService.GetRoute(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Erro interno no servidor. Contate o suporte.",
                    Detalhes = ex.Message
                });
            }            
        }

        [HttpGet("GetBestRoute/{origin}/{destination}")]
        [SwaggerOperation(Summary = "Get que retorna a rota mais barata para determinado origem e destino.")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(string))]
        public async Task<IActionResult> GetBestRoute(string origin, string destination) 
        {
            try
            {
                var response = await _routeService.GetBestRoute(origin, destination);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Erro interno no servidor. Contate o suporte.",
                    Detalhes = ex.Message
                });
            }           
        }

        [HttpPost]
        [SwaggerOperation(Summary = "POST que adiciona uma nova rota.")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(int))]
        public async Task<IActionResult> PostAdd(Domain.Entities.Route route) 
        {
            try
            {
                var response = await _routeService.Add(route);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Erro interno no servidor. Contate o suporte.",
                    Detalhes = ex.Message
                });
            }            
        }

        [HttpPut]
        [SwaggerOperation(Summary = "PUT que atualiza uma rota.")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(int))]
        public async Task<IActionResult> Put(Domain.Entities.Route route) 
        {
            try
            {
                var response = await _routeService.Update(route);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Erro interno no servidor. Contate o suporte.",
                    Detalhes = ex.Message
                });
            }            
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Excluir uma rota.")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(int))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _routeService.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Erro interno no servidor. Contate o suporte.",
                    Detalhes = ex.Message
                });
            }            
        }
    }
}
