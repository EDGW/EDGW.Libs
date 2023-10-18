using Microsoft.VisualStudio.TestTools.UnitTesting;
using EDGW.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDGW.IO.Tests
{
    [TestClass()]
    public class VirtualRootTests
    {
        [TestMethod()]
        public void VirtualRootTest()
        {
            VirtualRoot root = new("vroot", "vroot");
            LocalDirectory dir = new LocalDirectory(root, "d", "D:\\");
            root.AddDirectory(dir);
            var file = root.Directory.GetFile("d\\Installers\\Drivers\\NVIDIA GTX1660S Driver\\528.24-desktop-win10-win11-64bit-international-dch-whql.exe");
        }
    }
}