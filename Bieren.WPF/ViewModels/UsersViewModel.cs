using AutoMapper;
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
        private IMapper _mapper;
        public UsersViewModel(IDataService dataService, IDialogService dialogService, IFileDialogService fileDialogService,IMapper mapper) : base(dataService, dialogService, fileDialogService)
        {
            _mapper = mapper;
            base.DisplayName = "Users";
            //_dataService = dataService;
            //_users = new ObservableCollection<User>(ObjectConverter.BO_UsersToUsers(_dataService.GeefAlleUsers()));
            _users = new ObservableCollection<User>(_mapper.Map<List<User>>(_dataService.GeefAlleUsers()));
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
