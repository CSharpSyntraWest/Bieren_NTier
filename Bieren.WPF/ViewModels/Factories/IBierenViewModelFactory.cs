
using System;
using System.Collections.Generic;
using System.Text;

namespace Bieren.WPF.ViewModels.Factories
{
    public interface IBierenViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
