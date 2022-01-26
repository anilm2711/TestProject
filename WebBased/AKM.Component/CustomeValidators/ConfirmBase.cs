using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKM.Component.CustomeValidators
{
    public class ConfirmBase:ComponentBase
    {
        [Parameter]
        public bool ShowConfirmation { get; set; } = false;

        [Parameter]
        public string ConfirmationTitle { get; set; } = "Confirm Delete";

        [Parameter]
        public string ConfirmationMessage { get; set; } = "Are you sure you want to delete";

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        public void Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }
        protected async Task OnConfirmationChange(bool result)
        {
            ShowConfirmation = false;
            ConfirmationChanged.InvokeAsync(result);
        }

    }
}
