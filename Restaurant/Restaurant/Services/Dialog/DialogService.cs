﻿using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Restaurant.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public async Task<int> DisplayActionSheetAsync(string title, string cancelButton, string destroyButton, params string[] otherButtons)
        {
            var result =
                await UserDialogs.Instance.ActionSheetAsync(title, cancelButton, destroyButton, null, otherButtons?.Select(x => x).ToArray());

            for (var i = 0; i < otherButtons?.Length; i++)
                if (otherButtons[i] == result)
                    return i;
            return -1;
        }
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }
        public void ShowToast(string message)
        {
            DependencyService.Get<IToast>().Show(message);
        }
        public Task<bool> ShowConfirmDialog(string tittle, string message, string oklabel, string cancellabel)
        {
            return UserDialogs.Instance.ConfirmAsync(message, tittle, oklabel, cancellabel);
        }
    }
}
