using System.Diagnostics;
using Interfaces.ViewModels;
using JetBrains.Annotations;
using MVVM_Tools.Code.Commands;
using MVVM_Tools.Code.Providers;
using SaveToGameWpf.Logic.Utils;

namespace SaveToGameWpf.Logic.ViewModels
{
    public class AboutWindowViewModel : IAboutWindowViewModel
    {
        public IReadonlyProperty<string> Version { get; }

        public IActionCommand ShowDeveloperCommand { get; }
        public IActionCommand ThankDeveloperCommand { get; }
        public IActionCommand OpenAppDataFolderCommand { get; }
        public IActionCommand OpenSourcesPage { get; }

        public AboutWindowViewModel(
            [NotNull] ApplicationUtils applicationUtils,
            [NotNull] GlobalVariables globalVariables
        )
        {
            // properties
            Version = new DelegatedProperty<string>(applicationUtils.GetVersion, null).AsReadonly();

            // commands
            ShowDeveloperCommand = new ActionCommand(() =>
            {
                WebUtils.OpenLinkInBrowser("https://www.andnixsh.com/");
            });
            ThankDeveloperCommand = new ActionCommand(() =>
            {
                WebUtils.OpenLinkInBrowser("http://4pda.ru/forum/index.php?showtopic=477614&st=980");
            });
            OpenAppDataFolderCommand = new ActionCommand(() =>
            {
                Process.Start(globalVariables.AppDataPath);
            });
            OpenSourcesPage = new ActionCommand(() =>
            {
                WebUtils.OpenLinkInBrowser("https://github.com/AndnixSH/SaveToGame");
            });
        }
    }
}
