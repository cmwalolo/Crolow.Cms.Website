using Umbraco.Cms.Core.Strings;

namespace Crolow.Cms.Core.Models.ViewModel.Custom
{
    public class HeaderRollupModel
    {
        public string Image { get; set; }

        public List<IHtmlEncodedString> Items { get; set; }
        public string Template { get; set; }

    }
}
