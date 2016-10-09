using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HiLaarIsch
{
    public static class Bundles
    {
        public const string jQuery = @"~/bundles/jquery";
    }

    public static class BundleConfig
    {
        private class Cdns
        {
            public const string jQuery = @"https://code.jquery.com/jquery-3.1.1.min.js";
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
            var jquery = new ScriptBundle(Bundles.jQuery, Cdns.jQuery).Include("~/scripts/jquery-{version}.js");
            bundles.Add(jquery);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {

        }
    }
}