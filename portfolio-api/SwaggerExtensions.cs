namespace portfolio_api
{
  public static class SwaggerExtensions
  {
    public static void UseSwaggerInDevelopment(this WebApplication app)
    {
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Registration API V1");
          c.RoutePrefix = string.Empty; // Makes Swagger the default route
        });
      }
    }
  }
}
