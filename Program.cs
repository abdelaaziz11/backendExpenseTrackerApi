var builder = WebApplication.CreateBuilder(args);

// Ajouter CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://expense-tracker-3a15b.web.app")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAuthorization(); 

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();
app.Run();
