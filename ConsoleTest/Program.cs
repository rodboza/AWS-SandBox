using Amazon.Lambda.TestUtilities;
using fn_get_weather_info;
using System;
using System.Text.Json;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");



            var function = new Function();
            var context = new TestLambdaContext();
            string teste = "{\"local\": \"Santos\"}";

            var upperCase = function.FunctionHandler(JsonDocument.Parse(teste).RootElement, context);


            Console.WriteLine(upperCase);
            

        }
    }
}
