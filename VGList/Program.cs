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


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AnyOrigin"); //this globally applies the AnyOrigin Cors policy

app.MapControllers();

app.Run();
