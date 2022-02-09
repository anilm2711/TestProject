namespace BlazorAppServer.Data.ViewComponents
{
    public class SearchItems
    {
        public string CurrentSearchString { get; private set; }

        public void SetCurrentSearchString(string name)
        {
            if (!string.Equals(CurrentSearchString, name))
            {
                CurrentSearchString = name;
                NotifyStateChanged();
            }
        }

        public event Action OnChangeOfSearchString; // event raised when changed

        private void NotifyStateChanged() => OnChangeOfSearchString?.Invoke();
    }
}
