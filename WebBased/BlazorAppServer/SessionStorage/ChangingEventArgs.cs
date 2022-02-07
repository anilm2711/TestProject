namespace BlazorAppServer.SessionStorage
{
    public class ChangingEventArgs:ChangedEventArgs
    {
        public bool Cancel { get; set; }

    }
}
