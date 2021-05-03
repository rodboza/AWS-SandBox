using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAWSTools.Model
{
    [DynamoDBTable("Clima")]
    public class Clima
    {
        public String Cidade { get; set; }
        public DateTime DataColeta { get; set; }
        public decimal Temperatura { get; set; }
        public int Pressao { get; set; }
        public int Humidade { get; set; }
        public int Visibilidade { get; set; }
        public decimal VelocidadeVento { get; set; }
        public decimal DirecaoVento { get; set; }
        public DateTime NascerDoSol { get; set; }
        public DateTime PorDoSol { get; set; }

    }
}
