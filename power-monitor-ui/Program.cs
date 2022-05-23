using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using power_monitor_ui.Data;
using MudBlazor.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMudServices();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 443;
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", builder =>
     builder.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
