
using BlazorAppServer.Services;
using EBazarAppServer.Data;
using EBazarAppServer.Data.Cart;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using BlazorAppServer.SessionStorage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;
using BlazorAppServer.Data.ViewComponents;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options => options.EnableEndpointRouting = false);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});
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
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped<ShoppingCart>();
builder.Services.AddScoped<ItemsInCart>();
builder.Services.AddScoped<SearchItems>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddSingleton<IJSInProcessRuntime>(services => (IJSInProcessRuntime)services.GetRequiredService<IJSRuntime>());
//builder.Services.AddScoped<ISyncSessionStorageService, SyncSessionStorageService>();


//builder.Services.AddScoped(sc=>ShoppingCart.GetShoppingCart(sc));
//builder.Services.AddScoped(typeof(ShoppingCart));


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
app.UseSession();
app.UseAuthorization();
app.MapControllers();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();
