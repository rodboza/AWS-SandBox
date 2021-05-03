using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SecretsManager.Model.Internal.MarshallTransformations;
using MyAWSTools.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MyAWSTools.Services
{
    public static class AwsDynamoDB
    {
        public static void Salvar(this Clima clima)
        {

            var client = new AmazonDynamoDBClient();
            var context = new DynamoDBContext(client);
            var x =  context.SaveAsync(clima);
            x.Wait();
        }

        public static Clima ToModelClima (this JsonElement climaJson)
        {
            var clima = new Clima();
            clima.Cidade = climaJson.GetProperty("name").ToString();
            clima.DataColeta = climaJson.GetProperty("dt").GetInt64().ToDateTime();
            clima.Temperatura = climaJson.GetProperty("main").GetProperty("temp").GetDecimal();
            clima.Pressao = climaJson.GetProperty("main").GetProperty("pressure").GetInt32();
            clima.Humidade = climaJson.GetProperty("main").GetProperty("humidity").GetInt32();
            clima.Visibilidade = climaJson.GetProperty("visibility").GetInt32();
            clima.VelocidadeVento = climaJson.GetProperty("wind").GetProperty("speed").GetDecimal();
            clima.DirecaoVento = climaJson.GetProperty("wind").GetProperty("deg").GetDecimal();
            clima.NascerDoSol = climaJson.GetProperty("sys").GetProperty("sunrise").GetInt64().ToDateTime();
            clima.PorDoSol = climaJson.GetProperty("sys").GetProperty("sunset").GetInt64().ToDateTime();

            return clima;
        }

        public static DateTime ToDateTime (this Int64 segundos)
        {
            return new DateTime(1970, 01, 01).AddSeconds(segundos);
        }
    }
}



