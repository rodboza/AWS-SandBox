using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using MyAWSTools.Model;
using MyAWSTools.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace fn_get_weather_info
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(JsonElement input, ILambdaContext context)
        {
            try
            {
                //pegando das variáveis de ambiente a Region defaul
                var regionAws = Environment.GetEnvironmentVariable("AWS_REGION");
                //requisitando ao AWS Secrets Manager a chave de acesso no site open weather
                string apiId = AwsSecret.GetSecret("openweather", regionAws).GetProperty("apiid").ToString();
                string local = input.GetProperty("local").ToString();
                // faz request no openweather
                var climaJson = getOpenWeather(apiId, local);
                climaJson.ToModelClima().Salvar();

            }
            catch (Exception e)
            { 
                context.Logger.LogLine($">>>>>>>>> inicio LOG");
                context.Logger.LogLine($">>>>>>>>> tipo {input.GetType().ToString()}");
                context.Logger.LogLine($">>>>>>>>> Conteudo {input.ToString()}");
                context.Logger.LogLine($">>>>>>>>> ENVIRONMENT VARIABLES: " + JsonSerializer.Serialize(System.Environment.GetEnvironmentVariables()));
                context.Logger.LogLine($">>>>>>>>> Contex: " + JsonSerializer.Serialize(context));
                context.Logger.LogLine($">>>>>>>>> Exception: " + JsonSerializer.Serialize(e.ToString()));
                context.Logger.LogLine($">>>>>>>>> Fim  LOG");

                throw;
            }
            return "OK";
        }

        private JsonElement getOpenWeather(string apiId, string local)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(@"https://api.openweathermap.org/data/2.5/weather");
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var parametros = $"?q={local}&appid={apiId}&units=metric&lang=pt_br";
                using (HttpResponseMessage response = client.GetAsync(parametros).Result)
                {

                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body.
                        return JsonDocument.Parse(response.Content.ReadAsStringAsync().Result).RootElement;
                    }
                    else
                    {
                        throw new Exception($"Erro ao acessar o site >>{client.BaseAddress}<< com os parametros >>{parametros}<< response  >>{ response.Content.ReadAsStringAsync()}<< ");
                    }
                }
            }

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
        }
    }
}
