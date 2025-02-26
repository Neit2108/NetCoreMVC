namespace ExtendMethods
{
    public static class AppExtend
    {
        public static void AddStatusCodePage(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(
    appError =>
    {
        appError.Run(async context =>
        {
            var response = context.Response;
            var code = response.StatusCode;
            var content = @$"<html><body><h1>Custom error page</h1>
            <span style='color:red;font-size:20px;'>Status code: {code}</span>";
            await response.WriteAsync(content);
        });
    }
);
        }
    }
}