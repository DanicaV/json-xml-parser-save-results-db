using Prism.Commands;
using Prism.Mvvm;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using JsonXmlParserSaveResultsToDB.Database;
using Newtonsoft.Json;
using System.Xml;

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
        public string FilecontentP
        {
            get { return _filecontent; }
            set { SetProperty(ref _filecontent, value); }
        }

        public string FilecontentT
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
            try
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
                    MessageBox.Show("Podaci su uspešno snimljeni u bazu");

                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Neuspešno povezivanje sa bazom");

            }
        }

        private void onOpenString()
        {

            FilenameA = string.Empty;
            FilecontentA = string.Empty;
            FileparseA = string.Empty;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.Filter = "xml files (*.xml)|*.xml|JSON files (*.json)|*.json";
            dlg.Filter = "JSON, XML files (*.json,*.xml)|*.json; *.xml";
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                try
                {
                    FilenameA = dlg.FileName;
                    FilecontentA = File.ReadAllText(dlg.FileName);
                    FilecontentA = FilecontentA.Trim();

                }
                catch (IOException)
                {
                    MessageBox.Show("Nije moguce otvoriti odabrani fajl");
                }
            }
        }

        private void parseXML()
        {
            var popups = new Menu();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(FilecontentA);

            XmlNode GeneralInformationNode =
                          doc.SelectSingleNode("/menu");
            FileparseValue = GeneralInformationNode.Attributes.GetNamedItem("value").Value;
            XmlNode PopupListNode =
            doc.SelectSingleNode("/menu/popup");
            XmlNodeList PopupNodeList =
           PopupListNode.SelectNodes("menuitem");

            foreach (XmlNode node in PopupNodeList)
            {
                Popup aPopup = new Popup();
                aPopup.Value = node.Attributes.GetNamedItem("value").Value;
                aPopup.OnClick = node.Attributes.GetNamedItem("onclick").Value;
                popups.Popups.Add(aPopup);
                PopupValue = popups.Popups[0].Value;
                PopupOnCl = popups.Popups[0].OnClick;
            }

            PopupValue = popups.Popups[0].Value;
            PopupOnCl = popups.Popups[0].OnClick;
            PopupValue2 = popups.Popups[1].Value;
            PopupOnCl2 = popups.Popups[1].OnClick;
            PopupValue3 = popups.Popups[2].Value;
            PopupOnCl3 = popups.Popups[2].OnClick;
            FileparseA = FileparseValue + Environment.NewLine + PopupValue + Environment.NewLine + PopupOnCl + Environment.NewLine +
                  FileparseValue + Environment.NewLine + PopupValue2 + Environment.NewLine + PopupOnCl2 + Environment.NewLine +
                  FileparseValue + Environment.NewLine + PopupValue3 + Environment.NewLine + PopupOnCl3 + Environment.NewLine;
        }


        private void parseJSON()
        {
            dynamic jsonDe = JsonConvert.DeserializeObject(FilecontentA);
            foreach (var counter in jsonDe)
            {
                try
                {
                    FileparseValue = jsonDe.menu.value;

                }
                catch (Exception)
                {
                    MessageBox.Show("Nije moguce otvoriti odabrani fajl");

                }
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

        private void onParse()
        {

            if (FilecontentA.StartsWith("<") && FilecontentA.EndsWith(">"))
            {
                MessageBox.Show("Odabrali ste XML fajl, da li želite da ga parsirate?", "XML - Parsiranje");
                parseXML();

            }

            else if (FilecontentA.StartsWith("{") && FilecontentA.EndsWith("}"))
            {
                if (FilecontentA.Trim() == "{ }")
                {
                    FileparseA = "Prazan fajl";
                }
                else
                    MessageBox.Show("Odabrali ste JSON fajl, da li želite da ga parsirate?", "JSON - Parsiranje");
                parseJSON();

            }

            else
            {
                MessageBox.Show("Nije moguce parsirati odabrani fajl");
            }

        }
    }

}


