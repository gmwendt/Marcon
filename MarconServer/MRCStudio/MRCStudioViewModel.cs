using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MRCStudio
{
  public class MRCStudioViewModel : ObservableObject
  {
    #region Fields

    private ICommand _changePageCommand;

    private IPageViewModel _currentPageViewModel;
    private List<IPageViewModel> _pageViewModels;

    #endregion

    public MRCStudioViewModel()
    {
      // Add available pages
      PageViewModels.Add(new HomeViewModel());
      PageViewModels.Add(new ProductsViewModel());

      // Set starting page
      if(CurrentUser != null)
        CurrentPageViewModel = PageViewModels[0];
      else
      {
        LoginWindow loginWindow = new LoginWindow(this);
        loginWindow.ShowDialog();
      }

    }

    #region Properties / Commands

    public ICommand ChangePageCommand
    {
      get
      {
        if (_changePageCommand == null)
        {
          _changePageCommand = new RelayCommand(
              p => ChangeViewModel((IPageViewModel)p),
              p => p is IPageViewModel);
        }

        return _changePageCommand;
      }
    }

    public List<IPageViewModel> PageViewModels
    {
      get
      {
        if (_pageViewModels == null)
          _pageViewModels = new List<IPageViewModel>();

        return _pageViewModels;
      }
    }

    public IPageViewModel CurrentPageViewModel
    {
      get
      {
        return _currentPageViewModel;
      }
      set
      {
        if (_currentPageViewModel != value)
        {
          _currentPageViewModel = value;
          OnPropertyChanged("CurrentPageViewModel");
        }
      }
    }

    public User CurrentUser;

    #endregion

    #region Methods

    private void ChangeViewModel(IPageViewModel viewModel)
    {
      if (!PageViewModels.Contains(viewModel))
        PageViewModels.Add(viewModel);

      CurrentPageViewModel = PageViewModels
          .FirstOrDefault(vm => vm == viewModel);
    }

    #endregion
  }
}
