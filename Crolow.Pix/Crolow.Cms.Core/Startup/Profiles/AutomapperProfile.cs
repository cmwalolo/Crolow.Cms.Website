using AutoMapper;
using Crolow.Cms.Core.Models.Entities;
using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Cms.Core.Models.ViewModel;
using Crolow.Cms.Core.Models.ViewModel.Basket;
using Crolow.Cms.Core.Models.ViewModel.Cards;
using Crolow.Cms.Core.Models.ViewModel.Custom;
using Crolow.Cms.Core.Models.ViewModel.Forms;
using Crolow.Cms.Core.Models.ViewModel.Media;
using Crolow.Cms.Core.Models.ViewModel.Navigation;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Extensions;

namespace Crolow.Core.Startup.Profiles
{
    public class AutomapperProfile : Profile
    {
        string[] menuFallBackvalues = new string[] { "menuDisplayName", "pageTitle", "title", "Name" };
        string[] titleFallBackvalues = new string[] { "pageTitle", "Name" };
        string cardImageSize = "height=600&rmode=max";



        protected IPublishedUrlProvider urlProvider;
        public AutomapperProfile()
        {
            // TODO Fix UrlProvider
            // this.urlProvider = urlProvider;
            RegisterNavigation();
            RegisterCards();
            RegisterMedia();
            RegisterForm();
            RegisterCrolowBasket();
            RegisterSeo();
            RegisterCustom();
            RegisterProductModels();
        }

        private void RegisterSeo()
        {
            var summaries = new string[] { "summary", "caption" };
            var images = new string[] { "image" };

            CreateMap<IPublishedContent, SeoModel>().
                ForMember(p => p.OGImage, o => o.MapFrom(p => GetMediaCropUrl(p, "OGImage", images, UrlMode.Absolute))).
                ForMember(p => p.TwitterImage, o => o.MapFrom(p => GetMediaCropUrl(p, "TwitterImage", images, UrlMode.Absolute))).
                ForMember(p => p.FBAppId, o => o.MapFrom(p => GetValue<string>(p, "FBAppId"))).
                ForMember(p => p.Canonical, o => o.MapFrom(p => GetValue<string>(p, "Canonical"))).
                ForMember(p => p.MetaAuthor, o => o.MapFrom(p => GetValue<string>(p, "metaAuthor"))).
                ForMember(p => p.MetaDescription, o => o.MapFrom(p => GetValueAltProperties(p, "metaDescription", summaries))).
                ForMember(p => p.MetaKeywords, o => o.MapFrom(p => GetValue<List<string>>(p, "metaKeywords"))).
                ForMember(p => p.MetaTagBlock, o => o.MapFrom(p => GetValue<string>(p, "metaTagBlock"))).
                ForMember(p => p.OGDescription, o => o.MapFrom(p => GetValueAltProperties(p, "OGDescription", summaries))).
                ForMember(p => p.OGLocale, o => o.MapFrom(p => GetValue<string>(p, "OGLocale"))).
                ForMember(p => p.OGTitle, o => o.MapFrom(p => GetValueAlt(p, "OGTitle", p.Name))).
                ForMember(p => p.OGType, o => o.MapFrom(p => GetValue<string>(p, "OGType"))).
                ForMember(p => p.OGUrl, o => o.MapFrom(p => GetValue<string>(p, "OGURL"))).
                ForMember(p => p.TwitterCardType, o => o.MapFrom(p => GetValue<string>(p, "TwitterCardType"))).
                ForMember(p => p.TwitterDescription, o => o.MapFrom(p => GetValueAltProperties(p, "TwitterDescription", summaries))).
                ForMember(p => p.TwitterImageAlt, o => o.MapFrom(p => GetValue<string>(p, "TwitterImageAlt"))).
                ForMember(p => p.TwitterSite, o => o.MapFrom(p => GetValue<string>(p, "TwitterSite"))).
                ForMember(p => p.TwitterTitle, o => o.MapFrom(p => GetValueAlt(p, "TwitterTitle", p.Name))).
                ForMember(p => p.PageTitle, o => o.MapFrom(p => GetValueAlt(p, "pageTitle", p.Name)));
        }
        private void RegisterCrolowBasket()
        {
            CreateMap<Product, CrolowProductModel>();
            CreateMap<IPublishedContent, CrolowOrderLineModel>();
            CreateMap<Crolow.Cms.Core.Models.Umbraco.Member, CrolowMemberModel>();
        }

        private void RegisterForm()
        {
            CreateMap<GridForm, GridFormModel>()
                .ForMember(p => p.Template, o => o.MapFrom(p => p.Template))
                .ForMember(p => p.Application, o => o.MapFrom(p => p.Application));

            CreateMap<CrolowFormEntry, CrolowFormViewModel>();
        }


        private void RegisterMedia()
        {
            var map = CreateMap<MediaPage, MediaPageModel>()
                .ForMember(p => p.Caption, o => o.MapFrom(p => p.Caption))
                .ForMember(p => p.Url, o => o.MapFrom(p => GetUrl(p)))
                .ForMember(p => p.Tags, o => o.MapFrom(p => p.Tags))
                .ForMember(p => p.Url, o => o.MapFrom(p => GetUrl(p)))
                .ForMember(p => p.ImageUrl, o => o.MapFrom(p => GetMediaCropUrl(p.Image)));

            CreateMap<MediaCollectionPage, MediaCollectionPageModel>()
                .ForMember(p => p.Url, o => o.MapFrom(p => GetUrl(p)))
                .ForMember(p => p.ImageUrl, o => o.MapFrom(p => GetMediaCropUrl(p.Image)))
                .ForMember(p => p.Collections,
                                    o => o.MapFrom(p => p.Children<MediaCollectionPage>(null)))
                .ForMember(p => p.Images,
                                    o => o.MapFrom(p => p.Children<MediaPage>(null).OrderByDescending(p => p.Date)));


            CreateMap<MediaCollectionLandingPage, MediaCollectionLandingPageModel>()
                .ForMember(p => p.Url, o => o.MapFrom(p => GetUrl(p)))
                .ForMember(p => p.Collections,
                                    o => o.MapFrom(p => p.Children<MediaCollectionPage>(null)));

        }

        private void RegisterNavigation()
        {
            var map = CreateMap<IPublishedContent, MenuItemModel>()
                .ForMember(dest => dest.Title,
                            opt => opt.MapFrom(new StringPropertyFallBackResolver<MenuItemModel>(menuFallBackvalues)))
                .ForMember(dest => dest.PageTitle,
                            opt => opt.MapFrom(new StringPropertyFallBackResolver<MenuItemModel>(titleFallBackvalues)))
                .ForMember(p => p.Url, o => o.MapFrom(p => p is MenuItem ? ((MenuItem)p).Link.Url : GetUrl(p)))
                .ForMember(p => p.Icon, o => o.MapFrom(p => p is MenuItem ? ((MenuItem)p).Icon : ""))
                .ForMember(p => p.Children, o => o.Ignore());
        }

        private void RegisterCustom()
        {
            CreateMap<HeaderRollup, HeaderRollupModel>()
                .ForMember(p => p.Image, o => o.MapFrom(p => GetMediaUrl(p.Image)))
                .ForMember(p => p.ImageLogo, o => o.MapFrom(p => GetMediaUrl(p.ImageLogo)))
                .ForMember(p => p.ImageTitle, o => o.MapFrom(p => GetMediaUrl(p.ImageTitle)))
                .ForMember(p => p.Items, o => o.Ignore());
        }

        private void RegisterProductModels()
        {
        }
        private void RegisterCards()
        {

            CreateMap<Link, LinkModel>()
                .ForMember(p => p.Title, o => o.MapFrom(p => p.Name))
                .ForMember(p => p.Url, o => o.MapFrom(p => p.Url))
                .ForMember(p => p.Target, o => o.MapFrom(p => p.Target));

            CreateMap<GridCards, CardsModel>()
                .ForMember(p => p.Items, o => o.MapFrom(p => p.Cards));

            CreateMap<GridContentCards, CardsModel>()
                .ForMember(p => p.Items, o => o.Ignore());


            CreateMap<GridCard, CardItemModel>()
                .ForMember(p => p.CardType, o => o.MapFrom(p => "Default"))
                .ForMember(p => p.Image, o => o.MapFrom(p => GetCardMediaUrl(p.Image)))
                .ForMember(p => p.Links, o => o.MapFrom(p => p.Links));


            CreateMap<BasicCard, CardItemModel>()
                .ForMember(p => p.OriginId, o => o.MapFrom(p => p.Id))
                .ForMember(p => p.OriginVersion, o => o.MapFrom(p => p.UpdateDate.Ticks))
                .ForMember(p => p.Image, o => o.MapFrom(p => GetCardMediaUrl(p.Image)));
        }
        private string GetUrl(IPublishedContent content)
        {
            return content.Url();
        }

        private string GetMediaUrl(MediaWithCrops content)
        {
            return content.Url(mode: UrlMode.Default) ?? "";
        }

        private string GetCardMediaUrl(MediaWithCrops content)
        {
            return $"{content.Url(mode: UrlMode.Default) ?? ""}?{cardImageSize}";
        }

        private string GetMediaCropUrl(IPublishedContent content, string property, string[] altProperties, UrlMode urlMode)
        {
            var media = content.Value<MediaWithCrops>(property);
            if (media == null)
            {
                foreach (string pty in altProperties)
                {
                    if (content.HasProperty(pty))
                    {
                        media = content.Value<MediaWithCrops>(pty);
                        if (media != null)
                        {
                            break;
                        }
                    }
                }

            }

            if (media != null)
            {
                return media.Url(null, urlMode);
            }
            return null;
        }

        private string GetMediaCropUrl(MediaWithCrops content)
        {
            return content.GetCropUrl("Small and large", UrlMode.Default) ?? "";
        }

        private string GetMediaCropUrl(MediaWithCrops content, string crop)
        {
            return content.GetCropUrl(crop, UrlMode.Default);
        }

        private T GetValue<T>(IPublishedContent content, string property)
        {
            return content.Value<T>(property);
        }

        private string GetValueAlt(IPublishedContent content, string property, string altProperty = "")
        {
            var value = content.Value<string>(property);
            if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(altProperty))
            {
                value = altProperty;
            }
            return value;
        }

        private string GetValueAltProperties(IPublishedContent content, string property, string[] altProperties)
        {
            var value = content.Value<string>(property);
            if (string.IsNullOrEmpty(value))
            {
                foreach (var pty in altProperties)
                {
                    if (content.HasProperty(pty))
                    {
                        value = content.Value<string>(pty);
                        if (!string.IsNullOrEmpty(value))
                        {
                            break;
                        }
                    }
                }
            }
            return value;
        }
    }
}

