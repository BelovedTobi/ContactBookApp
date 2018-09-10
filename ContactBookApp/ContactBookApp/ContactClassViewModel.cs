using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLite;
using System.ComponentModel;


namespace ContactBookApp
{
    class ContactClassViewModel: ContactClass
    {
        public object getImageSource
        {
            get {
                return ImageSource.FromFile(ImagePath);
            }
        }
    }
}
