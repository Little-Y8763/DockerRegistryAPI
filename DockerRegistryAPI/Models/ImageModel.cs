using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DockerRegistryAPI.Models
{
    public class ImageModel
    {
        private static readonly string RegistryIp = "http://192.168.10.14:5000";

        public string Image { get; set; }
        public string Tag { get; set; }

        public string Name { get; set; }

        public string Architecture { get; set; }

        public string Os { get; set; }

        public string Created { get; set; }

        
        //public static List<ImageModel> GetAllimages()
        //{
        //    List<ImageModel> NameResult = new List<ImageModel>();
        //    List<ImageModel> TagResult = new List<ImageModel>();
        //    List<ImageModel> ArchitectureResult = new List<ImageModel>();

        //    var NameClient = new RestClient("http://192.168.253.134:5000/v2/_catalog");
        //    NameClient.Timeout = -1;
        //    var NameRequest = new RestRequest(Method.GET);
        //    IRestResponse NameResponse = NameClient.Execute(NameRequest);
        //    var NameImages = JsonConvert.DeserializeObject<DockerRegistryAPI.Controllers.HomeController.Root>(NameResponse.Content);

        //    foreach (var Image in NameImages.repositories)
        //    {
        //        NameResult.Add(new DockerRegistryAPI.Models.ImageModel
        //        {
        //            Image = Image
        //        });
        //    }

        //    for (int i = 0; i < NameResult.Count; i++)
        //    {
        //        var TagClient = new RestClient("http://192.168.253.134:5000/v2/" + NameResult[i].Image + "/tags/list");
        //        TagClient.Timeout = -1;
        //        var TagRequest = new RestRequest(Method.GET);
        //        IRestResponse TagResponse = TagClient.Execute(TagRequest);
        //        var TagImages = JsonConvert.DeserializeObject<DockerRegistryAPI.Controllers.HomeController.Root>(TagResponse.Content);
        //        foreach (var Tag in TagImages.tags)
        //        {
        //            TagResult.Add(new DockerRegistryAPI.Models.ImageModel
        //            {
        //                Name = TagImages.name,
        //                Tag = Tag
        //            });
        //        }
        //    }

        //    for (int j = 0; j < TagResult.Count; j++)
        //    {

        //        var ArchitectureClient = new RestClient("http://192.168.253.134:5000/v2/" + TagResult[j].Name + "/manifests/" + TagResult[j].Tag);
        //        ArchitectureClient.Timeout = -1;
        //        var ArchitectureRequest = new RestRequest(Method.GET);
        //        IRestResponse ArchitectureResponse = ArchitectureClient.Execute(ArchitectureRequest);
        //        var ArchitectureImages = JsonConvert.DeserializeObject<DockerRegistryAPI.Controllers.HomeController.Root>(ArchitectureResponse.Content);
        //        ArchitectureResult.Add(new DockerRegistryAPI.Models.ImageModel
        //        {
        //            Name = ArchitectureImages.name,
        //            Tag = ArchitectureImages.tag,
        //            Architecture = ArchitectureImages.architecture
        //        });
        //    }

        //    return ArchitectureResult;
        //}

        public static List<ImageModel> GetAllImages()
        {
            List<ImageModel> NameResult = new List<ImageModel>();

            var NameClient = new RestClient(RegistryIp + "/v2/_catalog");
            NameClient.Timeout = -1;
            var NameRequest = new RestRequest(Method.GET);
            IRestResponse NameResponse = NameClient.Execute(NameRequest);
            var NameImages = JsonConvert.DeserializeObject<DockerRegistryAPI.Controllers.HomeController.Root>(NameResponse.Content);

            foreach (var Image in NameImages.repositories)
            {
                NameResult.Add(new DockerRegistryAPI.Models.ImageModel
                {
                    Image = Image
                });
            }

            return NameResult;
        }

        public static List<ImageModel> GetAllTags(string Name)
        {
            List<ImageModel> TagResult = new List<ImageModel>();

            var TagClient = new RestClient(RegistryIp + "/v2/" + Name + "/tags/list");
            TagClient.Timeout = -1;
            var TagRequest = new RestRequest(Method.GET);
            IRestResponse TagResponse = TagClient.Execute(TagRequest);
            var TagImages = JsonConvert.DeserializeObject<DockerRegistryAPI.Controllers.HomeController.Root>(TagResponse.Content);
            foreach (var Tag in TagImages.tags)
            {
                TagResult.Add(new DockerRegistryAPI.Models.ImageModel
                {
                    Name = TagImages.name,
                    Tag = Tag
                });
            }

            return TagResult;
        }

        public static List<ImageModel> GetAllDetails(string Image,string tag)
        {
            List<ImageModel> TagResult = new List<ImageModel>();
            List<ImageModel> ArchitectureResult = new List<ImageModel>();

            var ArchitectureClient = new RestClient(RegistryIp + "/v2/" + Image + "/manifests/" + tag);
            ArchitectureClient.Timeout = -1;
            var ArchitectureRequest = new RestRequest(Method.GET);
            IRestResponse ArchitectureResponse = ArchitectureClient.Execute(ArchitectureRequest);
            var ArchitectureImages = JsonConvert.DeserializeObject<DockerRegistryAPI.Controllers.HomeController.Root>(ArchitectureResponse.Content);
            ArchitectureResult.Add(new DockerRegistryAPI.Models.ImageModel
            {
                //Os = ArchitectureImages.history.v1Compatibility.os,
                //Created = ArchitectureImages.history.v1Compatibility.created,
                Architecture = ArchitectureImages.architecture
            });

            return ArchitectureResult;
        }

    }
  
}
