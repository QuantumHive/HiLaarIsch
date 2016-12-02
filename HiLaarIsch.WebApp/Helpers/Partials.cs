using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiLaarIsch
{
    public class Partials
    {
        public class Shared
        {
            public const string NavigationBar = "NavigationBar";
            public const string NoResults = "NoResults";
        }

        public class Commands
        {
            public const string Add = @"~\Views\Shared\Commands\Add.cshtml";
            public const string Back = @"~\Views\Shared\Commands\Back.cshtml";
        }
    }
}