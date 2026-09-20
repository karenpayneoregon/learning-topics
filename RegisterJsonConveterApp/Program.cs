using RegisterJsonConveterApp.JsonConverters;

namespace RegisterJsonConveterApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*
             * Register the custom JSON converter globally for all Razor Pages.
             * This ensures that the converter is applied to all JSON serialization
             * and deserialization operations.
             */
            builder.Services
                .AddRazorPages()
                .AddJsonOptions(options =>    
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new UpperCaseFirstCharConverter());
                });


            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
