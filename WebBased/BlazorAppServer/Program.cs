
using BlazorAppServer.Services;
using EBazarAppServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
//builder.Services.AddHttpClient<IActorService, ActorService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
//builder.Services.AddHttpClient<IProducerService, ProducerService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
//builder.Services.AddHttpClient<IMovieService, MovieService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
//builder.Services.AddHttpClient<IOrdersService, OrdersService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
//builder.Services.AddHttpClient<ICinemaService, CinemaService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
//builder.Services.AddHttpClient<ICinemaService, CinemaService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
//builder.Services.AddHttpClient<IFileUploadService,FileUploadService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));
//builder.Services.AddHttpClient<IMovieCustomService, MovieCustomService>(client => client.BaseAddress = new Uri("https://localhost:44356/"));

builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
//builder.Services.AddScoped<IFileUploadService, FileUploadService>();




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
