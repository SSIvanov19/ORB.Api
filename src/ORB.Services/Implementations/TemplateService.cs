using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ORB.Data.Data;
using ORB.Data.Models.Resumes;
using ORB.Services.Contracts;
using ORB.Shared.Models.Templates;

namespace ORB.Services.Implementations;

    public class TemplateService : ITemplateService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TemplateService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TemplateVM>?> GetAllTemplatesAsync()
        {
            var templates = await this.context.Templates.ToListAsync();
            if (templates is null)
            {
            return null;
            }

            return this.mapper.Map<List<TemplateVM>>(templates);
        }

        /// <inheritdoc/>
        public async Task<TemplateVM?> FindTemplateByIdAsync(string id)
        {
            var template = await this.context.Templates.FindAsync(id);
            if (template is null)
            {
                return null;
            }

            return this.mapper.Map<TemplateVM>(template);
        }
    }
