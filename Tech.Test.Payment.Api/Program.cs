using Tech.Test.Payment.Api;
using Tech.Test.Payment.Application;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation()
        .AddApplication();
}


var app = builder.Build();
{
    app.UseDeveloperExceptionPage();
    app.UseExceptionHandler();
    //app.UseInfrastructure();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}