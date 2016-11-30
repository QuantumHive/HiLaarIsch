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
            public const string jQuery = @"~/bundles/scripts/lib/jquery";
            public const string bootstrap = @"~/bundles/scripts/lib/bootstrap";
            public const string bootstrapValidator = @"~/bundles/scripts/lib/bootstrap-validator";
            //app
        }

        public static class Styles
        {
            //lib
            public const string bootstrap = @"~/bundles/styles/lib/bootstrap";
            public const string fontAwesome = @"~/bundles/styles/lib/font-awesome";
            //app
            public const string global = @"~/bundles/styles/global";
            public const string login = @"~/bundles/styles/signin";
        }
    }

    internal static class BundleConfig
    {
        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            //lib
            bundles.RegisterScript(Bundles.Scripts.jQuery, Cdns.JQuery, @"~/Scripts/lib/jquery/jquery-{version}.js");
            bundles.RegisterScript(Bundles.Scripts.bootstrap, Cdns.BootstrapJs, @"~/Scripts/lib/bootstrap/bootstrap-{version}.js");
            bundles.RegisterScript(Bundles.Scripts.bootstrapValidator, Cdns.BootstrapValidator, @"~/Scripts/lib/1000hz-bootstrap-validator/validator-{version}.js");
            //app
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            //lib
            bundles.RegisterStyle(Bundles.Styles.bootstrap, Cdns.BootstrapCss, @"~/Content/bootstrap-{version}.css");
            bundles.RegisterScript(Bundles.Styles.fontAwesome, Cdns.FontAwesome, @"~/Content/font-awesome-{version}.css");
            //app
            bundles.RegisterStyle(Bundles.Styles.global, @"~/Content/global.css");
            bundles.RegisterStyle(Bundles.Styles.login, @"~/Content/login.css");
        }

        private class Cdns
        {
            public static Cdns JQuery => new Cdns(@"https://code.jquery.com/jquery-3.1.1.min.js");
            public static Cdns BootstrapJs => new Cdns(@"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js");
            public static Cdns BootstrapCss => new Cdns(@"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css");
            public static Cdns BootstrapValidator => new Cdns(@"https://cdnjs.cloudflare.com/ajax/libs/1000hz-bootstrap-validator/0.11.5/validator.min.js");
            public static Cdns FontAwesome => new Cdns(@"https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css");

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
            var bundle = string.IsNullOrEmpty(cdn.Path) ?
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