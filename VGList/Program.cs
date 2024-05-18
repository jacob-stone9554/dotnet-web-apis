var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add and configure defualt CORS policies
builder.Services.AddCors(options =>
{
    //add a policy allowing only origins specified in appsettings.json
    options.AddDefaultPolicy(cfg =>
    {
        cfg.WithOrigins(builder.Configuration["AllowedOrigins"]);
        cfg.AllowAnyHeader();
        cfg.AllowAnyMethod();
    });

    //add a policy allowing all/any origins (apply only to certain endpoints, like /error)
    options.AddPolicy(name: "AnyOrigin",
        cfg =>
        {
            cfg.AllowAnyOrigin();
            cfg.AllowAnyHeader();
            cfg.AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//check to see if it is using the development environment AND if it should use swagger.
var useSwagger = builder.Configuration.GetValue<bool>("Swagger:UseSwagger");
if (app.Environment.IsDevelopment() && useSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else if (useSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/cod/test", async context =>
{
    context.Response.Headers["Cache-Control"] = "no-store";
    var responseText = "<script>" +
        "window.alert('Your client supports JavaScript!'" +
        "\\r\\n\\r\\n" +
        $"Server time (UTC): {DateTime.UtcNow.ToString("O")}" +
        "\\r\\n" +
        "Client time (UTC): ' + new Date().toISOString());" +
        "</script>" +
        "<noscript>Your client does not support javascript</noscript>";
    await context.Response.WriteAsync(responseText, context.RequestAborted);
})
.RequireCors("AnyOrigin");
//app.MapControllers()
//  .RequireCors("AnyOrigin");//this globally applies the AnyOrigin cors policy. To specify a more specific policy, use the [EnableCors] attribute on the controller.

app.Run();
