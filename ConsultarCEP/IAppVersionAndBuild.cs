using System;
using System.Collections.Generic;
using System.Text;

    namespace VersionAndBuildNumber.DependencyServices
    {
        public interface IAppVersionAndBuild
        {
            string GetVersionNumber();
            string GetBuildNumber();
        }
    }

