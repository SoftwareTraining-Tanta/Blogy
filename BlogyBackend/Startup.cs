using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using BlogyBackend.Shared;
using System.Security.Claims;

public class Startup
{

    public void ConfigureServices(IServiceCollection services)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // services.AddSingleton<ShopyCtx>();
        services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddSwaggerGen();
        services.AddAuthorization(options =>
        {

            options.AddPolicy(Roles.Admin,
                authBuilder =>
                {
                    authBuilder.RequireClaim(ClaimTypes.Role, Roles.Admin);
                });
            options.AddPolicy(Roles.Premium,
            authBuilder =>
            {
                authBuilder.RequireClaim(ClaimTypes.Role, Roles.Premium);
            });
            options.AddPolicy(Roles.Basic,
             authBuilder =>
            {
                authBuilder.RequireClaim(ClaimTypes.Role, Roles.Basic);
            });
        });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, Authentications.user, options =>
        {
            options.Cookie.Name = Authentications.user;
        })
        .AddCookie(Authentications.AdminAuthentication, options =>
        {
            options.Cookie.Name = Authentications.AdminAuthentication;
        });
        services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("*")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {


                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
        else
            app.UseHsts();

        app.UseRouting();
        app.UseCors("_myAllowSpecificOrigins");
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

        });


    }

}