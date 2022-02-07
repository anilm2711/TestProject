namespace BlazorAppServer.SessionStorage
{
    public interface ISessionStorageService
    {
        Task ClearAsync();
        Task<T> GetItemAsync<T>(string key);
        Task<string> KeyAsync(int index);
        Task RemoveItemAsync(string key);
        Task SetItemAsync(string key, object data);
        Task<int> LengthAsync();
        event EventHandler<ChangedEventArgs> Changed;
        event EventHandler<ChangingEventArgs> Changing;
    }
}
