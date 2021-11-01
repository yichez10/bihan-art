using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bihan_Art.Models;
using System.Text.Json;
using Newtonsoft.Json;



namespace Bihan_Art.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string bannerURL = "lib/Content/bibi.jpg";
        private string mainContentURL = "lib/Content/bibi.jpg";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<String> imageURL = new List<string>();
            List<float> imageHeightWidthRatio = new List<float>();
            imageURL.Add(bannerURL);
            imageURL.Add(mainContentURL);
            foreach(String image in imageURL)
            {
                Stream stream = System.IO.File.OpenRead("wwwroot/"+image);
                Image sourceImage = Image.FromStream(stream, false, false);
                imageHeightWidthRatio.Add((float)sourceImage.Height / sourceImage.Width);
            }
            ViewData["imageURL"] = imageURL;
            ViewData["imageSize"] = imageHeightWidthRatio;

            var jsonText = System.IO.File.ReadAllText("wwwroot/lib/Content/cover.json");
            var imagelist = JsonConvert.DeserializeObject<Product>(jsonText);
            IList<string> s = new List<string>();
            foreach (var p in imagelist.Products)
            {
                string url = p.imgurl;
                s.Add(url);
            }
            ViewData["imagelist"] = s;

            return View("Index");
            // calc(~'50% * @imagesize[0]')
        }


        public IActionResult Illustration()
        {
            var jsonText = System.IO.File.ReadAllText("wwwroot/lib/Content/images.json");
            var imagelist = JsonConvert.DeserializeObject<Product>(jsonText);
            IList<string> s1 = new List<string>();
            IList<string> s2 = new List<string>();
            IList<string> s3 = new List<string>();
            int n = 0;
            foreach (var p in imagelist.Products)
            {
                if (n % 3 == 0)
                {
                    s1.Add(p.imgurl);
                }
                if (n % 3 == 1)
                {
                    s2.Add(p.imgurl);
                }
                if (n % 3 == 2)
                {
                    s3.Add(p.imgurl);
                }
                n++;
            }
            ViewData["imagelist1"] = s1;
            ViewData["imagelist2"] = s2;
            ViewData["imagelist3"] = s3;

            var jsonText2 = System.IO.File.ReadAllText("wwwroot/lib/Content/painting.json");
            var imagelist2 = JsonConvert.DeserializeObject<Product>(jsonText2);
            IList<string> s4 = new List<string>();
            IList<string> s5 = new List<string>();
            IList<string> s6 = new List<string>();
            int n2 = 0;
            foreach (var p in imagelist2.Products)
            {
                if (n2 % 3 == 0)
                {
                    s4.Add(p.imgurl);
                }
                if (n2 % 3 == 1)
                {
                    s5.Add(p.imgurl);
                }
                if (n2 % 3 == 2)
                {
                    s6.Add(p.imgurl);
                }
                n2++;
            }
            ViewData["painting1"] = s4;
            ViewData["painting2"] = s5;
            ViewData["painting3"] = s6;

            return View("Illustration");
        }

        public IActionResult MaxmizeImage(string imageurl, string page, string id)
        {
            ViewData["index"] = id;
            
            if (page == "Index") {
            var jsonText = System.IO.File.ReadAllText("wwwroot/lib/Content/cover.json");
            var imagelist = JsonConvert.DeserializeObject<Product>(jsonText);
            IList<string> s = new List<string>();
            foreach (var p in imagelist.Products)
            {
                string url = p.imgurl;
                s.Add(url);
            }
            ViewData["page"] = s;
                ViewData["return"] = "Index";
            }

            if(page == "Illustration")
            {
                var jsonText = System.IO.File.ReadAllText("wwwroot/lib/Content/images.json");
                var imagelist = JsonConvert.DeserializeObject<Product>(jsonText);
                IList<string> s = new List<string>();
                foreach (var p in imagelist.Products)
                {
                    string url = p.imgurl;
                    s.Add(url);
                }
                ViewData["page"] = s;
                ViewData["return"] = "Illustration";
            }

            if (page == "Painting")
            {
                var jsonText = System.IO.File.ReadAllText("wwwroot/lib/Content/painting.json");
                var imagelist = JsonConvert.DeserializeObject<Product>(jsonText);
                IList<string> s = new List<string>();
                foreach (var p in imagelist.Products)
                {
                    string url = p.imgurl;
                    s.Add(url);
                }
                ViewData["page"] = s;
                ViewData["return"] = "Illustration";
            }

            ViewData["ImageMax"] = imageurl;
            return View("MaxmizeImage");
        }
        public IActionResult About()
        {
            return View("About");
        }
        public IActionResult Contact()
        {
            return View("Contact");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
