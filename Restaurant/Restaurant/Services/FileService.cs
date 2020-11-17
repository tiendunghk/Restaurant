using Acr.UserDialogs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Restaurant.Services.Dialog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Restaurant.Services
{
    public static class FileService
    {
        public static async Task<string> PickImageAsync(this IDialogService dialogService)
        {
            var path = string.Empty;
            var result = await dialogService.DisplayActionSheetAsync(
                "Vui lòng chọn",
                "Hủy",
                null,
                "Camera",
                "Thư viện");

            if (result == 1)
            {
                if (await RequestPermissionStatusAsync<Permissions.StorageRead>())
                {
                    await CrossMedia.Current.Initialize();
                    var file = await CrossMedia.Current.PickPhotoAsync(
                                    new PickMediaOptions
                                    {
                                        PhotoSize = PhotoSize.MaxWidthHeight,
                                        MaxWidthHeight = 1024
                                    });

                    if (file == null)
                        return path;
                    path = file.Path;
                }
            }
            else if (result == 0)
            {
                if (await RequestPermissionStatusAsync<Permissions.Camera>())
                {
                    await CrossMedia.Current.Initialize();
                    var file = await CrossMedia.Current.TakePhotoAsync(
                                new StoreCameraMediaOptions
                                {
                                    DefaultCamera = CameraDevice.Rear,
                                    PhotoSize = PhotoSize.Small,
                                    Directory = "Restaurant",
                                    SaveToAlbum = false,
                                    Name = $"media_{DateTime.Now.ToString()}.jpg"
                                });

                    if (file == null)
                        return path;
                    path = file.Path;
                }
            }
            return path;

        }
        public static async Task<bool> RequestPermissionStatusAsync<TPermission>() where TPermission : Permissions.BasePermission, new()
        {
            var permissionStatus = await Permissions.CheckStatusAsync<TPermission>();
            if (permissionStatus == PermissionStatus.Denied)
                if (Device.RuntimePlatform == Device.iOS)
                    return false;

            if (permissionStatus != PermissionStatus.Granted)
            {
                var newStatus = await Permissions.RequestAsync<TPermission>();

                return newStatus == PermissionStatus.Granted;
            }

            return true;
        }
        public static async Task<string> UploadImageCloudinary(string imgPath)
        {
            string output = string.Empty;
            Account account = new Account(
                        "ungdung-grocery-xamarin-by-dk",
                        "378791526477571",
                        "scsyCxQS_C74MbAGdOutpwrzlnU"
                        );

            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imgPath)
            };
            try
            {
                using (UserDialogs.Instance.Loading("Saving.."))
                {
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    output = uploadResult.SecureUri.ToString();
                }
            }
            catch (Exception)
            {

            }
            return output;
        }
    }
}
