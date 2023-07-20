using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using ORB.Data.Data;
using ORB.Data.Models.Resumes;
using ORB.Services.Contracts;
using ORB.Services.Implementations;
using ORB.Shared.Models.Resume;
using ORB.WebHost.Models;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;

namespace ORB.Services.Tests;

public class ResumeServiceTests
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly IResumeService resumeService;

    public ResumeServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        this.context = new ApplicationDbContext(options);

        this.mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }).CreateMapper();

        this.resumeService = new ResumeService(this.context, this.mapper);
    }

    [Fact]
    public async Task CreateResumeAsync_ValidInput_SavesResumeToDB()
    {
        // Arrange
        var resumeIM = new ResumeIM();
        var personaInfoId = "personainfoid";
        var userId = "userid";

        // Act
        _ = await this.resumeService.CreateResumeAsync(resumeIM, personaInfoId, userId);

        // Assert
        Assert.True(this.context.Resumes.ToList().Count == 1);
    }

    [Fact]
    public async Task DeleteResumeAsync_ValidInput_DeletesResume()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var resume = new Resume { Id = id };
        await this.context.Resumes.AddAsync(resume);
        await this.context.SaveChangesAsync();

        // Act 
        await this.resumeService.DeleteResumeAsync(id);

        // Assert
        Assert.True(this.context.Resumes.Find(id).IsDeleted);
    }

    [Fact]
    public async Task GetAllDeletedResumesForUserWithIdAsync_ValidInput_ReturnsListOfResumeVMs()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        await this.context.Resumes.AddAsync(new Resume { UserId = userId, IsDeleted = true });
        await this.context.SaveChangesAsync();

        // Act
        var result = await this.resumeService.GetAllDeletedResumesForUserWithIdAsync(userId);

        // Assert
        Assert.IsType<List<ResumeVM>>(result);
    }

    [Fact]
    public async Task GetResumeByIdAsync_ValidInput_ReturnsResumeVM()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var resume = new Resume { Id = id };
        await this.context.Resumes.AddAsync(resume);
        await this.context.SaveChangesAsync();

        // Act
        var result = await this.resumeService.GetResumeByIdAsync(id);

        // Assert
        Assert.IsType<ResumeVM>(result);
    }

    [Fact]
    public async Task UpdateResumeInfoWithIdAsync_ValidInput_ReturnsResumeVM()
    {
        // Arrange
        var resume = new Resume { Id = "id", Title = "oldtitle" };
        await this.context.Resumes.AddAsync(resume);
        await this.context.SaveChangesAsync();

        var newResumeInfo = new ResumeIM { Title = "newtitle", TemplateId = "templateid" };

        // Act
        var result = await this.resumeService.UpdateResumeInfoWithIdAsync("id", newResumeInfo);

        // Assert
        Assert.Equal(newResumeInfo.Title, result.Title);
    }
}
