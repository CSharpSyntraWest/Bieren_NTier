using AutoMapper;
using Bieren.BusinessLayer.Services;
using Bieren.WPF.Models;
using Bieren.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Bieren.WPF.ViewModels
{
    public class AandelenViewModel:ObservableObject
    {
        private IStockExchangeService _stockService;
        private IMapper _mapper;
        private Aandeel _stockInfo;
        public AandelenViewModel(IStockExchangeService stockExchangeService,IMapper mapper)
        {
            _mapper = mapper;
            _stockService = stockExchangeService;
            GetStockInfo("ABI.BR");
        }
        public Aandeel StockInfo
        {
            get { return _stockInfo; }
            set { 
                OnPropertyChanged(ref _stockInfo, value); 
            }
        }
        private void GetStockInfo(string Id)
        {

                _stockService.GeefAandeelInfo(Id).ContinueWith(task =>
                {
                    if (task.Exception == null)
                    {
                        StockInfo = _mapper.Map<Aandeel>(task.Result);
                    }
                });
                //var httpService = services.GetRequiredService<IMyHttpService>();
                //var stockService = services.GetRequiredService<IStockExchangeService>();
               // var result = await _stockService.GeefAandeelInfo(Id);
                //Console.WriteLine(result);

        }

    }
}
