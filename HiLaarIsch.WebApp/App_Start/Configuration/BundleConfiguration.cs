using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HiLaarIsch
{
    public static class Bundles
    {
        public static class Scripts
        {
            //lib
            public const string JQuery = "~/bundles/scripts/lib/jquery";
            public const string Bootstrap = "~/bundles/scripts/lib/bootstrap";
            public const string BootstrapValidator = "~/bundles/scripts/lib/bootstrap-validator";
            //app
        }

        public static class Styles
        {
            //lib
            public const string Bootstrap = "~/bundles/styles/lib/bootstrap";
            public const string FontAwesome = "~/bundles/styles/lib/font-awesome";
            //app
            public const string Global = "~/bundles/styles/app/global";
            public const string Login = "~/bundles/styles/app/signin";
        }
    }

    internal static class BundleConfig
    {
        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.RegisterScript(Bundles.Scripts.BootstrapValidator,
                "~/Scripts/lib/1000hz-bootstrap-validator/validator-{version}.js",
                "~/Scripts/app/validator.js");

            //lib
            bundles.RegisterScript(Bundles.Scripts.JQuery, Cdns.JQuery, "~/Scripts/lib/jquery/jquery-{version}.js");
            bundles.RegisterScript(Bundles.Scripts.Bootstrap, Cdns.BootstrapJs, "~/Scripts/lib/bootstrap/bootstrap-{version}.js");
            //app
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            //lib
            bundles.RegisterStyle(Bundles.Styles.Bootstrap, Cdns.BootstrapCss, "~/Content/bootstrap-{version}.css");
            bundles.RegisterScript(Bundles.Styles.FontAwesome, Cdns.FontAwesome, "~/Content/font-awesome-{version}.css");
            //app
            bundles.RegisterStyle(Bundles.Styles.Global, 
                "~/Content/app/global.css",
                "~/Content/app/validation.css");
            bundles.RegisterStyle(Bundles.Styles.Login, "~/Content/app/login.css");
        }

        private class Cdns
        {
            public static Cdns JQuery => new Cdns("https://code.jquery.com/jquery-3.1.1.min.js");
            public static Cdns BootstrapJs => new Cdns("https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js");
            public static Cdns BootstrapCss => new Cdns("https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css");
            public static Cdns FontAwesome => new Cdns("https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css");

            private Cdns(string cdnPath)
            {
                this.Path = cdnPath;
            }

            public string Path { get; }
        }

        internal static void ConfigureAndRegisterBundles()
        {
            var bundles = BundleTable.Bundles;
            bundles.UseCdn = true;

            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);
        }

        private static void RegisterScript(this BundleCollection bundles, string virtualPath, params string[] path)
        {
            bundles.RegisterScript(virtualPath, null, path);
        }

        private static void RegisterScript(this BundleCollection bundles, string virtualPath, Cdns cdn, params string[] path)
        {
            var bundle = cdn == null ?
                new ScriptBundle(virtualPath).Include(path) :
                new ScriptBundle(virtualPath, cdn.Path).Include(path);
            bundles.Add(bundle);
        }

        private static void RegisterStyle(this BundleCollection bundles, string virtualPath, params string[] path)
        {
            bundles.RegisterStyle(virtualPath, null, path);
        }

        private static void RegisterStyle(this BundleCollection bundles, string virtualPath, Cdns cdn, params string[] path)
        {
            var bundle = cdn == null ?
                new StyleBundle(virtualPath).Include(path) :
                new StyleBundle(virtualPath, cdn.Path).Include(path);
            bundles.Add(bundle);
        }
    }
}