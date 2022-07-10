using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC;
using System;
using System.Collections.Generic;
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
                var string_args = new List<string>();
                foreach(var arg in args)
                {
                    foreach (var property in arg.GetType().GetProperties())
                    {
                        var val = property.GetValue(arg);
                        if (val.GetType() == typeof(object)) 
                        {
                            string_args.Add(JsonConvert.SerializeObject(property.GetValue(arg)));
                        }
                        else
                        {
                            string_args.Add(val.ToString());
                        }
                    }
                }

                param_args = new
                {
                    resource,
                    args = string_args
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
                var string_args = new List<string>();
                foreach (var arg in args)
                {
                    foreach (var property in arg.GetType().GetProperties())
                    {
                        var val = property.GetValue(arg);
                        if (val.GetType() == typeof(object))
                        {
                            string_args.Add(JsonConvert.SerializeObject(property.GetValue(arg)));
                        }
                        else
                        {
                            string_args.Add(val.ToString());
                        }
                    }
                }

                param_args = new
                {
                    resource,
                    args = string_args
                };
            };

            var response = await PluginCache.Dispatcher.WriteAsync(new JsonRpcRequest
            {
                Method = FromPascalToCamelCase(method),
                Params = param_args
            });

            var result = response.Result;
            if (result.Type == JTokenType.Array)
            {
                return result.ToObject<T>();
            }
            else if (result.Type == JTokenType.Object)
            {
                if (result["data"] != null)
                {
                    return result.ToObject<RPCResult>().Data.ToObject<T>();
                } 
                else if (result["emitter"]?.ToString() == "PROMISE")
                {
                    var resourceId = result["resourceId"].ToString();
                    EventHandler<MessageDispatchedArgs> handler;
                    TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
                    handler = (object sender, MessageDispatchedArgs args) => 
                    {
                        var response = args.Response;
                        if (response.Result["emitter"]?.ToString() == "PROMISE" && response.Result["resourceId"]?.ToString() == resourceId)
                        {
                            if (response.Result["data"] != null) {
                                tcs.SetResult(response.Result["data"].ToObject<T>());
                            } else
                            {
                                AppLogger.Error("Failed to resolve promise appropriately:\n{0}", response);
                                tcs.SetCanceled();
                            }
                        }
                    };

                    PluginCache.Dispatcher.OnEventDispatched += handler;
                    return await tcs.Task;
                }
                else
                {
                    return result.ToObject<T>();
                }
            }
            else
            {
                return result.ToObject<T>();
            }
        }

        protected void AddSubscriber(EventHandler handler, [CallerMemberName] string propertyName = "")
        {
            PluginCache.Dispatcher.Subscribe(this.GetType().Name, FromPascalToCamelCase(propertyName), handler);
        }

        protected void RemoveSubscriber(EventHandler handler, [CallerMemberName] string propertyName = "")
        {
            PluginCache.Dispatcher.Unsubscribe(this.GetType().Name, FromPascalToCamelCase(propertyName));
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
