using EBazarWebApi.Data;
using EBazarWebApi.Data.Cart;
using EBazarWebApi.Data.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddMvc().AddSessionStateTempDataProvider();    
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});
//builder.Services.AddScoped<IActorsService, ActorsService>();
//builder.Services.AddScoped<IProducersService, ProducersService>();
//builder.Services.AddScoped<ICinemasService,CinemasService>();
builder.Services.AddScoped<IMoviesService,MoviesService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc=>ShoppingCart.GetShoppingCart(sc));
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();
//Cross-Origin Resource Sharing (CORS) - HTTP - MDN Web ...
app.UseCors("CorsPolicy");

//for image display
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthorization();


app.MapControllers();





//Seed database
AppDbInitializer.Seed(app);
//AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();
