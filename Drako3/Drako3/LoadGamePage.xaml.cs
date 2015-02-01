using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Drako3
{
    public partial class LoadGamePage : PhoneApplicationPage
    {
        List<string> files = new List<string>();
        private async Task<List<string>> GetFilesList()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            IReadOnlyList<StorageFile> files =await folder.GetFilesAsync();
            List<string> fileNames = new List<string>();
            foreach (StorageFile file in files)
            {
                string name=Regex.Replace(file.Name,@"\.sav","");
                fileNames.Add(name);
            }
            return fileNames;
        }
        private class ViewModel
        {
            private ObservableCollection<string> stringList = new ObservableCollection<string>();
            public ObservableCollection<string> StringList
            {
                get { return stringList; }
                set
                {
                    if (value == null)
                    {
                        stringList = new ObservableCollection<string>();
                    }
                    else
                        stringList = value;
                }
            }
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            files = await GetFilesList();
            base.OnNavigatedTo(e);

            ViewModel model = new ViewModel();
            model.StringList = new ObservableCollection<string>(files);
            this.DataContext = model;
        }
       
        public LoadGamePage()
        {
            InitializeComponent();
            //ViewModel model = new ViewModel();
            //var strings = new List<string> { "1", "2", "3" };
            //model.StringList = new ObservableCollection<string>(files);
            //this.DataContext = model;
           // string s = loadList.ItemsSource[0].ToString();
          //  StorageFolder folder=ApplicationData.Current.LocalFolder;
          //  IReadOnlyList<StorageFile> files= folder.GetFilesAsync();
            //StorageFolder folder = ApplicationData.Current.LocalFolder;
            //StorageFile file = await folder.GetFileAsync(fileName + ".sav");// (fileName + ".sav", CreationCollisionOption.OpenIfExists);
            //using (Stream readStream = await file.OpenStreamForReadAsync())
            //{
            //    DataContractSerializer serializer = new DataContractSerializer(typeof(DataType));
            //    DataType data;

            //    data = (DataType)serializer.ReadObject(readStream);

            //    return data;
           // }
        }

        private async void loadList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector list = sender as LongListSelector;
            String selected = list.SelectedItem.ToString();
            ClassToSerialize c = new ClassToSerialize();
            c=await MySerializer<ClassToSerialize>.LoadData(selected);
            gameDetails.Text = c.p1Name + ": " + c.p1Fraction + Environment.NewLine + c.p2Name + ": " + c.p2Fraction;
        }

        private  void loadGameButton_Click(object sender, RoutedEventArgs e)
        {
            
            String selected = loadList.SelectedItem.ToString();
            NavigationService.Navigate(new Uri("/PanoramaPage1.xaml?gameToLoad="+selected,UriKind.Relative));
            //  ClassToSerialize c = new ClassToSerialize();
          //  c = await MySerializer<ClassToSerialize>.LoadData(selected);
          //  Game g = new Game(c);
         //   PanoramaPage1 page = new PanoramaPage1(g);
            //page.NavigationService.Navigate(new PanoramaPage1());
            

        }
    }
}