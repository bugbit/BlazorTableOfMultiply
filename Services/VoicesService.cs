using Toolbelt.Blazor.SpeechSynthesis;

namespace BlazorTableOfMultiply.Services;

public class VoicesService
{
    private readonly SpeechSynthesis _speechSynthesis;
    private SpeechSynthesisVoice[]? _voices = null;
    private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public VoicesService(SpeechSynthesis speechSynthesis)
    {
        _speechSynthesis = speechSynthesis;
    }

    public async Task<SpeechSynthesisVoice[]> GetVoicesAsync()
    {
        SpeechSynthesisVoice[]? voices;

        await _semaphore.WaitAsync();
        try
        {
            voices = _voices;
            if (voices == null)
                _voices = voices = (await _speechSynthesis.GetVoicesAsync()).ToArray() ?? new SpeechSynthesisVoice[0];
        }
        finally
        {
            _semaphore.Release();
        }

        return voices;
    }

    public async Task<SpeechSynthesisVoice?> GetVoiceByCultureName(string cultureName)
    {
        var voices = await GetVoicesAsync();
        var voice =
            voices!.FirstOrDefault(v => string.Equals(v.Lang, cultureName))
            ?? GetVoiceBySplitCultureName();

        return voice;

        SpeechSynthesisVoice? GetVoiceBySplitCultureName()
        {
            var idx = cultureName.LastIndexOf("-");

            while (idx > 0)
            {
                var codIdioma = cultureName.Substring(0, idx - 1);

                var voice = voices.FirstOrDefault(v => string.Equals(v.Lang, codIdioma));

                if (voice != null)
                    return voice;

                idx = cultureName.LastIndexOf("-", 0, idx - 1);
            }

            return null;
        }
    }
}
