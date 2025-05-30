using Electronics;
using Electronics.Classes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DeviceManager>(serviceProvider => {
    var configuration = builder.Configuration;
    string filePath = configuration["DeviceFilePath"] ?? "input.txt";
    return new DeviceManager(filePath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();