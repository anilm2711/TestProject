using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages
{
    public class CinemaDetailsBase:ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public ICinemasService service { get; set; }
        public Cinema result { get; set; }

        protected string ButtonText { get; set; } = "Hide Footer";
        protected string? CssClass { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            result = (await service.GetByIdAsync(Convert.ToInt32(Id)));
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
    }

}
