using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services
{
    public class BaseService
    {
        public static string FromPascalToCamelCase(string pascalString) => Char.ToLowerInvariant(pascalString[0]) + pascalString.Replace("Async", "")[1..];

        public static async Task MakeCallAsync(string resource, object arg, [CallerMemberName] string method = "") => await MakeCallAsync(resource, new object[1] { arg }, method);

        public static async Task MakeCallAsync(string resource, object[] args = default, [CallerMemberName] string method = "")
        {
            object param_args = new
            {
                resource
            };

            if (args != null)
            {
                param_args = new
                {
                    resource,
                    args
                };
            }

            await PluginCache.Dispatcher.WriteAsync(new JsonRpcRequest
            {
                Method = FromPascalToCamelCase(method),
                Params = param_args
            });
        }

        public static async Task<T> MakeCallAsync<T>(string resource, object arg, [CallerMemberName] string method = "") => await MakeCallAsync<T>(resource, new object[1] { arg }, method);

        public static async Task<T> MakeCallAsync<T>(string resource, object[] args = default, [CallerMemberName] string method = "")
        {
            object param_args = new
            {
                resource
            };

            if (args != null)
            {
                param_args = new
                {
                    resource,
                    args
                };
            };

            var response = await PluginCache.Dispatcher.WriteAsync(new JsonRpcRequest
            {
                Method = FromPascalToCamelCase(method),
                Params = param_args
            });

            var result = response.Result;
            if (result.Type == JTokenType.Object)
            {
                return result.ToObject<RPCResult>().Data.ToObject<T>();
            } else if (result.Type == JTokenType.Array)
            {
                return result.ToObject<T>();
            }

            AppLogger.Error("Unexpected response type received: {0}", response);
            return default;
        }

        protected void AddSubscriber<T>(EventHandler<T> handler, [CallerMemberName] string propertyName = "")
        {
            PluginCache.Dispatcher.Subscribe(this.GetType().Name, FromPascalToCamelCase(propertyName), handler);
        }

        protected void RemoveSubscriber<T>(EventHandler<T> handler, [CallerMemberName] string propertyName = "")
        {
            PluginCache.Dispatcher.Unsubscribe(this.GetType().Name, FromPascalToCamelCase(propertyName));
        }
    }
}
