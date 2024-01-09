using API.GiamKichSan.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.GiamKichSan.Installers
{
    public static class ApplicationBuilderIntaller
    {
        public static void CustConfigure(this IApplicationBuilder app)
        {
            app.UseCors("AllowOrigin");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute(name: "default", pattern: "{controller=UploadFile}/{action=Index}/{id?}");
            });
        }
    }
}
