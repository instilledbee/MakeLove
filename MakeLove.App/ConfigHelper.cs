using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.IO;

namespace MakeLove.App
{
    internal static class ConfigHelper
    {
        /// <summary>
        /// Absolute path where the LOVE source codes and assets are located
        /// </summary>
        internal static string SourcePath => ConfigurationManager.AppSettings["sourcePath"];

        /// <summary>
        /// Absolute path where the build artifacts are created
        /// </summary>
        internal static string BuildPath => ConfigurationManager.AppSettings["buildPath"];

        /// <summary>
        /// Filename to use when creating the LOVE file build
        /// </summary>
        internal static string LoveBuildName
        {
            get
            {
                if (UseBuildNumber)
                    return String.Format("{0}{1:D3}.love", ConfigurationManager.AppSettings["buildName"], BuildNumber);

                else
                    return String.Format("{0}.love", ConfigurationManager.AppSettings["buildName"]);
            }
        }

        /// <summary>
        /// Absolute path where the Windows build is created
        /// </summary>
        internal static string WindowsBuildPath => Path.Combine(BuildPath, "windows");

        /// <summary>
        /// Filename to use when creating the Windows build
        /// </summary>
        internal static string WindowsBuildName
        {
            get
            {
                if (UseBuildNumber)
                    return String.Format("{0}{1:D3}.exe", ConfigurationManager.AppSettings["buildName"], BuildNumber);

                else
                    return String.Format("{0}.exe", ConfigurationManager.AppSettings["buildName"]);
            }
        }

        /// <summary>
        /// Absolute path where the LOVE SDK files are located
        /// </summary>
        internal static string LovePath => ConfigurationManager.AppSettings["loveLibPath"];

        /// <summary>
        /// Specify whether build numbers should be appended to the auto-generated builds
        /// </summary>
        internal static bool UseBuildNumber => Convert.ToBoolean(ConfigurationManager.AppSettings["useBuildNumber"]);

        /// <summary>
        /// The configured build targets to create artifacts for
        /// </summary>
        internal static BuildTargets CurrentBuildTargets
        {
            get
            {
                return (BuildTargets)Enum.Parse(typeof(BuildTargets), 
                    ConfigurationManager.AppSettings["buildTargets"].Replace(" ", String.Empty), true);
            }
        }

        /// <summary>
        /// Increment the stored build number in the config file
        /// </summary>
        internal static void IncrementBuildNumber()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var newBuildNumber = BuildNumber + 1;

            if (config.AppSettings.Settings["buildNumber"] != null)
                config.AppSettings.Settings["buildNumber"].Value = newBuildNumber.ToString();
            else
                config.AppSettings.Settings.Add("buildNumber", newBuildNumber.ToString());

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static int BuildNumber => Convert.ToInt32(ConfigurationManager.AppSettings["buildNumber"]);
    }

    /// <summary>
    /// Possible target platforms supported
    /// </summary>
    [Flags]
    internal enum BuildTargets
    {
        Windows = 1
    }
}
