using System;
using FriendStorage.UI.ViewModel;
using Xunit;
using Moq;

namespace FriendStorage.UITests.ViewModel
{
  public class MainViewModelTests
  {
    private Mock<INavigationViewModel> _navigationViewModelMock;
    private MainViewModel _viewModel;

    public MainViewModelTests()
    {
      _navigationViewModelMock = new Mock<INavigationViewModel>();
      _viewModel = new MainViewModel(_navigationViewModelMock.Object);
    }

    [Fact]
    public void ShouldCallTheLoadMethodOfTheNavigationViewModel()
    {
      _viewModel.Load();

      _navigationViewModelMock.Verify(vm => vm.Load(), Times.Once);
    }
  }
}
