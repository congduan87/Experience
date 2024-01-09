using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Model.GiamKichSan.Common.SQL;

namespace API.GiamKichSan.Installers
{
    public static class ServiceCollectionInstaller
    {
        public static void CustConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.GiamKichSan", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("*",
                            "https://giamkichsan.com",
                            "https://blogs.giamkichsan.com",
                            "https://happywedding.giamkichsan.com",
                            "http://localhost:42037")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddSingleton(new BaseSQLConnection(Configuration.GetConnectionString("DefaultDbContext")));
        }
    }
}
