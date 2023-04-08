using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using messenger2.DataAccessLayer.Interfaces;
using messenger2.DataAccessLayer.Repositories;
using messenger2.Hubs.SignalRApp;
using messenger2.Services.Implementations;
using messenger2.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllersWithViews();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySql(connection, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddMvc(options =>
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute()));

builder.Services.AddSignalR();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IChatRepository, ChatRepository>();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IContactsService, ContactsService>();
builder.Services.AddTransient<IChatsService, ChatsService>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(name: "chats",
                pattern: "Chats/{FriendId?}",
                defaults: new { controller = "Chats", action = "Chats" });

app.MapControllerRoute(name: "contacts",
                pattern: "Contacts",
                defaults: new { controller = "Contacts", action = "Contacts" });

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/Chats/{FriendId?}");
});

app.Run();
