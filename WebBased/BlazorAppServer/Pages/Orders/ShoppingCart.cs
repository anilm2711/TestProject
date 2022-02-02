using EBazarAppServer.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAppServer.Pages.Orders
{
    public partial class ShoppingCart : ComponentBase
    {
        public ShoppingCartVM MyProperty { get; set; }
    }
}
