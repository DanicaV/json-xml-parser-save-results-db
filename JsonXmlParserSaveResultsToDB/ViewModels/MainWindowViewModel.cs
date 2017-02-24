using Prism.Commands;
using Prism.Mvvm;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using JsonXmlParserSaveResultsToDB.Database;
using Newtonsoft.Json;

namespace JsonXmlParserSaveResultsToDB.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _filename;
        private string _fileparse;
        private string _filecontent;
        private string _fileparseValue;
        private string _popupOnCl;
        private string _popupValue;
        private string _popupOnCl2;
        private string _popupValue2;
        private string _popupOnCl3;
        private string _popupValue3;

        public string FilenameA
        {
            get { return _filename; }
            set { SetProperty(ref _filename, value); }
        }

        public string FileparseA
        {
            get { return _fileparse; }
            set { SetProperty(ref _fileparse, value); }
        }

        public string FileparseValue
        {
            get { return _fileparseValue; }
            set { SetProperty(ref _fileparseValue, value); }
        }

        public string PopupOnCl
        {
            get { return _popupOnCl; }
            set { SetProperty(ref _popupOnCl, value); }
        }

        public string PopupValue
        {
            get { return _popupValue; }
            set { SetProperty(ref _popupValue, value); }
        }

        public string PopupOnCl2
        {
            get { return _popupOnCl2; }
            set { SetProperty(ref _popupOnCl2, value); }
        }

        public string PopupValue2
        {
            get { return _popupValue2; }
            set { SetProperty(ref _popupValue2, value); }
        }

        public string PopupOnCl3
        {
            get { return _popupOnCl3; }
            set { SetProperty(ref _popupOnCl3, value); }
        }

        public string PopupValue3
        {
            get { return _popupValue3; }
            set { SetProperty(ref _popupValue3, value); }
        }

        public string FilecontentA
        {
            get { return _filecontent; }
            set { SetProperty(ref _filecontent, value); }
        }
        public Stream Stream
        {
            get;
            set;
        }

        public string Extension
        {
            get;
            set;
        }

        public string Filter
        {
            get;
            set;
        }

        public ICommand SumCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand OpenCommandString { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand ParseCommand { get; private set; }


        public MainWindowViewModel()
        {
            OpenCommandString = new DelegateCommand(onOpenString);
            ParseCommand = new DelegateCommand(onParse);
            SaveCommand = new DelegateCommand(onSave);
        }

        private void onSave()
        {
            using (var dc = new MenuContext())
            {
                string fileparseValue = FileparseValue;
                string popupValue = PopupValue;
                string popupOnCl = PopupOnCl;
                string popupValue2 = PopupValue2;
                string popupOnCl2 = PopupOnCl2;
                string popupValue3 = PopupValue3;
                string popupOnCl3 = PopupOnCl3;
                var menu = new Menu() { Value = fileparseValue };
                var menuitemNew = new Popup() { Value = popupValue, OnClick = popupOnCl };
                var menuitemNew2 = new Popup() { Value = popupValue2, OnClick = popupOnCl2 };
                var menuitemNew3 = new Popup() { Value = popupValue3, OnClick = popupOnCl3 };
                menu.Popups.Add(menuitemNew);
                menu.Popups.Add(menuitemNew2);
                menu.Popups.Add(menuitemNew3);
                dc.Menus.Add(menu);
                dc.SaveChanges();
                MessageBox.Show("snimljeno");
            }
        }

        private void onOpenString()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.Filter = "xml files (*.xml)|*.xml|JSON files (*.json)|*.json";
            dlg.Filter = "JSON, XML files (*.json,*.xml)|*.json; *.xml";
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                FilenameA = filename;
                FilecontentA = File.ReadAllText(filename);
            }

        }

        private void onParse()
        {
            dynamic jsonDe = JsonConvert.DeserializeObject(FilecontentA);
            foreach (var counter in jsonDe)
            {
                FileparseValue = jsonDe.menu.value;
                PopupValue = jsonDe.menu.popup.menuitem[0].value;
                PopupOnCl = jsonDe.menu.popup.menuitem[0].onclick;
                PopupValue2 = jsonDe.menu.popup.menuitem[1].value;
                PopupOnCl2 = jsonDe.menu.popup.menuitem[1].onclick;
                PopupValue3 = jsonDe.menu.popup.menuitem[2].value;
                PopupOnCl3 = jsonDe.menu.popup.menuitem[2].onclick;
                FileparseA = FileparseValue + Environment.NewLine + PopupValue + Environment.NewLine + PopupOnCl + Environment.NewLine
                    + FileparseValue + Environment.NewLine + PopupValue2 + Environment.NewLine + PopupOnCl2 + Environment.NewLine
                    + FileparseValue + Environment.NewLine + PopupValue3 + Environment.NewLine + PopupOnCl3 + Environment.NewLine;
            }

        }

    }

}

