using Microsoft.AspNetCore.Components;
using MovieScoutShared;

namespace MovieScout.Shared
{
    public partial class NavMenu
    {
        [Inject]
        public UserInfoGlobalClass userGlobal { get; set; }
        [Inject]
        AppState appState { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        protected override void OnInitialized()
        {
            appState.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            appState.OnChange -= StateHasChanged;
        }

        public void logout()
        {
            userGlobal.Token = "";
            userGlobal.Username = "";
            userGlobal.Password = "";
            appState.LoggedIn = false;
        }
    }
}
