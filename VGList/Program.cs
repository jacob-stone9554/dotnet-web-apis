var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapControllers();

app.Run();
