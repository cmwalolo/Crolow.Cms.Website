using Crolow.Cms.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Controllers.Api.CrolowForm;

[Route("/CrolowFormApi/[action]/")]
public class CrolowFormApiController : UmbracoApiController
{
    private readonly IScopeProvider _scopeProvider;
    private readonly IContentService contentService;
    public CrolowFormApiController(IScopeProvider scopeProvider, IContentService contentService)
    {
        _scopeProvider = scopeProvider;
        this.contentService = contentService;
    }


    [HttpGet]
    public IEnumerable<CrolowFormEntry> GetComments(int umbracoNodeId)
    {
        using var scope = _scopeProvider.CreateScope();
        var queryResults = scope.Database.Fetch<CrolowFormEntry>("SELECT * FROM CrolowForm WHERE Status = 2 and BlogPostUmbracoId = @0", umbracoNodeId);
        scope.Complete();
        return queryResults;
    }

    [HttpGet]
    public IContent GetById(Guid Id)
    {
        return contentService.GetById(Id);
    }

    [HttpPost]
    public void InsertComment(CrolowFormEntry comment)
    {
        comment.Date = DateTime.Now;
        comment.Status = 0;
        comment.Additional = "";
        using var scope = _scopeProvider.CreateScope();
        scope.Database.Insert(comment);
        scope.Complete();

    }
}