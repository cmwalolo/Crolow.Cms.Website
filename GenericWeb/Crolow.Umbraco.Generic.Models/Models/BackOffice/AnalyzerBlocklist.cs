using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Crolow.Cms.Core.Models.BackOffice
{

    public class AnalyzerBlocklist //this class is to mock the correct JSON structure when the object is serialized
    {
        public BlockListUdi layout { get; set; }
        public List<Dictionary<string, string>> contentData { get; set; }
        public List<Dictionary<string, string>> settingsData { get; set; }
    }
    public class BlockListUdi //this is a subclass which corresponds to the "Umbraco.BlockList" section in JSON
    {
        [JsonProperty("Umbraco.BlockList")]  //we mock the Umbraco.BlockList name with JsonPropertyAttribute to match the requested JSON structure
        public List<Dictionary<string, string>> contentUdi { get; set; }

        public BlockListUdi(List<Dictionary<string, string>> items)
        {
            this.contentUdi = items;
        }
    }
}
