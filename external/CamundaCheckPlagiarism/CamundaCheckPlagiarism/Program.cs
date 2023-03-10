using Camunda.Worker;
using Camunda.Worker.Client;
using CamundaCheckPlagiarism.Handlers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddExternalTaskClient(client => client.BaseAddress = new Uri("http://localhost:8080/engine-rest"));

services.AddCamundaWorker("checkPlagiarismWorker")
    .AddHandler<CheckPlagiarismHandler>()
    .ConfigurePipeline(pipeline => pipeline.Use(next => async context => await next(context)));

services.AddHealthChecks();

WebApplication app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseHealthChecks("/health");
app.Run();