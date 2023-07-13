using Contacts.Data;
using Contacts.Helpers;
using Contacts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<DBContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserSession, UserSession>();

builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

// Como funciona o ValidateAntiForgeryToken?
// Um token gerado na View é baseado no usuário logado e na sessão do browser e é submetido via POST para a Controller. 
// Se o servidor recebe um token que não corresponde a identidade do usuário autenticado, a solicitação é rejeitada (400 bad request)
// Posso adicionar aqui de forma global (new AutoValidateAntiforgeryTokenAttribute()), ou acima de cada método do tipo POST, PUT e DELETE na
// Controller com [ValidateAntiForgeryToken].
// https://www.eduardopires.net.br/2018/02/prevenindo-ataques-csrf-no-asp-net-core/
// https://learn.microsoft.com/pt-br/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-basic-crud-functionality-with-the-entity-framework-in-asp-net-mvc-application

builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();