using ClipboardDemo.Interfaces;
using Xamarin.Forms;
using ClipboardDemo.iOS.Services;
using UIKit;
[assembly: Dependency(typeof(ClipboardService))]
namespace ClipboardDemo.iOS.Services
{
    public class ClipboardService : IClipboardService
    {
        public void CopyToClipboard(string text)
        {
            UIPasteboard clipboard = UIPasteboard.General;
            clipboard.String = text;
        }
    }
}