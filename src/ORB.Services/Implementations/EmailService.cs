// <copyright file="EmailService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using Microsoft.Extensions.Configuration;
using ORB.Services.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;
using static ORB.Services.Contracts.IEmailService;

namespace ORB.Services.Implementations;

/// <summary>
/// Class that implements <see cref="IEmailService"/>.
/// </summary>
internal class EmailService : IEmailService
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailService"/> class.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    public EmailService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <inheritdoc/>
    public async Task SendEmailAsync(SendEmailRequest emailRequest)
    {
        var client = new SendGridClient(this.configuration["SendGrid:APIKey"]);
        var sendGridMessage = new SendGridMessage()
        {
            From = new EmailAddress(this.configuration["SendGrid:Email"], this.configuration["SendGrid:Name"]),
            Subject = emailRequest.Subject,
            PlainTextContent = emailRequest.Message,
            HtmlContent = emailRequest.Message,
        };

        if (!string.IsNullOrEmpty(emailRequest.FileName))
        {
            sendGridMessage.AddAttachment(emailRequest.FileName, emailRequest.FileContent);
        }

        sendGridMessage.AddTo(new EmailAddress(emailRequest.Email));
        await client.SendEmailAsync(sendGridMessage);
    }
}