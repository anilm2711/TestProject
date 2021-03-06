using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;
using BlazorAppServer.Pages.Actors;
using Newtonsoft.Json;

namespace BlazorAppServer.Pages
{
    public class MovieDetailsBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        public bool ShowFooter { get; set; } = true;

        [Inject]
        public IMoviesService service { get; set; }
        public Movie result { get; set; }

        public string movieavailability { get; set; }

        protected string ButtonText { get; set; } = "Hide Footer";
        protected string? CssClass { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                result =await  service.GetMovieByIdAsync(Convert.ToInt32(Id));
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void Button_Click()
        {
            if(ButtonText == "Hide Footer")
            {
                ButtonText = "Show Footer";
                CssClass = "hide-footer";
            }
            else
            {
                ButtonText = "Hide Footer";
                CssClass = null;
            }
        }
        protected int SelectedActorCount { get; set; } = 0;
        protected void ActorSelectionChanged(bool IsSelected)
        {
            if (IsSelected)
            {
                SelectedActorCount++;
            }
            else
            {
                SelectedActorCount--;
            }

        }

    }

}
