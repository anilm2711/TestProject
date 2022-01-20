namespace TestBlazorApp.Client.StateContainer
{
    public class StateContainer
    {
        private string? savedString;
        private int clickCounter;

        public string Property
        {
            get => savedString ?? string.Empty;
            set
            {
                savedString = value;
                NotifyStateChanged();
            }
        }

        public int CounterProperty
        {
            get => clickCounter ;
            set
            {
                clickCounter = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
