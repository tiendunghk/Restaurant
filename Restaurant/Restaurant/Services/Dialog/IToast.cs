using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Services.Dialog
{
    public interface IToast
    {
        void Show(string message);
    }
}
