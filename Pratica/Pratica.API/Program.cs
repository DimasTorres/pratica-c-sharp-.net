using Pratica.API.Configuration;
using Pratica.Application.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MapperFactory));
builder.Services.AddControllers();

//Configure Database
builder.DbConnectionConfigure();

//Configure Ioc
builder.Services.ConfigureIoC();

//Configure Open Api
builder.Services.ConfigureOpenApi();

//Configure Authentication
builder.ConfigureAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
