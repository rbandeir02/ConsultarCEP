﻿//using Foundation;
using Foundation;
using VersionAndBuildNumber.DependencyServices;
using VersionAndBuildNumber.iOS.DependencyServices;
using Xamarin.Forms;
[assembly: Dependency(typeof(VersionAndBuild_iOS))]
namespace VersionAndBuildNumber.iOS.DependencyServices
{
    public class VersionAndBuild_iOS : IAppVersionAndBuild
    {
        public string GetVersionNumber()
        {
            //var VersionNumber = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleShortVersionString")).ToString();   
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
        public string GetBuildNumber()
        {
            //var BuildNumber = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleVersion")).ToString();   
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
        }
    }
}