using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Models;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Cms.Web.Website.Controllers;

namespace Crolow.Cms.Core.Controllers.Surface
{
    public class MemberLoginController : SurfaceController
    {
        private readonly IMemberManager _memberManager;
        private readonly IMemberService _memberService;
        private readonly IMemberSignInManager _memberSignInManager;
        private readonly ICoreScopeProvider _coreScopeProvider;

        public MemberLoginController(
            IMemberManager memberManager,
            IMemberService memberService,
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider,
            IMemberSignInManager memberSignInManager,
            ICoreScopeProvider coreScopeProvider)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _memberManager = memberManager;
            _memberService = memberService;
            _memberSignInManager = memberSignInManager;
            _coreScopeProvider = coreScopeProvider;
        }

        [HttpPost()]
        public async Task<IActionResult> DoLogin([Bind(Prefix = "loginModel")] LoginModel model)
        {
            var member = await _memberManager.FindByNameAsync(model.Username);
            if (member == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return CurrentUmbracoPage();
            }

            var result = await _memberSignInManager.PasswordSignInAsync(
                model.Username, model.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    ModelState.AddModelError("", "Your account is locked.");
                else if (result.IsNotAllowed)
                    ModelState.AddModelError("", "Login not allowed.");
                else
                    ModelState.AddModelError("", "Invalid username or password.");

                return CurrentUmbracoPage();
            }


            if (!string.IsNullOrEmpty(model.RedirectUrl))
            {
                return Redirect(model.RedirectUrl!);
            }
            // Redirect to a member area page
            return RedirectToCurrentUmbracoPage();
        }

    }
}