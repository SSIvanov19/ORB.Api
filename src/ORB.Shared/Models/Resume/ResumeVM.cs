namespace ORB.Shared.Models.Resume;

/// <summary>
/// Represents a Resume View Model.
/// </summary>
public class ResumeVM
{
    /// <summary>
    /// Gets or sets the Id of the Resume.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the title of the Resume.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation time of the Resume.
    /// </summary>
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last modified time of the Resume.
    /// </summary>
    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the UserId of the User associated with the Resume.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the UserId of the User associated with the Resume.
    /// </summary>
    public string UserFullNames { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the PersonalInfoId of the PersonalInfo associated with the Resume.
    /// </summary>
    public string? PersonalInfoId { get; set; }

    /// <summary>
    /// Gets or sets the TemplateId of the Template associated with the Resume.
    /// </summary>
    public string? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Resume is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the list of Education models associated with the Resume.
    /// </summary>
    public List<string> EducationsIds { get; set; } = new ();

    /// <summary>
    /// Gets or sets the list of WorkExperience models associated with the Resume.
    /// </summary>
    public List<string> WorkExperienceIds { get; set; } = new ();
}
