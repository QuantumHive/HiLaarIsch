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
            public const string NavigationBar = @"_NavigationBar";
        }

        public class Commands
        {
            public const string Add = @"~\Views\Shared\Commands\_Add.cshtml";
            public const string Back = @"~\Views\Shared\Commands\_Back.cshtml";
        }
    }
}