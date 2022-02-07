namespace BlazorAppServer.SessionStorage
{
    public interface ISyncSessionStorageService
    {
        void Clear();

        T GetItem<T>(string key);

        string Key(int index);
        int Length();
        void RemoveItem(string key);
        void SetItem(string key, object data);

        event EventHandler<ChangedEventArgs> Changed;
        event EventHandler<ChangingEventArgs> Changing;
    }
}
