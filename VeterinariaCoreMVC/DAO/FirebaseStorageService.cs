namespace VeterinariaCoreMVC.DAO
{
    using FirebaseAdmin;
    using Google.Apis.Auth.OAuth2;
    using Firebase.Storage;

    public class FirebaseStorageService
    {
        private readonly FirebaseStorage _firebaseStorage;

        //public FirebaseStorageService()
        //{
        //    // Inicializar Firebase Admin SDK
        //    FirebaseApp.Create(new AppOptions
        //    {
        //        Credential = GoogleCredential.FromFile("Credentials/firebase-adminsdk.json")
        //    });

        //    _firebaseStorage = new FirebaseStorage(
        //        "your-firebase-storage-bucket.appspot.com",
        //        new FirebaseStorageOptions
        //        {
        //            AuthTokenAsyncFactory = async () => await GetAuthTokenAsync(),
        //            ThrowOnCancel = true
        //        });
        //}

        //private async Task<string> GetAuthTokenAsync()
        //{
        //    var googleCredential = GoogleCredential.FromFile("Credentials/firebase-adminsdk.json");
        //    var accessToken = await googleCredential.GetAccessTokenForRequestAsync();
        //    return accessToken;
        //}

        //public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        //{
        //    var task = _firebaseStorage
        //        .Child("ImagenesProductos")
        //        .Child(fileName)
        //        .PutAsync(fileStream);

        //    return await task;
        //}
    }

}
