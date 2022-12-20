using Blazored.LocalStorage;
using VoorraadBeheer.Models;

namespace VoorraadBeheer.Data
{
    public class ProfileService
    {
        private readonly ILocalStorageService _localStorageService;

        public event Action<Preferences> OnChange;

        public ProfileService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task ToggleDarkMode()
        {
            var preferences = await GetPreferences();
            var newPreferences = preferences with { DarkMode = !preferences.DarkMode };
            await _localStorageService.SetItemAsync("preferences", newPreferences);

            OnChange?.Invoke(newPreferences);
        }

        public async Task<Preferences> GetPreferences()
        {
            return await _localStorageService.GetItemAsync<Preferences>("preferences")
                ?? new Preferences();
        }
    }
}
