using Blazored.SessionStorage;
using static BlazorTableOfMultiply.Utils.ConstantsKeys;

namespace BlazorTableOfMultiply.Services;

public class CurrentVoiceOptions
{
    private readonly ISessionStorageService _sessionStorageService;

    public CurrentVoiceOptions(ISessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public async Task<VoiceOptions> LoadAsync(CancellationToken cancellationToken = default)
        => await _sessionStorageService.GetItemAsync<VoiceOptions>(VoiceOptionsKeyStore, cancellationToken);

    public async Task SaveAsync(VoiceOptions voiceOptions, CancellationToken cancellationToken = default)
        => await _sessionStorageService.SetItemAsync(VoiceOptionsKeyStore, voiceOptions, cancellationToken);
}
