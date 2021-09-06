using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DockerRegistryAPI.Models
{
    public class TestModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
