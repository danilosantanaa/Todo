using Todo.Api.Common.Middlewares;

namespace Todo.Api.Common.Settings;

public static class AppSettings
{
    public static WebApplication AddSettings(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        return app;
    }
}