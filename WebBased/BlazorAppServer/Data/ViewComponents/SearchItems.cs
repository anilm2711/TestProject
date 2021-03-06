
namespace BlazorAppServer.Data.ViewComponents
{
    public class SearchItems
    {
        public string CurrentSearchString { get; private set; }

        public void SetCurrentSearchString(string name)
        {
            CurrentSearchString = name;
            NotifyStateChanged();
        }

        public event Action OnChange; // event raised when changed

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
