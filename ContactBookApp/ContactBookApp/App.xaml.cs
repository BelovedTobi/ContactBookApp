//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Xamarin.Forms;

//namespace ContactBookApp
//{
//	public partial class App : Application
//	{
//		public App ()
//		{
//			InitializeComponent();

//			MainPage = new ContactBookApp.MainPage();
//		}

//		protected override void OnStart ()
//		{
//			// Handle when your app starts
//		}

//		protected override void OnSleep ()
//		{
//			// Handle when your app sleeps
//		}

//		protected override void OnResume ()
//		{
//			// Handle when your app resumes
//		}
//	}
//}


using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ContactBookApp
{
    public partial class App : Application
    {
        private static ContactRepository contactRepository = null;

        public static ContactRepository ContactRepository
        {
            get
            {
                if (contactRepository == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContactDb.db");
                    return new ContactRepository(dbPath);
                }
                return contactRepository;
            }
        }


        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
