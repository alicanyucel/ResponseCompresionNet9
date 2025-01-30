using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
    opt.Providers.Clear();
    // g�ncel browserlada daha iyi s�k�st�r�yor
   // opt.Providers.Add<BrotliCompressionProvider>();
    // t�m browserlarda performans zay�f
    opt.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseResponseCompression();
app.MapGet("/GetAll", () =>
{
    List<int> numbers = new();
    for(int i = 0; i <= 100; i++)
    {
        numbers.Add(i);
    }
    return numbers;
});
app.Run();
