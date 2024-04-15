using BackEnd_Portfolio.DTOS;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RedisToDoList.API.Infrastructure.Caching;

namespace BackEnd_Portfolio.Application
{
    public class UsuarioApplication
    {
        private readonly ICachingService _cache;

        public UsuarioApplication(ICachingService cache)
        {
            _cache = cache;
        }

        public async Task<IActionResult> Salvar(UsuarioDTO usuario)
        {
            try
            {
                var listaUsuario = await Buscar();

                if (listaUsuario != null && listaUsuario.Any(a => a.Name == usuario.Name))
                {
                    throw new Exception();
                }
                else if (listaUsuario != null && !listaUsuario.Any(a => a.Name == usuario.Name))
                {
                    listaUsuario.Add(usuario);
                    await _cache.SetAsync("ListaUsuario", JsonConvert.SerializeObject(listaUsuario, Formatting.Indented));
                }
                else
                {
                    var novaLista = new List<UsuarioDTO>
                    {
                        usuario
                    };

                    await _cache.SetAsync("ListaUsuario", JsonConvert.SerializeObject(novaLista, Formatting.Indented));
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IActionResult>  Excluir(string nomeUsuario)
        {
            var lista = await Buscar();
            var usuarioSalvo = lista.Where(a => a.Name == nomeUsuario).FirstOrDefault();

            if (usuarioSalvo != null)
            {
                lista.Remove(usuarioSalvo);
 
                await _cache.SetAsync("ListaUsuario", JsonConvert.SerializeObject(lista, Formatting.Indented));
            };
  
            return null;
        }

        public async Task<IActionResult> Editar(UsuarioDTO usuarioDTO)
        {
            var lista = await Buscar();
            var usuarioSalvo = lista.Where(a => a.Name == usuarioDTO.Name).FirstOrDefault();

            if (usuarioSalvo != null )
            {
                lista.Remove(usuarioSalvo);
                lista.Add(usuarioDTO);
                await _cache.SetAsync("ListaUsuario", JsonConvert.SerializeObject(lista, Formatting.Indented));
            }
            else
            {
                lista.Add(usuarioDTO);
                await _cache.SetAsync("ListaUsuario", JsonConvert.SerializeObject(lista, Formatting.Indented));
            }

            return null;
        }

        public async Task<List<UsuarioDTO>> Buscar()
        {
            var data = await _cache.GetAsync("ListaUsuario");

            if (!string.IsNullOrWhiteSpace(data))
            {
                return JsonConvert.DeserializeObject<List<UsuarioDTO>>(data);

            }
            return null;
        }

    }
}
