using DtoTranslator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBasicConversionAndGenerics()
        {
            var dllFolderName = Directory.GetCurrentDirectory() + "\\UnitTest.dll";
            var paths = new List<string>();
            paths.Add(dllFolderName);
            var objModel = DtoTranslator.DtoTranslator.GetObjectModelFromDlls(paths.ToArray(), "_1");
            var typescriptFileContent = DtoTranslator.DtoTranslator.TranslateObjectModelToTypescript(objModel);
            var expected = File.ReadAllText(Directory.GetCurrentDirectory() + "\\TestResources\\Results\\test1.ts");
            Assert.AreEqual(expected, typescriptFileContent);
        }

        [TestMethod]
        public void TestInheritance()
        {
            var dllFolderName = Directory.GetCurrentDirectory() + "\\UnitTest.dll";
            var paths = new List<string>();
            paths.Add(dllFolderName);
            var objModel = DtoTranslator.DtoTranslator.GetObjectModelFromDlls(paths.ToArray(), "_2");
            var typescriptFileContent = DtoTranslator.DtoTranslator.TranslateObjectModelToTypescript(objModel);
            var expected = File.ReadAllText(Directory.GetCurrentDirectory() + "\\TestResources\\Results\\test2.ts");
            Assert.AreEqual(expected, typescriptFileContent);
        }
    }
}
