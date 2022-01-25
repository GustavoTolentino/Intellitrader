using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Mobile.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Mobile.Services
{
    internal class DataService
    {
        HttpClient client = new HttpClient();
        private string apiURI = "http://10.0.2.2:49154/api/usuario";
        /// <summary>
        /// Obtém os itens de produtos
        /// </summary>
        public async Task<List<UsuarioResponse>> GetUsersAsync()
        {
            var response = await client.GetStringAsync(apiURI);
            var usuarioResponses = JsonConvert.DeserializeObject<List<UsuarioResponse>>(response);
            
            return usuarioResponses;
        }
        public async Task<string> PostUserAsync(UsuarioRequest newUser)
        {
            var jsonContent = JsonConvert.SerializeObject(newUser);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new
            MediaTypeHeaderValue("application/json");
            await client.PostAsync(apiURI, contentString);
            return "";
        }
        public async Task<string> PutUserAsync(Guid id, UsuarioRequest newUser)
        {
            var jsonContent = JsonConvert.SerializeObject(newUser);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new
            MediaTypeHeaderValue("application/json");
            await client.PutAsync(apiURI + "/" + id.ToString(), contentString);
            return "";
        }
        public async Task<string> DeleteUsersAsync(Guid id)
        {
            var response = await client.DeleteAsync(apiURI + "/" + id.ToString());
            return "";
        }
        public async Task<UsuarioResponse> GetUserByIdAsync(Guid id)
        {
            var response = await client.GetAsync(apiURI + "/" + id.ToString());
            return JsonConvert.DeserializeObject<UsuarioResponse>(response.ToString());
        }


    }
}
