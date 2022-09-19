using System;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.DbSqlite;
using System.IO;

namespace TabiTravel.XamarinAPP
{
    public partial class App : Application
    {
        private static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tabitravel.db3"));

                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();          
            
        }

      

        protected async override void OnStart()
        {
            await App.Database.GenerateUserGuideInfo();
            //await App.Database.GenerateUserTouristInfo();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
