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
            public const string jQuery = @"~/bundles/scripts/jquery";
            public const string bootstrap = @"~/bundles/scripts/bootstrap";
        }
        
        public static class Styles
        {
            public const string bootstrap = @"~/bundles/styles/bootstrap";
            public const string global = @"~/bundles/styles/global";
            public const string login = @"~/bundles/styles/signin";
        }
    }

    public static class BundleConfig
    {
        private class Cdns
        {
            public const string jQuery = @"https://code.jquery.com/jquery-3.1.1.min.js";
            public const string bootstrapJs = @"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js";
            public const string bootstrapCss = @"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css";
        }

        public static void ConfigureAndRegisterBundles()
        {
            var bundles = BundleTable.Bundles;
            bundles.UseCdn = true;

            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            var jquery = new ScriptBundle(Bundles.Scripts.jQuery, Cdns.jQuery).Include(@"~/Scripts/jquery-{version}.js");
            var bootstrap = new ScriptBundle(Bundles.Scripts.bootstrap, Cdns.bootstrapJs).Include(@"~/Scripts/bootstrap.js");

            bundles.Add(jquery);
            bundles.Add(bootstrap);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            var bootstrap = new StyleBundle(Bundles.Styles.bootstrap, Cdns.bootstrapCss).Include(@"~/Content/bootstrap.css");
            var global = new StyleBundle(Bundles.Styles.global).Include(@"~/Content/global.css");
            var login = new StyleBundle(Bundles.Styles.login).Include(@"~/Content/login.css");

            bundles.Add(bootstrap);
            bundles.Add(global);
            bundles.Add(login);
        }
    }
}