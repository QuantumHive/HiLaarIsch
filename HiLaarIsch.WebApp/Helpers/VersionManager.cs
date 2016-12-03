using System;
using System.Reflection;

namespace HiLaarIsch
{
    public static class VersionManager
    {
        public static readonly Version Version = Assembly.GetExecutingAssembly().GetName().Version;

        public static string FriendlyVersion => $"v{VersionManager.Version.Major}.{VersionManager.Version.Minor}.{VersionManager.Version.Build}";
    }
}