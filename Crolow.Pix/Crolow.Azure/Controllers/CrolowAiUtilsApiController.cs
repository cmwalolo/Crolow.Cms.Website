using Asp.Versioning;
using Crolow.AzureServices.Interfaces;
using Crolow.AzureServices.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.Attributes;

namespace Crolow.AzureServices.Controllers
{
    [Route("api/v{version:apiVersion}/crolow.openai")]
    [ApiController]
    [ApiVersion("1.0")]
    [MapToApi("v1/crolow.openai")]
    public class CrolowAiUtilsApiController : Controller
    {
        private readonly ICrolowOpenAiService aiService;

        public CrolowAiUtilsApiController(ICrolowOpenAiService aiService)
        {
            this.aiService = aiService;
        }

        // Controller actions
        [HttpPost("generate-image")]
        public string GenerateImage([FromBody] ImageGenerationRequest request)
        {
            return aiService.GenerateImage(request).ImageUri.AbsoluteUri;
        }

        [HttpPost("describe-image")]
        public string DescribeImage([FromBody] ImageDescriptionRequest request)
        {
            return string.Join("n", aiService.DescribeImage(request).Content.Select(p => p.Text));
        }

        [HttpPost("translate-text")]
        public string TranslateText([FromBody] TranslationRequest request)
        {
            return string.Join("n", aiService.TranslateText(request).Content.Select(p => p.Text));
        }

        [HttpPost("correct-text")]
        public string CorrectText([FromBody] CorrectionRequest request)
        {
            return string.Join("n", aiService.CorrectText(request).Content.Select(p => p.Text));
        }

        [HttpPost("summarize-text")]
        public string SummarizeText([FromBody] SummarizeRequest request)
        {
            return string.Join("n", aiService.SummarizeText(request).Content.Select(p => p.Text));
        }

        [HttpPost("create-hashtags")]
        public string CreateHashTags([FromBody] CreateHashTagsRequest request)
        {
            return string.Join("n", aiService.CreateHashTags(request).Content.Select(p => p.Text));
        }
    }
}