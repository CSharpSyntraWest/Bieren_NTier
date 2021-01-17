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
using System.Windows.Input;

namespace Bieren.WPF.ViewModels
{
    public class SoortenViewModel : WorkspaceViewModel
    {
        //private IDataService _dataService;
        private BierSoort _selectedSoort;
        private ObservableCollection<BierSoort> _soorten;
        private IMapper _mapper;
        public ICommand AddSoortCommand { get; private set; }
        public ICommand UpdateSoortCommand { get; private set; }
        public ICommand DeleteSoortCommand { get; private set; }
        public ObservableCollection<BierSoort> Soorten {
            get
            {
                return _soorten;
            }
            set
            {
               OnPropertyChanged(ref _soorten, value);
            }
        }
        public BierSoort SelectedSoort
        {
            get
            {
                return _selectedSoort;
            }
            set
            {
                OnPropertyChanged(ref _selectedSoort, value);
            }

        }

        public SoortenViewModel(IDataService dataService, IDialogService dialogService, IFileDialogService fileDialogService, IMapper mapper) : base(dataService, dialogService, fileDialogService)
        {
            _mapper = mapper;
           // _dataService = data;
            base.DisplayName = "Biersoorten";
            AddSoortCommand = new RelayCommand(VoegBierSoortToe);
            UpdateSoortCommand = new RelayCommand(WijzigBierSoort);
            DeleteSoortCommand = new RelayCommand(VerwijderBierSoort);
            //Soorten = new ObservableCollection<BierSoort>(ObjectConverter.BO_BierSoortenToBierSoorten(_dataService.GeefAlleBierSoorten()));
            Soorten = new ObservableCollection<BierSoort>(_mapper.Map<List<BierSoort>>((_dataService.GeefAlleBierSoorten())));
        }

        private void VerwijderBierSoort()
        {
            if (SelectedSoort == null) return;
            int selectedId = Soorten.IndexOf(SelectedSoort);
            //Soorten = new ObservableCollection<BierSoort>(ObjectConverter.BO_BierSoortenToBierSoorten(_dataService.VerwijderBierSoort(ObjectConverter.BiersoortToBO_Biersoort(SelectedSoort))));
            Soorten = new ObservableCollection<BierSoort>(_mapper.Map<List<BierSoort>>(_dataService.VerwijderBierSoort(_mapper.Map<BO_BierSoort>(SelectedSoort))));

            SelectedSoort = Soorten[selectedId-1];
        }

        private void WijzigBierSoort()
        {
            if (SelectedSoort == null) return;
            int selectedId = Soorten.IndexOf(SelectedSoort);
            //Soorten = new ObservableCollection<BierSoort>(ObjectConverter.BO_BierSoortenToBierSoorten(_dataService.WijzigBierSoort(ObjectConverter.BiersoortToBO_Biersoort(SelectedSoort))));
            Soorten = new ObservableCollection<BierSoort>(_mapper.Map<List<BierSoort>>(_dataService.WijzigBierSoort(_mapper.Map<BO_BierSoort>(SelectedSoort))));

            SelectedSoort = Soorten[selectedId];
        }

        private void VoegBierSoortToe()
        {
            SelectedSoort = Soorten[Soorten.Count - 1];
            BierSoort bierSoort = new BierSoort() { SoortNaam = "NA" };
            Soorten.Add(bierSoort);
            if (SelectedSoort == null) return;
            
            if(String.IsNullOrWhiteSpace(SelectedSoort.SoortNaam))
            {
                //To Boodschap geven naar gebruiker
                //Alert dialoogvenster tonen
                return;
            }
            //Biersoort toevoegen aan database;

            //Soorten = new ObservableCollection<BierSoort>(ObjectConverter.BO_BierSoortenToBierSoorten(_dataService.VoegBierSoortToe(ObjectConverter.BiersoortToBO_Biersoort(SelectedSoort))));
            Soorten = new ObservableCollection<BierSoort>(_mapper.Map<List<BierSoort>>(_dataService.VoegBierSoortToe(_mapper.Map<BO_BierSoort>(SelectedSoort))));

            SelectedSoort = Soorten[Soorten.Count - 1];
        }
    }
    
}
