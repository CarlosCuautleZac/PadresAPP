using Newtonsoft.Json;
using PadresAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PadresAPP.Services
{
    public class UsuarioService
    {
        HttpClient client = new HttpClient();
        public event Action<string> Error;

        public UsuarioService()
        {
            client.BaseAddress = new Uri("https://padres.sistemas19.com/");
        }

        public async Task<Usuario> GetUsuario(string user, string password)
        {
            if(string.IsNullOrWhiteSpace(user)&&string.IsNullOrWhiteSpace(password)) 
            {
                Error?.Invoke("Nombre de usuario o contraseña vacío. " +
                    "Escriba los datos restantes e intente de nuevo.");
            }

            Usuario usuario = new Usuario()
            {
                NombreUsuario = user.Trim(),
                Password = password
            };

            var json = JsonConvert.SerializeObject(usuario);
            var response= client.PostAsync("api/usuario/login",new StringContent(json, Encoding.UTF8, "application/json"));
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                 json = await response.Result.Content.ReadAsStringAsync();
                 usuario = JsonConvert.DeserializeObject<Usuario>(json);
                return usuario;
            }
            else
            {
                var errores = await response.Result.Content.ReadAsStringAsync();
                Error?.Invoke(errores);
                return null;
            }

            


        }
    }
}
