﻿using SolidTA.Commons;
using SolidTA.Core;
using SolidTA.XmlUtils.XmlSerializers;
using SolidTA.XmlUtils.XmlModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SolidTA.DAL.MapProfiles;
using SolidTA.DAL.Entities;
using SolidTA.DAL.DataAccessServices;
using SolidTA.Interfaces;
using SolidTA.ExcelUtils.ExcelModels;
using SolidTA.ExcelUtils.ExcelExporters;

namespace SolidTA.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private DateTime _selectedDate;
        private string _processInfo;
        private string _excelPath;
        private bool _isStartButtonEnabled;
        public MainViewModel() 
        {
            SelectedDate = DateTime.Now;
            IsStartButtonEnabled = true;
        }


        #region Properties
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set 
            { 
                if(DateTime.Compare(DateTime.Today, value) < 0)
                {
                    _selectedDate = DateTime.Today;
                }
                else _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        public string ProcessInfo
        {
            get => _processInfo;
            set
            {
                _processInfo = value;
                OnPropertyChanged(nameof(ProcessInfo));
            }
        }
        public bool IsStartButtonEnabled
        {
            get => _isStartButtonEnabled;
            set
            {
                _isStartButtonEnabled = value;
                OnPropertyChanged(nameof(IsStartButtonEnabled));
            }
        }
        public string ExcelPath
        {
            get => _excelPath;
            set
            {
                _excelPath = value;
                OnPropertyChanged(nameof(ExcelPath));
            }
        }
        #endregion

        #region Commands
        public RelayCommand StartCommand => new RelayCommand(async o =>
        {
            IsStartButtonEnabled = false ;
            ExcelPath = string.Empty;
            ProcessInfo = "Загрузка данных с сайта ЦБ...";
            ValCurs valCurs = null;
            try
            {
                valCurs = await GetValCurs();
            }
            catch
            {
                IsStartButtonEnabled = true;
                ProcessInfo = "Не удалось получить данные с сайта ЦБ.\nПроверьте подключение к интернету";
                return;

            }

            ProcessInfo = "Загрузка данных в БД...";
            List<Rate> rateList = new List<Rate>();
            bool MapResult = RateMapper.Map(valCurs, out rateList);
            if (!MapResult) 
            {
                ProcessInfo="Corrupted data! Process terminated!";
                IsStartButtonEnabled = true;
                return;
            }
            bool dbCheck =await RateService.AddRates(rateList);

            ProcessInfo = "Формирование отчета в Excel...";
            List<IExcelModel> excelModels = new List<IExcelModel>();
            excelModels.Add(new ExcelCursModel(
                "RUB", 
                valCurs.Date, 
                1, 
                1));
            foreach(Rate rate in rateList)
            {
                excelModels.Add(new ExcelCursModel(
                    rate.Currency.CharCode,
                    rate.Date.ToShortDateString(),
                    rate.Nominal,
                    rate.Value));
            }
            foreach(IExcelModel model in excelModels)
            {
                model.SetCrossCurs(rateList);
            }
            CrosCursExporter crosCursExporter = new CrosCursExporter();
            crosCursExporter.SetExportData(excelModels);
            DateTime dateTime  = DateTime.Parse(valCurs.Date);
            string excelPathTemp = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}\\{dateTime:yyyy.MM.dd}.xlsx";
            var exportResult = crosCursExporter.Export(excelPathTemp);
            if(!exportResult)
            {
                if (!dbCheck)
                {
                    ProcessInfo = "Не удалость подключиться к БД.\nНе удалось сформировать отчет Excel!";
                 }
                else
                {
                    ProcessInfo = "Не удалось сформировать отчет Excel!";
                }
            }
            else
            {
                if (!dbCheck)
                {
                    ProcessInfo = "Не удалость подключиться к БД.\nОтчет в Excel cформирован";
                }
                else
                {
                    ProcessInfo = "Готово";
                }
                ExcelPath = excelPathTemp;

            }
            
            IsStartButtonEnabled = true;

        });
        #endregion

        #region Methods
        public async Task<ValCurs> GetValCurs()
        {
            
            HttpClient httpClient = new HttpClient();
            var responce = await httpClient.GetAsync(URL.GetUriString(SelectedDate));
            var content = await responce.Content.ReadAsStringAsync();
            return ValCursSerializer.Deserialize(content);

        }
        #endregion

    }
}
