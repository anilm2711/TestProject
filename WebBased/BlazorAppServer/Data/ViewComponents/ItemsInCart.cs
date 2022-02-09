namespace BlazorAppServer.Data.ViewComponents
{
    public class ItemsInCart
    {
        public string CurrentPageName { get; private set; }

        public void SetCurrentPageName(string name)
        {
            if (!string.Equals(CurrentPageName, name))
            {
                CurrentPageName = name;
                NotifyStateChanged();
            }
        }

        public event Action OnChange; // event raised when changed

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
    
}
