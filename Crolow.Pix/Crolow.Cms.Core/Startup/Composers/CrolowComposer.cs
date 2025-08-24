using AutoMapper;
using Crolow.Cms.Core.Controllers.Api.BasketManagement;
using Crolow.Cms.Core.HtmlHelpers.Dictionary;
using Crolow.Cms.Core.Services.Interdaces;
using Crolow.Cms.Core.Startup.Mvc;
using Crolow.Core.Startup.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Crolow.CMS.Core.Startup.Composers
{
    public class CrolowComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddControllersWithViews()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationExpanders.Add(new ComponentViewLocationExpander());
                });

            CreateMappers(builder);
            AddDependencies(builder);
            AddIOC(builder);
            AddCaching(builder);
            Console.WriteLine("Crolow Composer is loaded");
        }

        private void AddCaching(IUmbracoBuilder builder)
        {
            builder.Services.AddResponseCaching();
            builder.Services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                        Duration = 30,
                        Location = ResponseCacheLocation.Client
                    });
            });
        }

        private void CreateMappers(IUmbracoBuilder builder)
        {
            // builder.Services.AddAutoMapper(new[] { typeof(CrolowComposer).Assembly });
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutomapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
        }

        private void AddIOC(IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<DictionaryHelper>();
            builder.Services.AddScoped<IDictionaryLocalizer, DictionaryLocalizer>();

            builder.Services.AddSingleton<ICrolowBasketService, CrolowBasketService>();
        }

        private void AddDependencies(IUmbracoBuilder builder)
        {

        }
    }
}
