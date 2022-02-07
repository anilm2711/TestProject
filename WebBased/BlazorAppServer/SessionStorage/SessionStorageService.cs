using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorAppServer.SessionStorage
{
    public class SessionStorageService :ISessionStorageService
    {
        private readonly IJSRuntime _jSRuntime;
        public event EventHandler<ChangedEventArgs> Changed;
        public event EventHandler<ChangingEventArgs> Changing;

        public SessionStorageService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }


        public async Task ClearAsync()=> await _jSRuntime.InvokeAsync<object>("sessionStorage.clear");

        public async Task<T> GetItemAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (_jSRuntime == null)
            {
                throw new InvalidOperationException("IJSInProcessRuntime not valid");
            }
            var serialiseData = await _jSRuntime.InvokeAsync<string>("sessionStorage.getItem", key);
            if (serialiseData == null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(serialiseData);
        }

        public async Task<string> KeyAsync(int index)=> await _jSRuntime.InvokeAsync<string>("sessionStorage.key", index);

        public async Task RemoveItemAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            await _jSRuntime.InvokeAsync<object>("sessionStorage.removeItem", key);
        }

      

        public async Task SetItemAsync(string key, object data)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            var e = RaiseOnChangingSync(key, data);
            if (e.Cancel) return;
            await _jSRuntime.InvokeAsync<object>("sessionStorage.setItem", key, JsonSerializer.Serialize(data));
            RaiseOnChangingSync(key, e.OldValue, data);
        }

        private ChangingEventArgs RaiseOnChangingSync(string key,object data)
        {
            var e = new ChangingEventArgs
            {
                Key = key,
                OldValue = ((ISessionStorageService)this).GetItemAsync<object>(key),
                NewValue = data
            };
            Changing?.Invoke(this, e);
            return e;
        }

        private void RaiseOnChangingSync(string key,object oldValue ,object data)
        {
            var e = new ChangingEventArgs
            {
                Key = key,
                OldValue = oldValue,
                NewValue = data
            };
            Changing?.Invoke(this, e);
        }

        public async Task<int> LengthAsync()
        {
           return  await _jSRuntime.InvokeAsync<int>("eval", "sessionStorage.length");
        }

    }
}
