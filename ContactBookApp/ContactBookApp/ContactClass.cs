//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ContactBookApp
//{
//    class ContactClass
//    {
//    }
//}


using System;
using SQLite;
using System.ComponentModel;
using Xamarin.Forms;

namespace ContactBookApp
{
    public class ContactClass : INotifyPropertyChanged
    {
        private string name;
        private string phone;
        private string email;
        private string imagePath;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public ContactClass()
        {
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }


        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string ImagePath
        {
            get => imagePath;
            set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }


        [Ignore]
        public string Info
        {
            get
            {
                return $"{Name}, {Phone}, {Email}";
            }
        }


        [Ignore]
        public object getImageSource
        {
            get
            {
                return ImageSource.FromFile(ImagePath);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}