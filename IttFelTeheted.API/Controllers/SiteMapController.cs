using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace IttFelTeheted.API.Controllers
{
    public class SiteMapController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), 
                "map", "itfhuSiteMap.xml"), "text/xml");
        }
    }
}