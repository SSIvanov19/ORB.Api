using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORB.Data.Data;
using ORB.Data.Models.Resumes;

namespace ORB.WebHost.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public TemplatesController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Template>>> GetTemplates()
        {
            if (_context.Templates == null)
            {
                return NotFound();
            }

            return await _context.Templates.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Template>> GetTemplateById(string id)
        {
            if (_context.Templates == null)
            {
                return NotFound();
            }
            var template = await _context.Templates.FindAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            return template;
        }


    }
}
