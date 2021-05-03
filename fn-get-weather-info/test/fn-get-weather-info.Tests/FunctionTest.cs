using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using fn_get_weather_info;
using System.Text.Json;

namespace fn_get_weather_info.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            string input = "{\"local\": \"Santos\"}";
            
            var upperCase = function.FunctionHandler(JsonDocument.Parse(input).RootElement, context);

            Assert.Equal("OK", upperCase.ToString());
        }
    }
}
