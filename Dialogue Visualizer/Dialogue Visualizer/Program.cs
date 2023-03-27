
using Dialogue_Visualizer.Helpers;
using Dialogue_Visualizer.Repositories;
using Dialogue_Visualizer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DialogueDbContext>();
builder.Services.AddTransient<DialogueRepository>();
builder.Services.AddTransient<DialogueBlockRepository>();
builder.Services.AddTransient<ProjectRepository>();
builder.Services.AddTransient<SceneRepository>();
builder.Services.AddTransient<DialogueService>();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
