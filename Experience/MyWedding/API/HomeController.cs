using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;

namespace MyWedding.API
{
    public static class HomeController
    {
        public static void SendMessage(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/ChucMung/Index", async (context) =>
            {
                var suggestionTitle = Convert.ToString(context.Request.Form["SuggestionTitle"]);
                var suggestionContent = Convert.ToString(context.Request.Form["SuggestionContent"]);

                context.Response.Redirect($"/Admin/Suggestion/Index?SuggestionTitle={suggestionTitle}&SuggestionContent={suggestionContent}&handler=Update");
            });
        }
        public static void InsertGuestConnect(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/GuestConnect/Index", async (context) =>
            {
                context.Response.Redirect($"/Admin/GuestConnect/Index?handler=UpdateToken");
            });
        }
    }
}
