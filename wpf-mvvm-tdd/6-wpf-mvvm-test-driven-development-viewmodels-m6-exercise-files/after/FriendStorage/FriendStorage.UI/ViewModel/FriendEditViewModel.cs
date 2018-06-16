using FriendStorage.Model;
using System;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.Command;
using System.Windows.Input;
using FriendStorage.UI.Wrapper;
using Prism.Events;
using FriendStorage.UI.Events;

namespace FriendStorage.UI.ViewModel
{
  public interface IFriendEditViewModel
  {
    void Load(int friendId);
    FriendWrapper Friend { get; }
  }
  public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
  {
    private IFriendDataProvider _dataProvider;
    private FriendWrapper _friend;
    private IEventAggregator _eventAggregator;

    public FriendEditViewModel(IFriendDataProvider dataProvider,
      IEventAggregator eventAggregator)
    {
      _dataProvider = dataProvider;
      _eventAggregator = eventAggregator;
      SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
    }

    public ICommand SaveCommand { get; private set; }

    public FriendWrapper Friend
    {
      get
      {
        return _friend;
      }
      set
      {
        _friend = value;
        OnPropertyChanged();
      }
    }

    public void Load(int friendId)
    {
      var friend = _dataProvider.GetFriendById(friendId);

      Friend = new FriendWrapper(friend);

      Friend.PropertyChanged += Friend_PropertyChanged;

      ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
    }

    private void Friend_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
    }

    private void OnSaveExecute(object obj)
    {
      _dataProvider.SaveFriend(Friend.Model);
      Friend.AcceptChanges();
      _eventAggregator.GetEvent<FriendSavedEvent>().Publish(Friend.Model);
    }

    private bool OnSaveCanExecute(object arg)
    {
      return Friend != null && Friend.IsChanged;
    }
  }
}
