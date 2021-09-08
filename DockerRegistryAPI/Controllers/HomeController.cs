using DockerRegistryAPI.Models;
using ElectronNET.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DockerRegistryAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Electron.IpcMain.On("channelToServer", (args) =>
            {
                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                Electron.IpcMain.Send(mainWindow, "channelToClient", $"{args}, world!");
            });
            return View();
        }

        public IActionResult Images()
        {
            var result = Models.ImageModel.GetAllImages();
            return View(result);
        }
        [HttpGet("Home/images/tags")]
        public IActionResult Tags(string image)
        {
            var result = Models.ImageModel.GetAllTags(image);
            return View(result);
        }
        [HttpGet("Home/images/tags/details")]
        public IActionResult Details(string image ,string tag)
        {
            var result = Models.ImageModel.GetAllDetails(image, tag); 
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public class Root
        {
            public List<string> repositories { get; set; }
            public List<string> tags { get; set; }
            public int schemaVersion { get; set; }
            public string name { get; set; }
            public string tag { get; set; }
            public string architecture { get; set; }
            public List<FsLayer> fsLayers { get; set; }
            public List<History> history { get; set; }
            public List<Signature> signatures { get; set; }
        }
        public class FsLayer
        {
            public string blobSum { get; set; }
        }

        public class History
        {
            public string v1Compatibility { get; set; }
        }

        public class Jwk
        {
            public string crv { get; set; }
            public string kid { get; set; }
            public string kty { get; set; }
            public string x { get; set; }
            public string y { get; set; }
        }

        public class Header
        {
            public Jwk jwk { get; set; }
            public string alg { get; set; }
        }

        public class Signature
        {
            public Header header { get; set; }
            public string signature { get; set; }
            public string @protected { get; set; }
        }

    }
}
