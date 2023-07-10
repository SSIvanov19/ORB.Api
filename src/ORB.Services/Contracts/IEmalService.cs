// <copyright file="IEmalService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

namespace ORB.Services.Contracts;

/// <summary>
/// Interface of the email service.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Send email.
    /// </summary>
    /// <param name="emailRequest">Email Request.</param>
    /// <returns>Response with the result.</returns>
    Task SendEmailAsync(SendEmailRequest emailRequest);

    /// <summary>
    /// Request for sending an email.
    /// </summary>
    public class SendEmailRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendEmailRequest"/> class.
        /// </summary>
        /// <param name="email">Email to be sent to.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="message">Message of the email.</param>
        public SendEmailRequest(string email, string subject, string message)
        {
            this.Email = email;
            this.Subject = subject;
            this.Message = message;
        }

        /// <summary>
        /// Gets email to be sent to.
        /// </summary>
        public string Email { get; } = string.Empty; 

        /// <summary>
        /// Gets subject of the email.
        /// </summary>
        public string Subject { get; } = string.Empty;

        /// <summary>
        /// Gets message to be sent.
        /// </summary>
        public string Message { get; } = string.Empty;
    }
}
