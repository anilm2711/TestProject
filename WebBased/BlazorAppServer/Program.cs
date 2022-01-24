
using BlazorAppServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient<IActorService, ActorService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
builder.Services.AddHttpClient<IProducerService, ProducerService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
builder.Services.AddHttpClient<IMovieService, MovieService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
builder.Services.AddHttpClient<IOrderService, OrderService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
builder.Services.AddHttpClient<ICinemaService, CinemaService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
builder.Services.AddHttpClient<ICinemaService, CinemaService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));

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
