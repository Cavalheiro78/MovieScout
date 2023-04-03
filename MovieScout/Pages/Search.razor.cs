using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MovieScout.Services;
using MovieScoutShared;

namespace MovieScout.Pages
{
    public partial class Search
    {
        public IEnumerable<Movie> movies { get; set; }
        [Inject]
        public IMovieDataService MovieDataService { get; set; }
        [Inject]
        public UserInfoGlobalClass userGlobal { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        private string? searchValue { get; set; }
        private int currentPage { get; set; }
        private Page resultsPage { get; set; }
        private string pagePickerVisibility { get; set; }
        private int inputPageNumber { get; set; }
        

        protected override void OnInitialized()
        {
            if (userGlobal.Token == "")
                NavigationManager.NavigateTo("/login");
            pagePickerVisibility = "none";
            inputPageNumber = 1;
        }

        public async Task SearchContentAsync() 
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                try { 

                    currentPage = 1;
                    resultsPage = await MovieDataService.GetSearchResults(searchValue, 1, userGlobal.Token);
                    if (resultsPage != null)
                        movies = resultsPage.results;
                }
                catch (Exception ex) { }
            }
        }

        public async Task ChangePageByIndex(int i)
        {
            try
            {
                if (resultsPage != null)
                    if(i <= resultsPage.total_results && i >= 1)
                    {
                        resultsPage = await MovieDataService.GetSearchResults(searchValue, i, userGlobal.Token);
                        movies = resultsPage.results;
                        currentPage = i;
                    }
            }
            catch (Exception ex) { }
        }
        public async Task ChangePagePrevious()
        {
            try
            {
                if (resultsPage != null)
                    if(resultsPage.page > 1) { 
                        resultsPage = await MovieDataService.GetSearchResults(searchValue, resultsPage.page-1, userGlobal.Token);
                        movies = resultsPage.results;
                        currentPage--;
                    }
            }
            catch (Exception ex) { }
        }

        public async Task ChangePageNext()
        {
            try
            {
                if (resultsPage != null)
                    if (resultsPage.page < resultsPage.total_pages)
                    {
                        resultsPage = await MovieDataService.GetSearchResults(searchValue, resultsPage.page+1, userGlobal.Token);
                        movies = resultsPage.results;
                        currentPage++;
                    }
            }
            catch (Exception ex) { }
        }

        public void ChangePagePickerVisibility()
        {
            if (pagePickerVisibility == "none")
                pagePickerVisibility = "block";
            else
                pagePickerVisibility = "none";
        }

        public async Task EnterAsync(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" || e.Key == "NumpadEnter")
            {
                await ChangePageByIndex(inputPageNumber);
                pagePickerVisibility = "none";
            }
        }
    }
}
