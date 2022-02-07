using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorAppServer.SessionStorage
{
    public class SyncSessionStorageService: ISyncSessionStorageService
    {
        private readonly IJSInProcessRuntime _jSRuntime;

        public event EventHandler<ChangedEventArgs> Changed;
        public event EventHandler<ChangingEventArgs> Changing;

        public SyncSessionStorageService(IJSInProcessRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public void Clear()
        {
            _jSRuntime.Invoke<object>("sessionStorage.clear");
        }

        public T GetItem<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (_jSRuntime == null)
            {
                throw new InvalidOperationException("IJSInProcessRuntime not valid");
            }
            var serialiseData = _jSRuntime.Invoke<string>("sessionStorage.getItem", key);
            if (serialiseData == null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(serialiseData);
        }

        public string Key(int index)
        {
            return _jSRuntime.Invoke<string>("sessionStorage.key", index);
        }

        public int Length()
        {
           return _jSRuntime.Invoke<int>("eval", "sessionStorage.length");
        }

        public void RemoveItem(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            _jSRuntime.Invoke<object>("sessionStorage.removeItem", key);
        }

        public void SetItem(string key, object data)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            var e = RaiseOnChangingSync(key, data);
            if (e.Cancel) return;
            _jSRuntime.InvokeAsync<object>("sessionStorage.setItem", key, JsonSerializer.Serialize(data));
            RaiseOnChangingSync(key, e.OldValue, data);
        }

        private ChangingEventArgs RaiseOnChangingSync(string key, object data)
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

        private void RaiseOnChangingSync(string key, object oldValue, object data)
        {
            var e = new ChangingEventArgs
            {
                Key = key,
                OldValue = oldValue,
                NewValue = data
            };
            Changing?.Invoke(this, e);
        }
    }
}
