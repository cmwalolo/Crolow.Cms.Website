using Crolow.Cms.Generic.Core.Models.Umbraco;
using Umbraco.Community.BlockPreview.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
.AddBlockPreview(options =>
{
    options.BlockGrid = new()
    {
        Enabled = true,
        ContentTypes = [
            ImageBlock.ModelTypeAlias,
            CallToActionBlock.ModelTypeAlias,
            HeadlineBlock.ModelTypeAlias,
            CardBlock.ModelTypeAlias,
            HeroBlock.ModelTypeAlias,
            InspirationBlock.ModelTypeAlias,
            RichTextBlock.ModelTypeAlias
            ]
            ,
        Stylesheet = "/css/myblockgridlayout.css"
    };
    options.BlockList = new()
    {
        Enabled = false
    };
})
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
