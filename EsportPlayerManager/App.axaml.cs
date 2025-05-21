using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using EsportPlayerManager.Repositorys;
using EsportPlayerManager.Services;
using EsportPlayerManager.ViewModels;
using EsportPlayerManager.Views;

namespace EsportPlayerManager;

public partial class App : Application
{
    private PlayerViewModel _playerViewModel;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            var connectionString = """
                                   Host=localhost;
                                   Port=5432;
                                   Username=dawidek;
                                   Password=1234;
                                   Database=esport_manager;
                                   """;

            var playerRepository = new PlayerRepository(connectionString);
            var playerService = new PlayerService(playerRepository);
            _playerViewModel = new PlayerViewModel(playerService);

            _ = playerRepository.InitDb().ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    Console.WriteLine($"DB ERROR: ${t.Exception}");
                }
            }, TaskScheduler.Default);
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = _playerViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}