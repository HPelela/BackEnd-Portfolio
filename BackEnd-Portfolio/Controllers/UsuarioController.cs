using BackEnd_Portfolio.Application;
using BackEnd_Portfolio.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisToDoList.API.Infrastructure.Caching;

namespace BackEnd_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly ICachingService _cache;

        public UsuarioController(ICachingService cache) 
        {
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario(UsuarioDTO usuario)
        {
            try
            {
                var retorno = await new UsuarioApplication(_cache).Salvar(usuario);
                return Ok(retorno);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> ExcluirUsuario(UsuarioDTO usuario)
        {
            try
            {
                //await new UsuarioApplication().Excluir(usuario);
                return Ok(usuario);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioDTO usuario)
        {
            try
            {
                //await new UsuarioApplication().Editar(usuario);
                return Ok(usuario);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarListaUsuario()
        {
            try
            {
                var retorno = await new UsuarioApplication(_cache).Buscar();
                return Ok(retorno);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
