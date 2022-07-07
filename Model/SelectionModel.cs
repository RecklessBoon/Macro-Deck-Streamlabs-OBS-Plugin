using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SelectionModel : ISelectionModel
    {
        [JsonProperty("lastSelectedId")]
        public string LastSelectedId { get; set; }

        [JsonProperty("selectedIds")]
        public string[] SelectedIds { get; set; }
    }
}
