using Bieren.BusinessLayer.Models;
using Bieren.WPF.Services;
using Bieren.WPF.ViewModels;
using Bieren.WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Windows;
using Bieren.WPF.Automapper;
using Bieren.DataLayer.Repositories;
using Bieren.BusinessLayer.Services;

namespace Bieren.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private readonly IMapper _mapper;
        public App()
        {
            var automapperConfig = new AutomapperConfig();
            var autoMapperconfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(automapperConfig);
            });
            _mapper = autoMapperconfiguration.CreateMapper();
            
            //services.AddAutoMapper(mapperConfig => mapperConfig.AddProfiles(GetType().Assembly))
            _host = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    // clear default logging providers
                    logging.ClearProviders();
                    // add built-in providers manually, as needed 
                    logging.AddConsole();
                    //logging.AddDebug();
                    logging.AddEventLog();
                    //logging.AddEventSourceLogger();
                })
                .ConfigureServices((context, services) => {
                    ConfigureServices(services);
                    
                })
                .Build();
        }
        private void ConfigureServices(IServiceCollection services)
        {

            //services.AddAutoMapper(mapperConfig => mapperConfig.AddProfiles(GetType().Assembly));
            //services.AddDbContext<BierenDbContext>(options =>
            //{
            //    options.UseSqlServer(ConfigurationManager.ConnectionStrings["BierenDbCon"].ConnectionString);//
            //});
            services.AddSingleton(_mapper);
            services.AddTransient<IDialogService, DialogService>();
            services.AddTransient<IFileDialogService, FileDialogWindow>();
            services.AddScoped<IBierenRepository, BierenRepository>();
            services.AddScoped<IBierSoortenRepository, BierSoortenRepository>();
            services.AddTransient<DialogWindow>();
            services.AddSingleton<MainWindow>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<BierenViewModel>();
            services.AddTransient<AandelenViewModel>();
            
            //services.AddSingleton<IPasswordHasher, PasswordHasher>();

            //services.AddSingleton<IAuthenticationService, AuthenticationService>();
            //services.AddSingleton<IAccountService, AccountDataService>();
            //services.AddTransient<AuthenticationService>();
            //services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDataService, BierenDataService>();
            services.AddHttpClient("stockMarket", c =>
            {
                c.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");

            });
            services.AddTransient<IMyHttpService, MyHttpService>();
            services.AddTransient<IStockExchangeService, StockExchangeService>();
         
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            //IAuthenticationService authentication = _host.Services.GetRequiredService<AuthenticationService>();
            //Account account = await authentication.Login("hcoppieters", "hcoppieters");
            ////await authentication.Register("hcoppieters@hotmail.com", "hcoppieters", "hcoppieters", "hcoppieters");
            //string beerAdjunctUrl = ConfigurationManager.AppSettings.Get("beerBaseUrl") + "adjuncts ? key=";
            //string apiKeyBeer = ConfigurationManager.AppSettings.Get("beerApiKey");
            //string apiKeyFinance = ConfigurationManager.AppSettings.Get("financeApiKey");
            await _host.StartAsync();
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainViewModel mainVm = _host.Services.GetRequiredService<MainViewModel>();
            AandelenViewModel aandelenVM = _host.Services.GetRequiredService<AandelenViewModel>();
            mainVm.AandelenVM = aandelenVM;
            mainWindow.DataContext = mainVm;

            mainWindow.Show();
           // mainVm.IsLoggedIn = false;
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
            base.OnExit(e);
        }
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    
        //    base.OnStartup(e);

        //    MainWindow window = new MainWindow();

        //    // Create the ViewModel to which 
        //    // the main window binds.
        //    IDataService dataService = new BierenDataService();
        //    IDialogService dialogService = new DialogService();
        //    IFileDialogService fileDialogService = new FileDialogWindow();
        //    var viewModel = new MainViewModel(dataService, dialogService, fileDialogService);

        //    // When the ViewModel asks to be closed, 
        //    // close the window.
        //    EventHandler handler = null;
        //    handler = delegate
        //    {
        //        viewModel.RequestClose -= handler;
        //        window.Close();
        //    };
        //    viewModel.RequestClose += handler;

        //    window.DataContext = viewModel;

        //    window.Show();
        //}
    }
}

