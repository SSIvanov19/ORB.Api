using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ORB.WebHost.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {

        public static readonly List<Template> templates = new List<Template>()
        {
            new Template
            {
                Id = 1
            },
            new Template
            {
                Id = 2
            }
        };

        [HttpGet]

        public ActionResult<IEnumerable<Template>> GetTemplates()
        {
            return templates.ToList();
        }

        public class Template
        {
            public int Id { get; set; }
        }


    }
}
