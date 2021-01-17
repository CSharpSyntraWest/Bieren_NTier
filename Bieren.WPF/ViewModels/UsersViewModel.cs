using Bieren.BusinessLayer.Models;
using Bieren.BusinessLayer.Services;
using Bieren.WPF.Models;
using Bieren.WPF.Services;
using Bieren.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bieren.WPF.ViewModels
{
    public class UsersViewModel : WorkspaceViewModel
    {
        //private IDataService _dataService;
        private User _selectedUser;
        private ObservableCollection<User> _users;

        public UsersViewModel(IDataService dataService, IDialogService dialogService, IFileDialogService fileDialogService) : base(dataService, dialogService, fileDialogService)
        {
            base.DisplayName = "Users";
            //_dataService = dataService;
            _users = new ObservableCollection<User>(ObjectConverter.BO_UsersToUsers(_dataService.GeefAlleUsers()));
        }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set { OnPropertyChanged(ref _selectedUser, value); }
        }
        public ObservableCollection<User> Users
        {
            get { return _users; }
        }
    }
}
