// <copyright file="InitApp.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using ORB.Data.Models.Resumes;
using ORB.Services.Contracts;
using ORB.Shared.Models.Templates;

namespace ORB.WebHost.Helpers;

/// <summary>
/// A class for initializing the application.
/// </summary>
public static class InitApp
{
    /// <summary>
    /// Initializes the application asynchronously.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task InitAppAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var templateService = scope.ServiceProvider.GetRequiredService<ITemplateService>();

        var templates = new List<TemplateIM>
        {
            new TemplateIM
            {
                Name = "Blue Lagoon",
                Content = """
                <html>
                <head>
                <style>
                body {
                 margin: 0;
                }
                .resume0 > *{
                    font-family: Arial, sans-serif;
                    background-color: #F5F5F5;
                    color: #A1CCD1;
                    margin: 0;
                    padding: 0;
                    text-align: center;
                }

                .resume0-container {
                    display: flex;  
                    justify-content: space-between;
                    margin: 0 auto;
                    flex-direction: column;
                        width: 100%;
                }

                .resume0 .dark-side {
                    background-color: #A1CCD1;
                    color: #F4F2DE;
                    flex-basis: 40%;
                    padding: 20px;
                }

                .resume0 .light-side {
                    background-color: #F4F2DE;
                    flex-basis: 60%;
                    padding: 10px;
                }

                .resume0 .image-container
                {
                    text-align: center;
                }

                .resume0 .photo {
                    width: 150px;
                    height: 150px;
                    border-radius: 50%;
                    object-fit: cover;
                    display: inline-block;
                }

                .resume0 h1 {
                    color: #F4F2DE;
                    font-size: 28px;
                    margin-bottom: 10px;
                    font-weight: 700;
                    line-height: 1.5;
                    margin-top: 10px;
                    font-family: 'Kaushan Script', cursive;
                }

                .resume0 .text-dark {
                    color: #A1CCD1;
                }

                .resume0 .span-dark {
                    background-color: #A1CCD1;
                    border-radius: 25px;
                  }

                .resume0 .span-light {
                    background-color: #F4F2DE;
                    border-radius: 25px;
                  }

                .resume0 p {
                    margin-bottom: 5px;
                    font-family: 'Comfortaa', cursive;
                }

                .resume0 .section-title {
                    font-weight: bold;
                    margin-bottom: 10px;
                    font-size: 24px;
                }

                .resume0 .section-content {
                    margin-left: 20px;
                }
                </style>
                </head>
                <body>
                <div class="resume0 ">
                    <div class="resume0-container">
                        <div class="dark-side">
                            <div class="image-container">
                                {{#if ImageUrl}}
                                    <img class="photo"
                                        src={{ImageUrl}}
                                        alt={{FullName}} />  
                                {{/if}}                     
                            </div>
                            <h1> {{FullName}}</h1>
                            <div>
                                <h1 class="section-title span-light text-dark">About me</h1>   
                                <p>{{Summary}}</p>
                            </div>
                            <div class="contact-section">
                                <h1 class="section-title span-light text-dark">Contact</h1>
                                <p>
                                    <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#F4F2DE" height="1em" viewBox="0 0 384 512">
                                        <path d="M215.7 499.2C267 435 384 279.4 384 192C384 86 298 0 192 0S0 86 0 192c0 87.4 117 243 168.3 307.2c12.3 15.3 35.1 15.3 47.4 0zM192 128a64 64 0 1 1 0 128 64 64 0 1 1 0-128z"/>
                                    </svg>
                                    Address: {{Contacts.Address}}
                                </p>
                                <p>
                                    <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#F4F2DE" height="1em" viewBox="0 0 512 512">
                                        <path d="M164.9 24.6c-7.7-18.6-28-28.5-47.4-23.2l-88 24C12.1 30.2 0 46 0 64C0 311.4 200.6 512 448 512c18 0 33.8-12.1 38.6-29.5l24-88c5.3-19.4-4.6-39.7-23.2-47.4l-96-40c-16.3-6.8-35.2-2.1-46.3 11.6L304.7 368C234.3 334.7 177.3 277.7 144 207.3L193.3 167c13.7-11.2 18.4-30 11.6-46.3l-40-96z"/>
                                    </svg>
                                    Phone: {{Contacts.PhoneNumber}}</p>
                                <p>
                                    <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#F4F2DE" height="1em" viewBox="0 0 512 512">
                                        <path d="M48 64C21.5 64 0 85.5 0 112c0 15.1 7.1 29.3 19.2 38.4L236.8 313.6c11.4 8.5 27 8.5 38.4 0L492.8 150.4c12.1-9.1 19.2-23.3 19.2-38.4c0-26.5-21.5-48-48-48H48zM0 176V384c0 35.3 28.7 64 64 64H448c35.3 0 64-28.7 64-64V176L294.4 339.2c-22.8 17.1-54 17.1-76.8 0L0 176z"/>
                                    </svg>
                                    Email: {{Contacts.Email}}
                                </p>
                            </div>                
                        </div>
                        <div class="light-side">

                            <div class="education-section">
                                <h1 class="section-title span-dark">Education</h1>
                                {{#each Education}}
                                    <div class="section-content">
                                        <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 640 512">
                                                <path d="M320 32c-8.1 0-16.1 1.4-23.7 4.1L15.8 137.4C6.3 140.9 0 149.9 0 160s6.3 19.1 15.8 22.6l57.9 20.9C57.3 229.3 48 259.8 48 291.9v28.1c0 28.4-10.8 57.7-22.3 80.8c-6.5 13-13.9 25.8-22.5 37.6C0 442.7-.9 448.3 .9 453.4s6 8.9 11.2 10.2l64 16c4.2 1.1 8.7 .3 12.4-2s6.3-6.1 7.1-10.4c8.6-42.8 4.3-81.2-2.1-108.7C90.3 344.3 86 329.8 80 316.5V291.9c0-30.2 10.2-58.7 27.9-81.5c12.9-15.5 29.6-28 49.2-35.7l157-61.7c8.2-3.2 17.5 .8 20.7 9s-.8 17.5-9 20.7l-157 61.7c-12.4 4.9-23.3 12.4-32.2 21.6l159.6 57.6c7.6 2.7 15.6 4.1 23.7 4.1s16.1-1.4 23.7-4.1L624.2 182.6c9.5-3.4 15.8-12.5 15.8-22.6s-6.3-19.1-15.8-22.6L343.7 36.1C336.1 33.4 328.1 32 320 32zM128 408c0 35.3 86 72 192 72s192-36.7 192-72L496.7 262.6 354.5 314c-11.1 4-22.8 6-34.5 6s-23.5-2-34.5-6L143.3 262.6 128 408z"/>
                                            </svg>
                                            Institute: {{this.SchoolName}}</p>
                                        <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 576 512">
                                                <path d="M316.9 18C311.6 7 300.4 0 288.1 0s-23.4 7-28.8 18L195 150.3 51.4 171.5c-12 1.8-22 10.2-25.7 21.7s-.7 24.2 7.9 32.7L137.8 329 113.2 474.7c-2 12 3 24.2 12.9 31.3s23 8 33.8 2.3l128.3-68.5 128.3 68.5c10.8 5.7 23.9 4.9 33.8-2.3s14.9-19.3 12.9-31.3L438.5 329 542.7 225.9c8.6-8.5 11.7-21.2 7.9-32.7s-13.7-19.9-25.7-21.7L381.2 150.3 316.9 18z"/>
                                            </svg>
                                            Degree: {{this.Degree}}
                                        </p>
                                        <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 512 512">
                                                <path d="M243.4 2.6l-224 96c-14 6-21.8 21-18.7 35.8S16.8 160 32 160v8c0 13.3 10.7 24 24 24H456c13.3 0 24-10.7 24-24v-8c15.2 0 28.3-10.7 31.3-25.6s-4.8-29.9-18.7-35.8l-224-96c-8-3.4-17.2-3.4-25.2 0zM128 224H64V420.3c-.6 .3-1.2 .7-1.8 1.1l-48 32c-11.7 7.8-17 22.4-12.9 35.9S17.9 512 32 512H480c14.1 0 26.5-9.2 30.6-22.7s-1.1-28.1-12.9-35.9l-48-32c-.6-.4-1.2-.7-1.8-1.1V224H384V416H344V224H280V416H232V224H168V416H128V224zM256 64a32 32 0 1 1 0 64 32 32 0 1 1 0-64z"/>
                                            </svg>
                                            Field of Study: {{this.FieldOfStudy}}
                                        </p>  
                                        <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 384 512">
                                                <path d="M0 64C0 28.7 28.7 0 64 0H224V128c0 17.7 14.3 32 32 32H384V448c0 35.3-28.7 64-64 64H64c-35.3 0-64-28.7-64-64V64zm384 64H256V0L384 128z"/>
                                            </svg>
                                            Description: {{this.Description}}
                                        </p>  
                                        {{#if this.EndDate}}
                                            <p>
                                                <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 384 512">
                                                    <path d="M32 0C14.3 0 0 14.3 0 32S14.3 64 32 64V75c0 42.4 16.9 83.1 46.9 113.1L146.7 256 78.9 323.9C48.9 353.9 32 394.6 32 437v11c-17.7 0-32 14.3-32 32s14.3 32 32 32H64 320h32c17.7 0 32-14.3 32-32s-14.3-32-32-32V437c0-42.4-16.9-83.1-46.9-113.1L237.3 256l67.9-67.9c30-30 46.9-70.7 46.9-113.1V64c17.7 0 32-14.3 32-32s-14.3-32-32-32H320 64 32zM288 437v11H96V437c0-25.5 10.1-49.9 28.1-67.9L192 301.3l67.9 67.9c18 18 28.1 42.4 28.1 67.9z"/>
                                                </svg>
                                                Start Date: {{this.StartDate}}
                                            </p>

                                            <p style="margin-bottom: 20px">
                                                <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 384 512">
                                                    <path d="M32 0C14.3 0 0 14.3 0 32S14.3 64 32 64V75c0 42.4 16.9 83.1 46.9 113.1L146.7 256 78.9 323.9C48.9 353.9 32 394.6 32 437v11c-17.7 0-32 14.3-32 32s14.3 32 32 32H64 320h32c17.7 0 32-14.3 32-32s-14.3-32-32-32V437c0-42.4-16.9-83.1-46.9-113.1L237.3 256l67.9-67.9c30-30 46.9-70.7 46.9-113.1V64c17.7 0 32-14.3 32-32s-14.3-32-32-32H320 64 32zM96 75V64H288V75c0 25.5-10.1 49.9-28.1 67.9L192 210.7l-67.9-67.9C106.1 124.9 96 100.4 96 75z"/>
                                                </svg>
                                                End Date: {{this.EndDate}}
                                            </p>
                                        {{else}}
                                            <p style="margin-bottom: 20px">
                                                <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 384 512">
                                                    <path d="M32 0C14.3 0 0 14.3 0 32S14.3 64 32 64V75c0 42.4 16.9 83.1 46.9 113.1L146.7 256 78.9 323.9C48.9 353.9 32 394.6 32 437v11c-17.7 0-32 14.3-32 32s14.3 32 32 32H64 320h32c17.7 0 32-14.3 32-32s-14.3-32-32-32V437c0-42.4-16.9-83.1-46.9-113.1L237.3 256l67.9-67.9c30-30 46.9-70.7 46.9-113.1V64c17.7 0 32-14.3 32-32s-14.3-32-32-32H320 64 32zM288 437v11H96V437c0-25.5 10.1-49.9 28.1-67.9L192 301.3l67.9 67.9c18 18 28.1 42.4 28.1 67.9z"/>
                                                </svg>
                                                Start Date: {{this.StartDate}}
                                            </p>
                                        {{/if}}
                                    </div>
                                {{/each}}
                            </div>
                            <div class="experience-section">
                                <h1 class="section-title span-dark">Working Experience</h1>
                                {{#each Experience}}
                                    <div class="section-content">
                                        <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 512 512">
                                                <path d="M184 48H328c4.4 0 8 3.6 8 8V96H176V56c0-4.4 3.6-8 8-8zm-56 8V96H64C28.7 96 0 124.7 0 160v96H192 320 512V160c0-35.3-28.7-64-64-64H384V56c0-30.9-25.1-56-56-56H184c-30.9 0-56 25.1-56 56zM512 288H320v32c0 17.7-14.3 32-32 32H224c-17.7 0-32-14.3-32-32V288H0V416c0 35.3 28.7 64 64 64H448c35.3 0 64-28.7 64-64V288z"/>
                                            </svg>
                                            Company: {{this.CompanyName}}
                                        </p>
                                        <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 448 512">
                                                <path d="M224 256A128 128 0 1 1 224 0a128 128 0 1 1 0 256zM209.1 359.2l-18.6-31c-6.4-10.7 1.3-24.2 13.7-24.2H224h19.7c12.4 0 20.1 13.6 13.7 24.2l-18.6 31 33.4 123.9 36-146.9c2-8.1 9.8-13.4 17.9-11.3c70.1 17.6 121.9 81 121.9 156.4c0 17-13.8 30.7-30.7 30.7H285.5c-2.1 0-4-.4-5.8-1.1l.3 1.1H168l.3-1.1c-1.8 .7-3.8 1.1-5.8 1.1H30.7C13.8 512 0 498.2 0 481.3c0-75.5 51.9-138.9 121.9-156.4c8.1-2 15.9 3.3 17.9 11.3l36 146.9 33.4-123.9z"/>
                                            </svg>
                                            Position: {{this.Position}}
                                        </p>
                                        <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 384 512">
                                                <path d="M0 64C0 28.7 28.7 0 64 0H224V128c0 17.7 14.3 32 32 32H384V448c0 35.3-28.7 64-64 64H64c-35.3 0-64-28.7-64-64V64zm384 64H256V0L384 128z"/>
                                            </svg>
                                            Description: {{this.Description}}
                                        </p>
                                        {{#if this.EndDate}}
                                            <p>
                                                <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 384 512">
                                                    <path d="M32 0C14.3 0 0 14.3 0 32S14.3 64 32 64V75c0 42.4 16.9 83.1 46.9 113.1L146.7 256 78.9 323.9C48.9 353.9 32 394.6 32 437v11c-17.7 0-32 14.3-32 32s14.3 32 32 32H64 320h32c17.7 0 32-14.3 32-32s-14.3-32-32-32V437c0-42.4-16.9-83.1-46.9-113.1L237.3 256l67.9-67.9c30-30 46.9-70.7 46.9-113.1V64c17.7 0 32-14.3 32-32s-14.3-32-32-32H320 64 32zM288 437v11H96V437c0-25.5 10.1-49.9 28.1-67.9L192 301.3l67.9 67.9c18 18 28.1 42.4 28.1 67.9z"/>
                                                </svg>
                                                Start Date: {{this.StartDate}}
                                            </p>

                                            <p style="margin-bottom: 20px">
                                                <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 384 512">
                                                    <path d="M32 0C14.3 0 0 14.3 0 32S14.3 64 32 64V75c0 42.4 16.9 83.1 46.9 113.1L146.7 256 78.9 323.9C48.9 353.9 32 394.6 32 437v11c-17.7 0-32 14.3-32 32s14.3 32 32 32H64 320h32c17.7 0 32-14.3 32-32s-14.3-32-32-32V437c0-42.4-16.9-83.1-46.9-113.1L237.3 256l67.9-67.9c30-30 46.9-70.7 46.9-113.1V64c17.7 0 32-14.3 32-32s-14.3-32-32-32H320 64 32zM96 75V64H288V75c0 25.5-10.1 49.9-28.1 67.9L192 210.7l-67.9-67.9C106.1 124.9 96 100.4 96 75z"/>
                                                </svg>
                                                End Date: {{this.EndDate}}
                                            </p>
                                        {{else}}
                                            <p style="margin-bottom: 20px">
                                                <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" fill="#A1CCD1" height="1em" viewBox="0 0 384 512">
                                                    <path d="M32 0C14.3 0 0 14.3 0 32S14.3 64 32 64V75c0 42.4 16.9 83.1 46.9 113.1L146.7 256 78.9 323.9C48.9 353.9 32 394.6 32 437v11c-17.7 0-32 14.3-32 32s14.3 32 32 32H64 320h32c17.7 0 32-14.3 32-32s-14.3-32-32-32V437c0-42.4-16.9-83.1-46.9-113.1L237.3 256l67.9-67.9c30-30 46.9-70.7 46.9-113.1V64c17.7 0 32-14.3 32-32s-14.3-32-32-32H320 64 32zM288 437v11H96V437c0-25.5 10.1-49.9 28.1-67.9L192 301.3l67.9 67.9c18 18 28.1 42.4 28.1 67.9z"/>
                                                </svg>
                                                Start Date: {{this.StartDate}}
                                            </p>
                                        {{/if}}                      
                                    </div>
                                {{/each}}
                            </div>
                        </div>
                    </div>
                </div>
                </body>
                </html>
                """,
            },
            new TemplateIM
            {
                Name = "Purple forest",
                Content = """
                  <html>
                  <head>
                    <style>

                    @import url('https://fonts.googleapis.com/css2?family=Josefin+Slab&family=Ysabeau+SC:wght@300&display=swap');

                    .resume1>* {
                      font-family: Arial, sans-serif;
                      background-color: #F5F5F5;
                      color: #ffffff;
                      margin: 0;
                      padding: 20px;
                    }

                    .resume1-all-container {
                        display: flex;
                        flex-direction: row;
                      margin: 0;
                    }

                    .resume1-container {
                        min-width: 600px;
                        max-width: 700px;
                      height: 1000px;
                        background: #fff;
                        margin: 0px auto 0px; 
                        box-shadow: 1px 1px 2px #DAD7D7;
                        border-radius: 3px;  
                        padding: 40px;
                        margin-top: 50px;
                    }

                    .resume1-left-button-container {
                      width: 30%;
                        display: flex;
                        justify-content: flex-start;
                        align-items: center;
                      padding-right: 0;
                      margin: 0;
                    }

                    .resume1-right-button-container {
                        width: 30%;
                        display: flex;
                        justify-content: flex-end;
                        align-items: center;
                        padding-left: 20%;
                      margin: 0;
                    }

                    @media screen and (max-width: 1186px) {
                      .resume1-container {
                        flex-direction: column;
                        width: 100%;
                      }
                    }

                    @media only screen and (max-width: 600px) {
                      .col {
                        display: block;
                        width: 100%;
                      }
                    }

                    .resume1 .strip1 {
                      width:100%;
                      height: 100%;
                      border-style: solid;
                      border-color: #ffffff;
                      background-color: #ffffff;
                    }

                    .resume1 .strip2 {
                      width: 100%;
                      height: 60px;
                      border-style: solid;
                      background-color: #ffffff;
                    }

                    .resume1 .lower-side {
                      background-color: #ACBCFF;
                      flex-basis: 60%;
                      padding: 10px;
                    }

                    .resume1 .photo {
                      width: 150px;
                      height: 150px;
                      display: inline-block;
                      margin-top: 10px;
                      margin-bottom: 10px;
                    }

                    .resume1 .about-me {
                      display: grid;
                      background-color: #68c1a4;

                    }

                    .resume1 .text-green {
                      color: #68c1a4;
                    }

                    .resume1 .text-blue {
                      color: #ACBCFF;
                    }

                    .resume1 p {
                      margin-bottom: 5px;
                      font-family: 'Ysabeau SC', sans-serif;
                    }

                    .resume1 .section-title {
                      font-size: 37px;
                      font-weight: 700;
                      line-height: 1.5;
                      margin-top: 10px;
                      font-family: 'Josefin Slab', serif;
                    }

                    .resume1 .section-content {
                      text-align: left;
                      margin-top: 20px;
                      margin-bottom: 20px;
                    }
                    </style>
                  </head>
                  <body>
                    <div class="resume1-all-container">
                      <div class="resume1">
                        <div class="resume1-container">
                          <div class="col-container">
                            <div class="strip1 col">
                              <h1 class="section-title text-blue">{{FullName}}</h1>
                              <div image-container>
                                {{#if ImageUrl}}
                                        <img class="photo"
                                            src={{ImageUrl}}
                                            alt={{FullName}} />  
                                    {{/if}}   
                              </div>
                              <p class="text-green">{{Summary}}</p>
                              <div class="contact-section about-me">
                                <h1 class="section-title strip2 text-blue">About Me</h1>
                                <div class="section-content">
                                  <p>
                                    <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                      <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                    </svg>
                                    Address: {{Contacts.Address}}
                                  </p>
                                  <p>
                                    <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                      <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                    </svg>
                                    Phone: {{Contacts.PhoneNumber}}
                                  </p>
                                  <p>
                                    <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                      <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                    </svg>
                                    Email: {{Contacts.Email}}
                                  </p>
                                </div>
                              </div>
                            </div>
                            <div class="lower-side col">
                              <div class="education-section">
                                <h1 class="section-title strip2 text-green">Education</h1>
                                {{#each Education}}
                                  <div class="section-content">
                                    <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                      <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                    </svg>
                                            Institute: {{this.SchoolName}}</p>
                                        <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                              <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                            </svg>
                                            Degree: {{this.Degree}}
                                        </p>
                                        <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                              <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                            </svg>
                                            Field of Study: {{this.FieldOfStudy}}
                                        </p>  
                                        <p>
                                          <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                            <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                          </svg>
                                          Description: {{this.Description}}
                                          </p>  
                                          {{#if this.EndDate}}
                                            <p>
                                                <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                                  <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                                </svg>
                                                Start Date: {{this.StartDate}}
                                            </p>
                                            <p style="margin-bottom: 20px">
                                                <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                                  <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                                </svg>
                                                End Date: {{this.EndDate}}
                                            </p>
                                          {{else}}
                                            <p style="margin-bottom: 20px">
                                              <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                                <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                              </svg>
                                              Start Date: {{this.StartDate}}
                                            </p>
                                          {{/if}}
                                  </div>
                                {{/each}}
                              </div>
                              <div class="experience-section">
                                <h1 class="section-title strip2 text-green">Work Experience</h1>
                                {{#each Experience}}
                                <div class="section-content">
                                  <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                                  <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                                </svg>
                                            Company: {{this.CompanyName}}
                                        </p>
                                        <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                                  <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                                </svg>
                                            Position: {{this.Position}}
                                        </p>
                                        <p>
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                                  <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                                </svg>
                                            Description: {{this.Description}}
                                        </p>
                                        {{#if this.EndDate}}
                                            <p>
                                                <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                                  <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                                </svg>
                                                Start Date: {{this.StartDate}}
                                            </p>

                                            <p style="margin-bottom: 20px">
                                                <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                                  <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                                </svg>
                                                End Date: {{this.EndDate}}
                                            </p>
                                        {{else}}
                                            <p style="margin-bottom: 20px">
                                                <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" xmlns="http://www.w3.org/2000/svg" height="1em" fill="#ffffff" viewBox="0 0 256 512">
                                                  <path d="M246.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-9.2-9.2-22.9-11.9-34.9-6.9s-19.8 16.6-19.8 29.6l0 256c0 12.9 7.8 24.6 19.8 29.6s25.7 2.2 34.9-6.9l128-128z"/>
                                                </svg>
                                                Start Date: {{this.StartDate}}
                                            </p>
                                        {{/if}}  
                                </div>
                                {{/each}}
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </body>
                </html>
                """,
            },
            new TemplateIM
            {
                Name = "Osaka Bay",
                Content = """
                    <html>
                    <head>
                        <style>
                            @import url('https://fonts.googleapis.com/css2?family=Amatic+SC:wght@700&family=Signika:wght@300&display=swap');

                            .resume2 > *{
                                font-family: Arial, sans-serif;
                                background-color: #F5F5F5;
                                color: #000000;
                                margin: 0;
                                padding: 20px;
                            }

                            .resume2-all-container {
                                display: flex;
                                flex-direction: row;
                            }

                            .resume2-left-button-container {
                                width: 30%;
                                display: flex;
                                justify-content: flex-start;
                                align-items: center;
                                padding-right: 0;
                                margin: 0;
                            }

                            .resume2-right-button-container {
                                width: 30%;
                                display: flex;
                                justify-content: flex-end;
                                align-items: center;
                                padding-left: 20%;
                                margin: 0;
                            }

                            .resume2-container {
                                min-width: 600px;
                                max-width: 700px;
                                height: 1000px;
                                background: #fff;
                                margin: 0px auto 0px; 
                                box-shadow: 1px 1px 2px #DAD7D7;
                                border-radius: 3px;  
                                padding: 40px;
                                margin-top: 50px;
                            }

                            @media screen and (max-width: 1186px) {
                                .resume2-container {
                                    flex-direction: column;
                                    width: 100%;
                                }
                            }

                            .resume2 .header {
                                margin-bottom: 30px;
                            }

                            .separator {
                                height: 10px;
                                display: inline-block;
                                border-left: 2px solid #999;
                                margin: 0px 10px;
                            }

                            .resume2 .image-container
                            {
                                text-align: center;
                            }

                            .resume2 .photo {
                                width: 150px;
                                height: 150px;
                                border-radius: 50%;
                                object-fit: cover;
                                display: inline-block;
                            }

                            .resume2 h1 {
                                color: #6554AF;
                                font-size: 40px;
                                margin-bottom: 10px;
                                font-weight: 700;
                                line-height: 1.5;
                                margin-top: 10px;
                                font-family: 'Amatic SC', cursive;
                            }

                            .resume2 p {
                                color:#000000;
                                margin-bottom: 5px;
                                font-family: 'Signika', sans-serif;
                            }

                            .resume2 span {
                                color:#000000;
                                margin-bottom: 5px;
                                font-family: 'Signika', sans-serif;
                            }

                            .resume2 .text-purple {
                                color: #9575DE;
                            }

                            .resume2 .text-underlined {
                                text-decoration: underline;
                            }

                            .resume2 .section-title {
                                color:#6554AF;
                                font-weight: bold;
                                margin-bottom: 10px;
                                font-size: 30px;
                                text-align: left;
                            }

                            .resume2 .section-content {
                                margin-left: 20px;
                                text-align: left;
                            }

                        </style>
                    </head>
                    <body>
                        <div class="resume2-all-container">
                            <div class="resume2 ml-[20%]">
                                <div class="resume2-container">
                                    <div class="header">
                                        <div class="image-container">
                                            {{#if ImageUrl}}
                                                <img class="photo"
                                                    src={{ImageUrl}}
                                                    alt={{FullName}} />  
                                            {{/if}}  
                                        </div>
                                        <h1 class="content-title">{{FullName}}</h1>
                                        <div class="contact-section">
                                            <span>Email: </span>
                                            <span class="text-purple">{{Contacts.Email}}</span>
                                            <span class="separator"></span>
                                            <span>Phone Number: </span>
                                            <span class="text-purple">{{Contacts.PhoneNumber}}</span>
                                            <span class="separator"></span>
                                            <span>Address: </span>
                                            <span class="text-purple">{{Contacts.Address}}</span>
                                        </div>
                                        <div class="about-me">
                                            <p class="text-underlined">{{Summary}}</p>
                                        </div>
                                    </div>
                                    <div class="experience-section">
                                        <h1 class="section-title">
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" fill="#6554af" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 512 512">
                                                <path d="M184 48H328c4.4 0 8 3.6 8 8V96H176V56c0-4.4 3.6-8 8-8zm-56 8V96H64C28.7 96 0 124.7 0 160v96H192 320 512V160c0-35.3-28.7-64-64-64H384V56c0-30.9-25.1-56-56-56H184c-30.9 0-56 25.1-56 56zM512 288H320v32c0 17.7-14.3 32-32 32H224c-17.7 0-32-14.3-32-32V288H0V416c0 35.3 28.7 64 64 64H448c35.3 0 64-28.7 64-64V288z"/>
                                            </svg>
                                            Working Experience
                                        </h1>
                                        {{#each Experience}}
                                            <div class="section-content">
                                                <p>Company: <span class="text-purple">{{this.CompanyName}}</span></p>
                                                <p>Position: <span class="text-purple">{{this.Position}}</span></p>
                                                {{#if this.EndDate}}
                                                    <p>
                                                        Start Date: <span class="text-purple">{{this.StartDate}}</span>
                                                    </p>
                                                    <p>
                                                        End Date: <span class="text-purple">{{this.EndDate}}</span>
                                                    </p>
                                                {{else}}
                                                    <p>
                                                    Start Date: <span class="text-purple">{{this.StartDate}}</span>
                                                    </p>
                                                {{/if}}
                                                <p style="margin-bottom: 20px">Description: <span class="text-purple">{{this.Description}}</span></p>
                                            </div>
                                        {{/each}}
                                    </div>
                                    <div class="education-section">
                                        <h1 class="section-title">
                                            <svg style="margin-left: 0.75rem; margin-right: 0.75rem;" fill="#6554af" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512">
                                                <path d="M219.3 .5c3.1-.6 6.3-.6 9.4 0l200 40C439.9 42.7 448 52.6 448 64s-8.1 21.3-19.3 23.5L352 102.9V160c0 70.7-57.3 128-128 128s-128-57.3-128-128V102.9L48 93.3v65.1l15.7 78.4c.9 4.7-.3 9.6-3.3 13.3s-7.6 5.9-12.4 5.9H16c-4.8 0-9.3-2.1-12.4-5.9s-4.3-8.6-3.3-13.3L16 158.4V86.6C6.5 83.3 0 74.3 0 64C0 52.6 8.1 42.7 19.3 40.5l200-40zM111.9 327.7c10.5-3.4 21.8 .4 29.4 8.5l71 75.5c6.3 6.7 17 6.7 23.3 0l71-75.5c7.6-8.1 18.9-11.9 29.4-8.5C401 348.6 448 409.4 448 481.3c0 17-13.8 30.7-30.7 30.7H30.7C13.8 512 0 498.2 0 481.3c0-71.9 47-132.7 111.9-153.6z"/>
                                            </svg>
                                            Education
                                        </h1>
                                        {{#each Education}}
                                            <div class="section-content">
                                                <p>Institute: <span class="text-purple">{{this.SchoolName}}</span></p>
                                                <p>Degree: <span class="text-purple">{{this.Degree}}</span></p>
                                                <p>Field of Study: <span class="text-purple">{{this.FieldOfStudy}}</span></p>
                                                <p>Description: <span class="text-purple">{{this.Description}}</span></p>
                                                {{#if this.EndDate}}
                                                    <p>
                                                        Start Date: <span class="text-purple">{{this.StartDate}}</span>
                                                    </p>
                                                    <p style="margin-bottom: 20px">
                                                        End Date: <span class="text-purple">{{this.EndDate}}</span>
                                                    </p>
                                                {{else}}
                                                    <p style="margin-bottom: 20px">
                                                    Start Date: <span class="text-purple">{{this.StartDate}}</span>
                                                    </p>
                                                {{/if}}
                                            </div>
                                        {{/each}}
                                    </div>
                                </div>
                            </div>

                        </div>
                    </body>
                </html>
                """,
            },
        };

        foreach (var template in templates)
        {
            await templateService.AddTemplateIfDoesNotExistAsync(template);
        }
    }
}