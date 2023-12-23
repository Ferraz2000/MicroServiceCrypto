using CryptoAPI.BackgroundServices.HubSignalR;
using CryptoAPI.Data.Configurations;
using CryptoAPI.Data.Repositories;
using Microsoft.Extensions.Options;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHostedService<BackgroundCryptoService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection(nameof(DatabaseConfig)));
builder.Services.AddSingleton<IDataBaseConfig>(sp => sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);
builder.Services.AddSingleton<ICryptoRepository, CryptoRepository>();
builder.Services.AddSingleton<IMelhorPrecoRepository, MelhorPrecoRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
    hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<CryptoHub>("/cryptohub");
app.Run();

