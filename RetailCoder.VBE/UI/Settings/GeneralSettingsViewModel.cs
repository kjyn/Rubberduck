﻿using System.Collections.ObjectModel;
using System.Linq;
using Rubberduck.Settings;

namespace Rubberduck.UI.Settings
{
    public class GeneralSettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        private readonly Configuration _config;

        public GeneralSettingsViewModel(Configuration config)
        {
            _config = config;

            Languages = new ObservableCollection<DisplayLanguageSetting>(
                new[] 
            {
                new DisplayLanguageSetting("en-US"),
                new DisplayLanguageSetting("fr-CA"),
                new DisplayLanguageSetting("de-DE"),
                new DisplayLanguageSetting("sv-SE"),
                new DisplayLanguageSetting("ja-JP")
            });

            SelectedLanguage = Languages.First(l => l.Code == _config.UserSettings.LanguageSetting.Code);
        }

        public ObservableCollection<DisplayLanguageSetting> Languages { get; set; } 

        private DisplayLanguageSetting _selectedLanguage;
        public DisplayLanguageSetting SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (!Equals(_selectedLanguage, value))
                {
                    _selectedLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        public void UpdateConfig(Configuration config)
        {
            config.UserSettings.LanguageSetting = SelectedLanguage;
        }
    }
}