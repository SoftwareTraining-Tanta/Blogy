using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using BlogyBackend.Shared;

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

        options.AddPolicy("adminstrator",
            authBuilder =>
            {
                authBuilder.RequireRole("adminstrator");
            });

        });        
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(Constants.user, options =>
        {
            options.Cookie.Name = Constants.user;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,"AdminAuthentication", options =>
        {
            options.Cookie.Name = "AdminAuthentication";
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