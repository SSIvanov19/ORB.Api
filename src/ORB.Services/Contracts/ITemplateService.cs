using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORB.Data.Models.Resumes;
using ORB.Shared.Models.Templates;

namespace ORB.Services.Contracts;

    public interface ITemplateService
    {
        Task<IEnumerable<TemplateVM>?> GetAllTemplatesAsync();

        Task<TemplateVM?> FindTemplateByIdAsync(string id);
    }
