using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactBookApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditContactPage : ContentPage
	{
		public EditContactPage ()
		{
			InitializeComponent ();
		}

        private async Task HandleEdit_Clicked(object sender, EventArgs e)
        {
            var contact = new ContactClass
            {
                Name = NameEntry.Text,
                Email = EmailEntry.Text,
                Phone = PhoneEntry.Text,
                ImagePath = ImageEntry.Text,
                Id = int.Parse(IdEntry.Text)
            };

            try
            {
                int uu = 9;
                              
                await App.ContactRepository.UpdateContact(contact);
                await DisplayAlert("Congrats", "Contact Updated Successfully", "Continue");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Continue");
            }
        }

        private async void btnImagePicker_Clicked(object sender, EventArgs e)
        {
            try
            {
                var pickedFile = await CrossFilePicker.Current.PickFile();
                if (pickedFile != null)
                {
                    //lblImage.Text = pickedFile.FilePath;
                    ImageEntry.Text = pickedFile.FileName;

                    var saveFile = await CrossFilePicker.Current.SaveFile(pickedFile);


                    if (pickedFile.FileName.EndsWith("jpg", StringComparison.Ordinal)
                            || pickedFile.FileName.EndsWith("png", StringComparison.Ordinal))
                    {
                        FileImagePreview.Source = ImageSource.FromStream(() => pickedFile.GetStream());
                        FileImagePreview.IsVisible = true;

                        int yy = 0;
                        //string tes = "/MyImages/" + pickedFile.FileName;                        
                        string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), pickedFile.FileName);

                        ImageEntry.Text = dirPath;
                        
                        if (File.Exists(dirPath))
                            File.Delete(dirPath);

                        File.WriteAllBytes(dirPath, pickedFile.DataArray);                        

                    }
                    else
                    {
                        FileImagePreview.IsVisible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Message", ex.Message, "Cancel");
                //throw;
            }





        }

    }
}