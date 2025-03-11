using MeuSiteEmMVC.Data;
using MeuSiteEmMVC.Services;
using Microsoft.EntityFrameworkCore;

namespace MeuSiteEmMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            //Mapeamento de Interfaces e Serviços
            builder.Services.AddScoped<IContatoInterface, ContatoService>();
            builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();

            builder.Services.AddAutoMapper(typeof(Program)); // Registra os profiles do AutoMapper baseados no assembly fornecido

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
        }
    }
}