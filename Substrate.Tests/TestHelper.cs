using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Substrate.Tests
{
    public static class TestHelper
    {
        public static string DataPath(string path)
        {
            return Path.Combine("../../../Data", path);
        }
    }
}
