﻿
using AutoMapper;
using Bieren.BusinessLayer.Models;
using Bieren.BusinessLayer.Services;
using Bieren.WPF.Models;
using Bieren.WPF.Services;
using Bieren.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Bieren.WPF.ViewModels
{
    public class BierenViewModel:WorkspaceViewModel
    {
        //private IFileDialogService _fileDialog;
        //private IDialogService _dialogService;
        //private IDataService _dataService;
        private ObservableCollection<Bier> _bieren;
        private ObservableCollection<BierSoort> _biersoorten;
        private ObservableCollection<Brouwer> _brouwers;
        private BierSoort _selectedBierSoort;
        private Brouwer _selectedBrouwer;
        private Bier _selectedBier;
        private IMapper _mapper;

        public BierenViewModel(IDataService dataService, IDialogService dialogService, IFileDialogService fileDialogService,IMapper mapper) : base(dataService, dialogService, fileDialogService)
        {
            base.DisplayName = "Bieren";
            _mapper = mapper;
            //_fileDialog = fileDialogService;
            //_dialogService = dialogService;
            //_dataService = dataService;
            //Bieren = new ObservableCollection<Bier>(ObjectConverter.BO_BierenToBieren(dataService.GeefAlleBieren()));
            Bieren = new ObservableCollection<Bier>(_mapper.Map<List<Bier>>(dataService.GeefAlleBieren()));
            //BierSoorten = new ObservableCollection<BierSoort>(ObjectConverter.BO_BierSoortenToBierSoorten(dataService.GeefAlleBierSoorten()));           
            BierSoorten = new ObservableCollection<BierSoort>(_mapper.Map<List<BierSoort>>(dataService.GeefAlleBierSoorten()));
            if (Brouwers == null)
            {
                //Brouwers = new ObservableCollection<Brouwer>(ObjectConverter.BO_BrouwersToBrouwers(dataService.GeefAlleBrouwers()));
                Brouwers = new ObservableCollection<Brouwer>(_mapper.Map<List<Brouwer>>(dataService.GeefAlleBrouwers()));
            }
            if (Bieren != null && Bieren.Count > 0)
            {
                SelectedBier = Bieren[0];
            }
            if (SelectedBier != null)
            {
                SelectedBierSoort = BierSoorten.Where(s => s.SoortNr== SelectedBier.BierSoort.SoortNr).SingleOrDefault();
                SelectedBrouwer = Brouwers.Where(b => b.BrouwerNr == SelectedBier.Brouwer.BrouwerNr).SingleOrDefault();
            }
            AddBierCommand = new RelayCommand(VoegBierToe);
            UpdateBierCommand = new RelayCommand(WijzigBierGegevens);
            DeleteBierCommand = new RelayCommand(VerwijderBier);
            //BrowseImageCommand = new RelayCommand(BrowseImage);
            OpenInputDialogCommand = new RelayCommand(OpenInputDialogBierSoort);

            //CollectionView bierenView = (CollectionView)CollectionViewSource.GetDefaultView(Bieren);
            //bierenView.Filter = BierenFilter;
        }



        //private string _filterText;
        //public string FilterText
        //{
        //    get { return _filterText; }
        //    set
        //    {
        //        OnPropertyChanged(ref _filterText, value);
        //        //TO REFRESH VAN VIEW
        //        CollectionViewSource.GetDefaultView(Bieren).Refresh();
        //    }
        //}
        //private bool BierenFilter(object bier)
        //{
        //    if (String.IsNullOrEmpty(FilterText))
        //        return true;
        //    else
        //        return ((bier as Bier).Naam.Contains(FilterText, StringComparison.OrdinalIgnoreCase));
        //}
        private void OpenInputDialogBierSoort()
        {
            string bierSoort =null;
            //Input Dialoogvenster tonen om een nieuwe biersoort toe te voegen
            InputDialogViewModel dialog = new InputDialogViewModel("Toevoegen biersoort", "Geef naam van biersoort:");

            var result = _dialogService.OpenDialog(dialog);
            // Debug.WriteLine(result);
            if (result != null)
            {
                if (result.ToString() != DialogResults.Cancel.ToString())
                    bierSoort = result.ToString();
            }
            if (bierSoort !=null) VoegBierSoortToe(bierSoort);
        }

        private void VoegBierSoortToe(string bierSoort)
        {           
            BierSoort biersoort = new BierSoort() { SoortNaam = bierSoort };
            // BierSoorten = new ObservableCollection<BierSoort>(ObjectConverter.BO_BierSoortenToBierSoorten(_dataService.VoegBierSoortToe(ObjectConverter.BiersoortToBO_Biersoort(biersoort))));
            //Dataservice aanroepen om een biersoort toe te voegen
            BierSoorten = new ObservableCollection<BierSoort>(_mapper.Map<List<BierSoort>>((_dataService.VoegBierSoortToe(_mapper.Map<BO_BierSoort>(biersoort)))));
            SelectedBier.BierSoort = BierSoorten[BierSoorten.Count - 1];
        }

        //private void BrowseImage()
        //{
        //    var filepad = _fileDialog.OpenFile("Image Files (*.gif;*.png;*.jpg)|*.gif;*.png;*.jpg");
        //    if (filepad != null)
        //    {

        //        string appPath = Environment.CurrentDirectory;
        //        FileInfo fileInfo = new FileInfo(filepad);
        //        string filename = fileInfo.Name;
        //        string destPath = Path.Combine(appPath + "\\Images", filename);
        //        if(!File.Exists(destPath)) File.Copy(filepad, destPath);
        //        SelectedBier.ImagePad = filename;
        //    }
        //}
        private void VerwijderBier()
        {
            //Vraag bevestiging om bier te verwijderen via YesNo Dialoogvenster
            YesNoDialogViewModel dialog = new YesNoDialogViewModel("Verwijderen Bier", $"Bent u zeker dat u Bier '{SelectedBier.Naam}' wilt verwijderen?" );

            DialogResults result = _dialogService.OpenDialog(dialog);
            Debug.WriteLine(result);
            if(result== DialogResults.Yes)
            {
                // Bieren = new ObservableCollection<Bier>(ObjectConverter.BO_BierenToBieren(_dataService.VerwijderBier(ObjectConverter.BierToBO_Bier(SelectedBier))));
                Bieren = new ObservableCollection<Bier>(_mapper.Map<List<Bier>>(_dataService.VerwijderBier(_mapper.Map<BO_Bier>(SelectedBier))));
                if (_bieren.Count > 0) SelectedBier = _bieren[0];
            }         
        }

        private void WijzigBierGegevens()
        {
           _dataService.WijzigBier(ObjectConverter.BierToBO_Bier(SelectedBier));
        }

        private void VoegBierToe()
        {
            SelectedBier = Bieren[Bieren.Count - 2];
            Bier bier = new Bier() { Naam = "Nieuw Bier", BierSoort =null, Brouwer =null };
            Bieren.Add(bier);
            SelectedBier = Bieren[Bieren.Count - 1];
            //Bieren = new ObservableCollection<Bier>(ObjectConverter.BO_BierenToBieren(_dataService.VoegBierToe(ObjectConverter.BierToBO_Bier(bier))));
            Bieren = new ObservableCollection<Bier>(_mapper.Map<List<Bier>>(_dataService.VoegBierToe(_mapper.Map<BO_Bier>(bier))));
            SelectedBier = Bieren[Bieren.Count - 1];
        }

        public ICommand AddBierCommand { get; private set; }
        public ICommand UpdateBierCommand { get; private set; }
        public ICommand DeleteBierCommand { get; private set; }
       // public ICommand BrowseImageCommand { get; private set; }
        public ICommand OpenInputDialogCommand { get; private set; }


        public ObservableCollection<Brouwer> Brouwers {
            get { return _brouwers; }
            set { OnPropertyChanged(ref _brouwers, value); }
        }
        public ObservableCollection<Bier> Bieren {
            get { return _bieren; }
            set { OnPropertyChanged(ref _bieren, value); }
        }
        public Brouwer SelectedBrouwer
        {
            get { return _selectedBrouwer; }
            set { OnPropertyChanged(ref _selectedBrouwer, value); }
        }
        public Bier SelectedBier
        {
            get { 
                return _selectedBier; 
            }
            set { 
                OnPropertyChanged(ref _selectedBier, value);
                if (value == null) return;
                if(SelectedBier.BierSoort != null)
                    SelectedBierSoort = BierSoorten.Where(s => s.SoortNr == SelectedBier.BierSoort.SoortNr).SingleOrDefault();
                if (SelectedBier.Brouwer != null)
                    SelectedBrouwer = Brouwers.Where(b => b.BrouwerNr == SelectedBier.Brouwer.BrouwerNr).SingleOrDefault();
            }
        }
        public ObservableCollection<BierSoort> BierSoorten
        {
            get { return _biersoorten; }
            set { OnPropertyChanged(ref _biersoorten, value); }
        }
        public BierSoort SelectedBierSoort
        {
            get { 
                return _selectedBierSoort; 
            }
            set { 
                OnPropertyChanged(ref _selectedBierSoort, value); 
            }
        }


    }
}
