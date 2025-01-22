using PlanCare.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();  // Add SignalR services
builder.Services.AddHostedService<CarRegistrationMonitorService>();  // Register Background Service
builder.Services.AddCors(o =>
{
    o.AddPolicy("MyPolicy", p => p
        .WithOrigins("https://localhost:64508")
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.MapFallbackToFile("/index.html");




app.UseCors("MyPolicy"); // Use the policy

app.UseWebSockets();

app.UseHttpsRedirection();

app.MapHub<CarRegistrationHub>("/carRegistrationHub");


app.Run();


