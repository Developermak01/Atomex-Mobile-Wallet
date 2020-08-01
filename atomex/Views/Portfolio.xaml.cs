﻿using System;
using Xamarin.Forms;
using SkiaSharp;
using atomex.ViewModel;
using atomex.CustomElements;
using System.Linq;

namespace atomex
{
    public partial class Portfolio : ContentPage
    {
        private CurrenciesViewModel _currenciesViewModel;

        private INavigationService _navigationService { get; }

        public Portfolio()
        {
            InitializeComponent();
        }

        public Portfolio(CurrenciesViewModel currenciesViewModel, INavigationService navigationService)
        {
            InitializeComponent();
            _currenciesViewModel = currenciesViewModel;
            _navigationService = navigationService;

            _currenciesViewModel.QuotesUpdated += (s, a) =>
            {
                Device.BeginInvokeOnMainThread(UpdateChart);   
            };

            BindingContext = currenciesViewModel;

            UpdateChart();
        }

        private void UpdateChart()
        {
            try
            {
                if (_currenciesViewModel.CurrencyViewModels != null)
                {
                    if (_currenciesViewModel.TotalCost == 0)
                    {
                        var entry = new Microcharts.Entry[]
                        {
                        new Microcharts.Entry(100)
                        {
                             Color = SKColor.Parse("#dcdcdc")
                        }
                        };

                        if (portfolioChart.Chart == null)
                            portfolioChart.Chart = new CustomDonutChart() { Entries = entry, HoleRadius = 0.6f, LabelTextSize = 20, FontFamily = "Roboto-Thin" };
                        else
                        {
                            var donutChart = portfolioChart.Chart as CustomDonutChart;
                            donutChart.Entries = entry;
                        }
                    }
                    else
                    {
                        var nonzeroWallets = _currenciesViewModel.CurrencyViewModels.Where(w => w.AvailableAmount != 0).ToList();
                        var entries = new Microcharts.Entry[nonzeroWallets.Count];
                        for (int i = 0; i < nonzeroWallets.Count; i++)
                        {
                            Random rnd = new Random();
                            entries[i] = new Microcharts.Entry(nonzeroWallets[i].PortfolioPercent)
                            {
                                Label = nonzeroWallets[i].CurrencyCode,
                                ValueLabel = string.Format("{0:0.#} %", nonzeroWallets[i].PortfolioPercent),
                                Color = SKColor.FromHsv(rnd.Next(256), rnd.Next(256), rnd.Next(256))
                            };
                        }

                        if (portfolioChart.Chart == null)
                            portfolioChart.Chart = new CustomDonutChart() { Entries = entries, HoleRadius = 0.6f, LabelTextSize = 20, FontFamily = "Roboto-Thin" };
                        else
                        {
                            var donutChart = portfolioChart.Chart as CustomDonutChart;
                            donutChart.Entries = entries;
                        }
                    }
                }
            }
            catch (Exception e) { }
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                _navigationService.ShowCurrency(e.Item as CurrencyViewModel);
            }
        }
    }
}
