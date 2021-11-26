using AUA.ProjectName.Common.Consts;

namespace AUA.ProjectName.WebApi.AppConfiguration
{
    public static class AppConfigExtension
    {
        public static void Configuration(this IApplicationBuilder app)
        {
            app.UseCors();

            app.DefaultConfiguration();

            app.SwaggerConfiguration();

            app.MiddlewareConfiguration();

            app.MvcConfiguration();

        }

        private static void DefaultConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.SwaggerConfiguration();

           

            //app.UseSession();

            //app.UseAuthorization();

        }

        private static void UseCors(this IApplicationBuilder app)
        {
            //app.UseCors(builder => builder.WithOrigins(AppSetting.ClientBaseUrlAddress)
            //    .AllowAnyOrigin()
            //    .AllowAnyHeader()
            //    .AllowAnyMethod());
            app.UseCors(options => options.AllowAnyOrigin()
                                          .AllowAnyHeader()
                                          .AllowAnyMethod());
        }

        private static void SwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(option =>
            {

                option.SwaggerEndpoint(SwaggerConsts.JsonPathV1, SwaggerConsts.VersionV1);
                option.EnableFilter();
                option.DefaultModelsExpandDepth(-1);

            });


        }

        private static void MiddlewareConfiguration(this IApplicationBuilder app)
        {
            ////  app.UseSession();

            // // app.UseCookiePolicy();

            //  //app.UseAuthentication();

            // // app.UseHttpsRedirection();

            //  app.UseStaticFiles();

            //  app.UseCookiePolicy();

        }

        private static void MvcConfiguration(this IApplicationBuilder app)
        {

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            });

        }

    }
}
