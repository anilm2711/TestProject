
using AKM.Component.CustomeValidators;
using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages
{
    public class DisplayActorBase:ComponentBase
    {
        [Inject]
        public IActorService service { get; set; }
        [Parameter]
        public Actor Actor { get; set; }

        [Parameter]
        public bool ShowFooter { get; set; }

        protected bool IsSelected { get; set; }

        [Parameter]
        public EventCallback<bool> OnActorSelection { get; set; }

        [Parameter]
        public EventCallback<int> OnActorDeleted { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ConfirmBase confirmBase { get; set; }    

        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            IsSelected = (bool)e.Value;
            await OnActorSelection.InvokeAsync(IsSelected);
        }

        protected async Task DeleteActor()
        {
            confirmBase.Show();
            //string Id = Convert.ToString(Actor.Id);
            //var result = service.DeleteAsync($"api/Actors/{Id}");
            //await OnActorDeleted.InvokeAsync(Actor.Id);
            
        }

        protected async Task ConfirmDelete_Click()
        {

        }

    }
}
