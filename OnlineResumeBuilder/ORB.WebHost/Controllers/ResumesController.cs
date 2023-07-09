using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ORB.WebHost.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        public static readonly List<Resume> _resumes = new List<Resume>()
        {
            new Resume
            {   Id = 1,
                Name = "Ivan Peshev",
                Age = 25,
                Education = "Bachelor's Degree in Computer Science",
                Languages = new[] { "English","Bulgarian", "Korean"}
            },
            new Resume
            {   Id=2,
                Name ="Yoan Genchev",
                Age = 27,
                Education = "Bachelor's Degree in Neuroscience",
                Languages = new[] { "Bulgarian", "Dutch", "English"}
            }
        };

        // GET: /resumes
        [HttpGet]
        public ActionResult<IEnumerable<Resume>> GetResume()
        {
            List<Resume> resumes = _resumes.ToList();
            return resumes;
        }


        // GET: /resumes/2
        [HttpGet("{id}")]
        public ActionResult<Resume> GetResumeById(int id)
        {
            var resume = _resumes.Find(r => r.Id == id);
            if (resume == null)
            {
                return NotFound();
            }
            return Ok(resume);
        }

        //POST: /resumes
        [HttpPost]
        public ActionResult<Resume> PostResume(Resume resume)
        {
            //add additional validation
            resume.Id = _resumes.Count + 1;
            _resumes.Add(resume);
            return CreatedAtAction(nameof(GetResumeById),
                new {id = resume.Id}, resume);
        }

        //PUT: /resumes/2
        [HttpPut("{id}")]
        public IActionResult PutResume(int id, Resume updatedResume)
        {
            var existingResume = _resumes.Find(r => r.Id == id);
            if (existingResume == null)
            {
                return NotFound();
            }

            existingResume.Name = updatedResume.Name;
            existingResume.Age = updatedResume.Age;
            existingResume.Education = updatedResume.Education;
            existingResume.Languages = updatedResume.Languages;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Resume> DeleteResume(int id)
        {
            var resumeToRemove = _resumes.Find(r => r.Id == id);
            if (resumeToRemove == null)
            {
                return NotFound();
            }

            _resumes.Remove(resumeToRemove);
            return NoContent();
        }


        public class Resume
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }

            public string? Education { get; set; }

            public string[] Languages { get; set; }


        }
    }
}
