
using System;
using System.Collections.Generic;
using System.Text;

namespace Bieren.WPF.ViewModels.Factories
{
    public enum ViewType
    {
        Bieren,
        BierSoorten,
        Brouwers,
        Users  
    }
    public class BierenViewModelFactory : IBierenViewModelFactory
    {
        private readonly CreateViewModel<BierenViewModel> _createBierenViewModel;
        private readonly CreateViewModel<SoortenViewModel> _createSoortenViewModel;
        private readonly CreateViewModel<BrouwersViewModel> _createBrouwersViewModel;
        private readonly CreateViewModel<UsersViewModel> _createUsersViewModel;


        public BierenViewModelFactory(CreateViewModel<BierenViewModel> createBierenViewModel,
            CreateViewModel<SoortenViewModel> createSoortenViewModel, CreateViewModel<BrouwersViewModel> createBrouwersViewModel,
            CreateViewModel<UsersViewModel> createUsersViewModel)

        {
            _createBierenViewModel = createBierenViewModel;
            _createSoortenViewModel = createSoortenViewModel;
            _createBrouwersViewModel = createBrouwersViewModel;
            _createUsersViewModel = createUsersViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {

                case ViewType.Bieren:
                    return _createBierenViewModel();
                case ViewType.Brouwers:
                    return _createBrouwersViewModel();
                case ViewType.BierSoorten:
                    return _createSoortenViewModel();
                case ViewType.Users:
                    return _createUsersViewModel();
                default:
                    throw new ArgumentException($"Het ViewType {viewType} heeft geen ViewModel.", "viewType");
            }
        }
    }
}
