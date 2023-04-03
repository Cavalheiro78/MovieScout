using Microsoft.AspNetCore.Components;
using MovieScout.Services;
using MovieScoutShared;
using System;

namespace MovieScout.Pages
{
    public partial class LoginForm
    {
        [Inject]
        public UserInfoGlobalClass userGlobal { get; set; }
        [Inject]
        public IMovieDataService MovieDataService { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        AppState appState { get; set; }

        public bool showLogin { get; set; } = true;
        public string usernameLogin { get; set; }
        public string passwordLogin { get; set; }
        public string usernameRegister { get; set; }
        public string passwordRegister { get; set; }
        public string emailRegister { get; set; }

        void changeShowLoginState()
        {
            if (showLogin)
                showLogin = false;
            else
                showLogin = true;
        }

        async Task loginAsync()
        {
            if (usernameLogin != null && passwordLogin != null)
            {
                var token =  await MovieDataService.Authenticate(usernameLogin, passwordLogin);
                if(token != null)
                {
                    string userIdString = await MovieDataService.GetUserId(usernameLogin);
                    userGlobal.Id = int.Parse(userIdString);
                    userGlobal.Username = usernameLogin;
                    userGlobal.Password = passwordLogin;
                    userGlobal.Token = token;
                    appState.LoggedIn = true;
                    NavigationManager.NavigateTo("/");
                }
            }
        }
        
        void register()
        {
            if (usernameRegister != null && passwordRegister != null && emailRegister != null)
            {
                MovieDataService.RegisterUser(usernameRegister, passwordRegister, emailRegister);
                showLogin = true;
            }
        }
    }
}
