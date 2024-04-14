using BackEnd_Portfolio.DTOS;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BackEnd_Portfolio.Repository
{
    public class UsuarioRep
    {
        private readonly IDistributedCache _cache;

        public UsuarioRep(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentException(nameof(cache));
        }

        public async Task<UsuarioDTO> Salvar(UsuarioDTO usuarioDTO)
        {
            await _cache.SetStringAsync("1","teste");

            return usuarioDTO;
        }

        public async Task<UsuarioDTO> Buscar(string chave)
        {
            var data = await _cache.GetStringAsync(chave);

            if (string.IsNullOrEmpty(data)) return null;

            return JsonSerializer.Deserialize<UsuarioDTO>(data);
        }
    }
}
