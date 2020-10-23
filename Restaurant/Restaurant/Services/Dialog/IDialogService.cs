using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Dialog
{
    public interface IDialogService
    {
        Task<int> DisplayActionSheetAsync(string title, string cancelButton, string destroyButton, params string[] otherButtons);
    }
}
