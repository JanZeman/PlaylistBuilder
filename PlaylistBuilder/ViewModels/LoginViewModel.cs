namespace PlaylistBuilder.ViewModels;

public partial class LoginViewModel : ViewModel
{
	public LoginViewModel(ISpotifyService spotifyService)
	{
            this.spotifyService = spotifyService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        [ObservableProperty]
	    private bool showLogin;
        private readonly ISpotifyService spotifyService;

        [RelayCommand]
	    public void OpenLogin()
	    {
		    ShowLogin = true;
	    }

        [RelayCommand]
        public async void OpenHome()
        {
            IsBusy = true;

            try
            {
                if (await spotifyService.IsSignedIn())
                {
                    await Navigation.NavigateTo("//Home");
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }

            IsBusy = false;
        }

        public async Task HandleAuthCode(string code)
	    {
		    var result = await spotifyService.Initialize(code);

		    if(result)
            {
                var item = Shell.Current.CurrentItem;
			    await Navigation.NavigateTo("Home");
		    }
	    }
}

