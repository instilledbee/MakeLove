using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace MakeLove.App
{
    internal static class ConfigHelper
    {
        /// <summary>
        /// Absolute path where the LOVE source codes and assets are located
        /// </summary>
        internal static string SourcePath
        {
            get
            {
                return ConfigurationManager.AppSettings["sourcePath"];
            }
        }

        /// <summary>
        /// Absolute path where the build artifacts are created
        /// </summary>
        internal static string BuildPath
        {
            get
            {
                return ConfigurationManager.AppSettings["buildPath"];
            }
        }

        /// <summary>
        /// Filename to use when creating the LOVE file build
        /// </summary>
        internal static string LoveBuildName
        {
            get
            {
                return String.Format("{0}.love", ConfigurationManager.AppSettings["buildName"]);
            }
        }

        /// <summary>
        /// Absolute path where the Windows build is created
        /// </summary>
        internal static string WindowsBuildPath
        {
            get
            {
                return Path.Combine(BuildPath, "windows");
            }
        }

        /// <summary>
        /// Filename to use when creating the Windows build
        /// </summary>
        internal static string WindowsBuildName
        {
            get
            {
                return String.Format("{0}.exe", ConfigurationManager.AppSettings["buildName"]);
            }
        }

        /// <summary>
        /// Absolute path where the LOVE SDK files are located
        /// </summary>
        internal static string LovePath
        {
            get
            {
                return ConfigurationManager.AppSettings["loveLibPath"];
            }
        }
    }
}
