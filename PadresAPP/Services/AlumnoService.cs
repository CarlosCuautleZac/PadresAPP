using Newtonsoft.Json;
using PadresAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadresAPP.Services
{
    public class AlumnoService
    {
        HttpClient client = new HttpClient();
        public event Action<string> Error;

        public AlumnoService()
        {
            client.BaseAddress = new Uri("https://padres.sistemas19.com/");
        }

        public async Task<List<Alumno>> Get(int idtutor)
        {
            List<Alumno> alumnos = new List<Alumno>();
            var response = client.GetAsync($"api/alumno/{idtutor}");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                var json = await response.Result.Content.ReadAsStringAsync();
                alumnos = JsonConvert.DeserializeObject<List<Alumno>>(json);
            }
            
            return alumnos;
        }

    }
}
