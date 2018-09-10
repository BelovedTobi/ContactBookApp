//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xamarin.Forms;

//namespace ContactBookApp
//{
//	public partial class MainPage : ContentPage
//	{
//		public MainPage()
//		{
//			InitializeComponent();
//		}
//	}
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace ContactBookApp
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<ContactClass> Contacts = new ObservableCollection<ContactClass>();
        
        public MainPage()
        {
            InitializeComponent();

            MainList.ItemsSource = Contacts;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Contacts = new ObservableCollection<ContactClass>(await App.ContactRepository.GetAllContact());            
            MainList.ItemsSource = Contacts;
        }

        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            Loader.IsVisible = true;
            MainList.IsRefreshing = true;
            Contacts = new ObservableCollection<ContactClass>(await App.ContactRepository.GetAllContact());
            MainList.ItemsSource = Contacts;
            Loader.IsVisible = false;
            MainList.IsRefreshing = false;
        }

        async void AddContactClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NewContactPage());
        }

        private void EditContact_Clicked(object sender, EventArgs e)
        {
            //var listView = sender as ListView;
            //var contact = listView.SelectedItem as ContactClass;

            var item = sender as MenuItem;
            var contact = item.BindingContext as ContactClass;
            var contactPage = new EditContactPage();
            contactPage.BindingContext = contact;
            Navigation.PushAsync(contactPage);

            

        }
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var item = sender as MenuItem;
            var contact = item.BindingContext as ContactClass;
            //var choice = await DisplayAlert("Delete Contact", "Do you want to delete?", "Delete", "Stop");
            var choice = await DisplayActionSheet("Delete Contact", "Cancel", "Destruction", "Delete");

            switch (choice)
            {
                case "Delete":
                    try
                    {
                        await App.ContactRepository.DeleteContact(contact);
                        Contacts.Remove(contact);
                        await DisplayAlert("Success", $"{contact.Name} deleted successfully", "OK");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "OK");

                    }

                    break;
                default:                  
                    break;
            }
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            var contact = listView.SelectedItem as ContactClass;
            var contactPage = new ContactDetailPage();
            contactPage.BindingContext = contact;
            Navigation.PushAsync(contactPage);
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }

        
    }

}
