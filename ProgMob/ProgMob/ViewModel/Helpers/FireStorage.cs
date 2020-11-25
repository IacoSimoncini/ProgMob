using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;
using ProgMob.Models;

namespace ProgMob.ViewModel.Helpers
{
    public class HelpStorage
    {
        FirebaseStorage firebaseStorage = new FirebaseStorage("gs://progmobxam-af105.appspot.com/");
        User user;
        public HelpStorage(User loggedUser)
        {
            user = loggedUser;
        }

        public async void UploadFile(Stream fileStream)
        {
            await firebaseStorage
                .Child("ProfilesPic")
                .Child(user.Id)
                .Child("propic.jpg")
                .PutAsync(fileStream);
        }

        public async Task<string> GetPic()
        {
            return await firebaseStorage
                .Child("ProfilesPic")
                .Child(user.Id)
                .Child("propic.jpg")
                .GetDownloadUrlAsync();
        }

        public async Task<string> GetDefaultPic()
        {
            return await firebaseStorage
                .Child("Default")
                .Child("user.png")
                .GetDownloadUrlAsync();
        }

    }
}
