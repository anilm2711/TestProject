
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages
{
    public class DisplayActorBase:ComponentBase
    {
        [Parameter]
        public Actor Actor { get; set; }

        [Parameter]
        public bool ShowFooter { get; set; }

        protected bool IsSelected { get; set; }

        [Parameter]
        public EventCallback<bool> OnActorSelection { get; set; }

        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            IsSelected = (bool)e.Value;
            await OnActorSelection.InvokeAsync(IsSelected);
        }
    }
}
