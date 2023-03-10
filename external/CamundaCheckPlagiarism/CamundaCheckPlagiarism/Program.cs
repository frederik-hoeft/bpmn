using Camunda.Worker;
using Camunda.Worker.Client;
using CamundaCheckPlagiarism.Handlers;
using CamundaCheckPlagiarism.Mail;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddExternalTaskClient(client => client.BaseAddress = new Uri("http://localhost:8080/engine-rest"));

services.AddCamundaWorker("checkPlagiarismWorker")
    .AddHandler<CheckPlagiarismHandler>()
    .AddHandler<SendPassedEmailHandler>()
    .AddHandler<SendFailedEmailHandler>()
    .ConfigurePipeline(pipeline => pipeline.Use(next => async context => await next(context)));

EmailConfiguration emailConfig = EmailConfiguration.LoadFromConfigFile();

services.AddSingleton(emailConfig);

services.AddScoped<IEmailService, EmailService>();

services.AddHealthChecks();

WebApplication app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseHealthChecks("/health");
app.Run();