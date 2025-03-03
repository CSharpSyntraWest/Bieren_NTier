﻿using Bieren.BusinessLayer.Models;
using Bieren.BusinessLayer.Services;
using Bieren.WPF.Services;
using Bieren.WPF.Utilities;
using System;
using System.Windows.Input;

namespace Bieren.WPF.ViewModels
{
    /// <summary>
    /// This ViewModelBase subclass requests to be removed 
    /// from the UI when its CloseCommand executes.
    /// This class is abstract.
    /// </summary>
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        #region Fields

        ICommand _closeCommand;
        protected IFileDialogService _fileDialog;
        protected IDialogService _dialogService;
        protected IDataService _dataService;
        #endregion // Fields

        #region Constructor


        protected WorkspaceViewModel(IDataService dataService, IDialogService dialogService, IFileDialogService fileDialog)
        {
            _dataService = dataService;
            _fileDialog = fileDialog;
            _dialogService = dialogService;
        }
        #endregion // Constructor

        #region CloseCommand

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(OnRequestClose);

                return _closeCommand;
            }
        }

        #endregion // CloseCommand

        #region RequestClose [event]

        /// <summary>
        /// Raised when this workspace should be removed from the UI.
        /// </summary>
        public event EventHandler RequestClose;

        void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion // RequestClose [event]
    }
}