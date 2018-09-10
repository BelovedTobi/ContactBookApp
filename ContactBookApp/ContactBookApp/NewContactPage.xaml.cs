using System;
using System.Collections.Generic;
using System.IO;
using Plugin.FilePicker;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace ContactBookApp
{
    public partial class NewContactPage : ContentPage
    {
        public NewContactPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var contact = new ContactClass
            {
                Name = NameEntry.Text,
                Email = EmailEntry.Text,
                Phone = PhoneEntry.Text,
                ImagePath = ImageEntry.Text
            };

            try
            {
                await App.ContactRepository.CreateContact(contact);
                await DisplayAlert("Congrats", "Contact Added Successfully", "Continue");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Continue");
            }
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{

        //}

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

                        //string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "/MyFolder");

                        //string dirPath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                        if (status != PermissionStatus.Granted)
                        {
                            if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                            {
                                await DisplayAlert("Need storage", "Request storage permission", "OK");
                            }

                            var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                            //Best practice to always check that the key exists
                            if (results.ContainsKey(Permission.Storage))
                                status = results[Permission.Storage];
                        }

                        //if (status == PermissionStatus.Granted)
                        //{
                        //    await DisplayAlert("Need storage", "Request storage permission granted", "OK");
                        //}


                            string tes = "/MyImages/" + pickedFile.FileName;
                        //string imageFullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "/MyImages/");
                        //if (!Directory.Exists(imageFullPath))
                        //Directory.CreateDirectory(imageFullPath);
                        //string dirPath = Path.Combine(temp, pickedFile.FileName);
                        string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), pickedFile.FileName);

                        ImageEntry.Text = dirPath;

                        lblImage.Text = dirPath;

                        if (File.Exists(dirPath))
                            File.Delete(dirPath);

                        File.WriteAllBytes(dirPath, pickedFile.DataArray);
                        //var notes = File.ReadAllText(Path.Combine(dirPath1, "notes.txt"));

                        //yy = 9;
                        // var dirPath = ContactBookApp.FilesDir + "/MyFolder";
                        //var exists = Directory.Exists(dirPath);
                        //var filepath = dirPath + pickedFile.FileName;
                        //if (!exists)
                        //{
                        //    Directory.CreateDirectory(dirPath);
                        //}
                        


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