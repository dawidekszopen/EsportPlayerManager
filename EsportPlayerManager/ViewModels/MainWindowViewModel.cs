using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EsportPlayerManager.Services;

namespace EsportPlayerManager.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ICommand ShowPlayerCommand { get; }
    [ObservableProperty] private object? _currentView;


    [ObservableProperty] private bool _playerButtonEnabled;
    [ObservableProperty] private bool _tournamentsButtonEnabled = true;
    [ObservableProperty] private bool _trenningButtonEnabled = true;
    
    public MainWindowViewModel()
    {
        ShowPlayerCommand = new RelayCommand(ShowPlayers);
        CurrentView = new PlayersViewModel();
    }


    private void ShowPlayers()
    {
        CurrentView = new PlayersViewModel();
        TournamentsButtonEnabled = true;
        TrenningButtonEnabled = true;
    }    

}