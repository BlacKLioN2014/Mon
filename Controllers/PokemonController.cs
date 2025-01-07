using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mon.Models.Dtos;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Mon.Models.Pokemon;

namespace Mon.Controllers
{
    public class PokemonController : Controller
    {

        [HttpGet]
        [Route("GetPokemon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<JsonResult> Pokemon()
        {
            var respuesta = string.Empty;
            var endpoint = "https://pokeapi.co/api/v2/pokemon/?offset=0&limit=500";
            var listaPokemon = new List<Pokemon>();
            var index = 0;

            try
            {

                do
                {

                    using (HttpClient client = new HttpClient())
                    {
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                        client.Timeout = TimeSpan.FromSeconds(20);

                        HttpResponseMessage response = await client.SendAsync(request);

                        if (response.IsSuccessStatusCode)
                        {
                            respuesta = await response.Content.ReadAsStringAsync();
                            var jsonObject = JObject.Parse(respuesta);

                            foreach (var item in jsonObject["results"])
                            {
                                index++;
                                var pokemon = new Pokemon()
                                {
                                    Id = index,
                                    Nombre = item["name"]?.ToString(),
                                    Url = item["url"]?.ToString()
                                };
                                listaPokemon.Add(pokemon);
                            }
                            endpoint = jsonObject["next"]?.ToString();
                        }
                        else
                        {
                            var errorMessage = response.Content.ReadAsStringAsync().Result;
                            endpoint = "";
                        }
                    }

                } while (endpoint != "");

                return new JsonResult(listaPokemon)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (HttpRequestException httpRequestEx)
            {
                respuesta = "HTTP Request Error: " + httpRequestEx.Message.ToString();
            }
            catch (TaskCanceledException taskCanceledEx)
            {
                respuesta = "Request Timeout: " + taskCanceledEx.Message.ToString();
            }
            catch (Exception ex)
            {
                respuesta = "General Error: " + ex.Message.ToString();
            }

            return new JsonResult("")
            {
                StatusCode = StatusCodes.Status500InternalServerError 
            };
        }

    }
}
