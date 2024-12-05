var builder = WebApplication.CreateBuilder(args);

// Ajouter les services CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // URL du frontend Angular
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Ajouter les services du contrôleur
builder.Services.AddControllers();

var app = builder.Build();

// Activer CORS
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
