using AutoMapper;
using Crolow.Cms.Core.Models.Entities;
using Crolow.Cms.Core.Models.ViewModel.Forms;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Web.Common.Controllers;
namespace Crolow.Cms.Core.Controllers.Api.FormsManagement;


public class CrolowFormBoApiController : UmbracoAuthorizedController
{
    private readonly IScopeProvider _scopeProvider;
    private readonly IContentService contentService;
    private readonly IMapper mapper;
    public CrolowFormBoApiController(IMapper mapper, IScopeProvider scopeProvider, IContentService contentService)
    {
        _scopeProvider = scopeProvider;
        this.contentService = contentService;
        this.mapper = mapper;
    }


    [HttpGet]
    public IEnumerable<CrolowFormViewModel> GetComments([FromQuery] string application, [FromQuery] int status = 0, [FromQuery] int page = 0, [FromQuery] int pageSize = 20)
    {
        application = string.IsNullOrEmpty(application) ? "" : application;
        using var scope = _scopeProvider.CreateScope();
        var queryResults = scope.Database.Fetch<CrolowFormEntry>("SELECT * FROM CrolowForm WHERE Status=@1 and (@0='' or Application = @0) order by Id", application, status).Skip(page * pageSize).Take(pageSize);
        scope.Complete();

        var grouped = queryResults.GroupBy(p => p.BlogPostUmbracoId);
        var contents = grouped.Select(p => contentService.GetById(p.Key));

        var results = mapper.Map<List<CrolowFormViewModel>>(queryResults);

        string[] statuses = { "Awaiting", "In progress", "Accepted", "Rejected" };
        foreach (var item in results)
        {
            item.PageName = contents.FirstOrDefault(p => p != null && p.Id == item.BlogPostUmbracoId)?.Name ?? string.Empty;
            item.StatusText = statuses[item.Status];
        }

        return results;
    }

    [HttpGet]
    public IEnumerable<string> GetApplications()
    {
        using var scope = _scopeProvider.CreateScope();
        var queryResults = scope.Database.Fetch<string>("SELECT distinct application FROM CrolowForm order by Application");
        scope.Complete();
        return queryResults;
    }

    [HttpPost]
    public void UpdateComment(CrolowFormEntry comment)
    {
        using var scope = _scopeProvider.CreateScope();
        scope.Database.Update(comment);
        scope.Complete();
    }

    [HttpPost]
    public void DeleteComment(CrolowFormEntry comment)
    {
        using var scope = _scopeProvider.CreateScope();
        scope.Database.Delete(comment);
        scope.Complete();
    }
}