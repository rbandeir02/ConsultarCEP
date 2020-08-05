using System;
using ClipboardDemo.Interfaces;
using Xamarin.Forms;
using Android.Content;
using ClipboardDemo.Droid.DepencencyServices;

[assembly: Dependency(typeof(ClipboardService))]
namespace ClipboardDemo.Droid.DepencencyServices
{
    public class ClipboardService : IClipboardService
    {
        public void CopyToClipboard(string text)
        {
            var clipboardManager = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);
            ClipData clip = ClipData.NewPlainText("Consultar CEP", text);
            clipboardManager.PrimaryClip = clip;
        }
    }
}