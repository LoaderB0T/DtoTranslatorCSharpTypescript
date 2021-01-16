using System;
using System.IO;

namespace DtoTranslator
{
    class Program
    {
        static string[] dllFilePaths;
        static string typescriptName;
        static string namespaceFilter = null;
        static bool closeAfterRun = false;

        static void Main(string[] args)
        {

            if (args.Length < 2)
            {
                AskForParams();
            }
            else
            {
                closeAfterRun = true;
                dllFilePaths = args[0].Split(',');
                typescriptName = args[1];
                if (args.Length >= 3)
                {
                    namespaceFilter = args[2];
                }
            }


            while (true)
            {
                TranslateCode();
                if(closeAfterRun)
                {
                    return;
                }
                Console.WriteLine("Code generation complete.");
                Console.WriteLine("Press '1' to re-run or '2' to change parameters.");
                char key;
                do
                {
                    key = Console.ReadKey().KeyChar;
                } while (key != '1' && key != '2');
                if (key == '2')
                {
                    AskForParams();
                }
            }
        }

        private static void TranslateCode()
        {
            var objModel = DtoTranslator.GetObjectModelFromDlls(dllFilePaths, namespaceFilter);
            var typescriptFileContent = DtoTranslator.TranslateObjectModelToTypescript(objModel);

            File.WriteAllText(typescriptName, typescriptFileContent);
            Console.WriteLine(typescriptFileContent);

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }

        static void AskForParams()
        {
            Console.WriteLine("Please start this program with two (or three) parameters:");
            Console.WriteLine("You can provide it here:");

            Console.WriteLine("  1. The Path(s) to the dll");
            var dllFilePathStr = Console.ReadLine().Replace("\"", "");
            dllFilePaths = dllFilePathStr.Split(',');

            Console.WriteLine("  2. The Path to the typescripe file that should be generated:");
            typescriptName = Console.ReadLine().Replace("\"", "");

            Console.WriteLine("  3. The namespace filter (single word) (optional):");
            namespaceFilter = Console.ReadLine();
        }
    }
}
