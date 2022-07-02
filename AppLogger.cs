using SuchByte.MacroDeck.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin
{
    public static class AppLogger
    {
        public static void Error(string message, params object[] args)
        {
            if (args.Length > 0) message = String.Format(message, args);
            MacroDeckLogger.Error(PluginCache.Plugin, String.Format("[ERROR]: {0}", message.ToString()));
        }

        public static void Info(string message, params object[] args)
        {
            if (args.Length > 0) message = String.Format(message, args);
            MacroDeckLogger.Info(PluginCache.Plugin, String.Format("[INFO]: {0}", message.ToString()));
        }

        public static void Trace(string message, params object[] args)
        {
            if (args.Length > 0) message = String.Format(message, args);
            MacroDeckLogger.Trace(PluginCache.Plugin, String.Format("[TRACE]: {0}", message.ToString()));
        }

        public static void Warning(string message, params object[] args)
        {
            if (args.Length > 0) message = String.Format(message, args);
            MacroDeckLogger.Warning(PluginCache.Plugin, String.Format("[WARN]: {0}", message.ToString()));
        }
    }
}
