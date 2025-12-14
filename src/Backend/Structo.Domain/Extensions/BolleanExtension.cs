using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Structo.Domain.Extensions
{
    public static class BolleanExtension
    {
        public static bool IsFalse(this bool value) => value == false;

    }
}
