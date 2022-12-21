using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace TaxCalculator.UI.Desktop.Extensions
{
    public static class ApplicationExtensions
    {
        public static TService GetService<TService>(this Application app)
        {
            return ((App)app).AppHost.Services.GetRequiredService<TService>();
        }
    }
}
