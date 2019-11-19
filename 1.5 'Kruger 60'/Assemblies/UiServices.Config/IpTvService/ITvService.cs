using System.Windows.Forms;

namespace IpTviewr.UiServices.Configuration.IpTvService
{
    public interface ITvService
    {
        IEpgInfoProvider EpgInfo { get; }

        ITvServiceTexts Texts { get; }

        InitializationResult Initialize();
    } // interface ITvService
} // namespace