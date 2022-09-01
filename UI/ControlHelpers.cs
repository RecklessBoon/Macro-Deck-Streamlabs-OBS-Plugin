using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI
{
    public static class ControlHelpers
    {
        public static void InvokeIfRequired<T>(this T control, Action<T> action) where T : ISynchronizeInvoke
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => action(control)), null);
            }
            else
            {
                action(control);
            }
        }
    }
}
