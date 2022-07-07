using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class StreamingState
    {
        [JsonProperty("recordingStatus")]
        public ERecordingState RecordingState { get; set; }

        [JsonProperty("recordingStatusTime")]
        public string RecordingStatusTime { get; set; }

        [JsonProperty("replayBufferStatus")]
        public EReplayBufferState ReplayBufferState { get; set; }

        [JsonProperty("replayBufferStatusTime")]
        public string ReplayBufferStatusTime { get; set; }

        [JsonProperty("streamingStatus")]
        public EStreamingState StreamingStatus { get; set; }

        [JsonProperty("streamingStatusTime")]
        public string StreamingStatusTime { get; set; }
    }
}
