﻿using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace WpfReflector
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<string> _currentResults;
        private bool _includeFields;
        private bool _includeProperties;
        private bool _includeTypes;
        private string _targetTypeName;

        #endregion

        public MainViewModel()
        {
            IncludeFields = true;
            IncludeProperties = true;
            IncludeTypes = true;
            SearchAppDomainCommand = new DelegateCommand(SearchAppDomain);
            CopyToClipboardCommand = new DelegateCommand(CopyToClipboard);
            CopyTypeToClipboardCommand = new DelegateCommand(CopyTypeToClipboard);
        }

        #region Properties
        public ObservableCollection<String> CurrentResults
        {
            get 
            {
                if (_currentResults == null)
                {
                    _currentResults = new ObservableCollection<string>();
                }

                return _currentResults; 
            }
            set
            {
                _currentResults = value;
                NotifyPropertyChanged("CurrentResults");
            }
        }

        public bool IncludeFields
        {
            get { return _includeFields; }
            set
            {
                _includeFields = value;
                NotifyPropertyChanged("IncludeFields");
            }
        }

        public bool IncludeProperties
        {
            get { return _includeProperties; }
            set
            {
                _includeProperties = value;
                NotifyPropertyChanged("IncludeProperties");
            }
        }

        public bool IncludeTypes
        {
            get { return _includeTypes; }
            set
            {
                _includeTypes = value;
                NotifyPropertyChanged("IncludeTypes");
            }
        }

        public String TargetTypeName
        {
            get { return _targetTypeName; }
            set
            {
                _targetTypeName = value;
                NotifyPropertyChanged("TargetTypeName");
            }
        }

        public string SelectedResult { get; set; }

        public ICommand SearchAppDomainCommand { get; private set; }
        public ICommand CopyToClipboardCommand { get; private set; }
        public ICommand CopyTypeToClipboardCommand { get; private set; }

        #endregion

        #region Methods


        void CopyToClipboard()
        {
            Clipboard.SetText(SelectedResult);
        }

        void CopyTypeToClipboard()
        {
            var type = SelectedResult;
            var pos = type.LastIndexOf('.');
            if (pos > 0) type = type.Substring(pos + 1);
            Clipboard.SetText(type);
        }

        /**
         * Searches the WPF Framework for usages of the given type.
         * The kind of usages to be scanned for will be respected.
         * If the specified type is invalid, an error message will be toasted.
         * */
        public void SearchAppDomain()
        {
            CurrentResults.Clear();

            var type = AppDomainInterface.ResolveTypeName(TargetTypeName);

            if (type == null)
            {
                MessageBox.Show("TYPE NOT FOUND. ABORTED.");
                return;
            }

            if (IncludeFields)
            {
                var fields = AppDomainInterface.GetAllNamesOfFieldsOfType(type);
                
                foreach (var field in fields)
                {
                    CurrentResults.Add(field);
                }
            }

            if (IncludeProperties)
            {
                var properties = AppDomainInterface.GetAllNamesOfPropertiesOfType(type);

                foreach (var property in properties)
                {
                    CurrentResults.Add(property);
               }
            }

            if (IncludeTypes)
            {
                var subtypes = AppDomainInterface.GetAllNamesOfSubtypesOfType(type);

                foreach (var subtype in subtypes)
                {
                    CurrentResults.Add(subtype);
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}