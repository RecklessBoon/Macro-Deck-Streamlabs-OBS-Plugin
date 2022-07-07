using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public enum TSourceType
    {
        image_source,
        color_source,
        browser_source,
        slideshow,
        ffmpeg_source,
        text_gdiplus,
        text_ft2_source,
        monitor_capture,
        window_capture,
        game_capture,
        dshow_input,
        wasapi_input_capture,
        wasapi_output_capture,
        decklink_input,
        scene,
        ndi_source,
        openvr_capture,
        liv_capture,
        ovrstream_dc_source,
        vlc_source
    }
}
