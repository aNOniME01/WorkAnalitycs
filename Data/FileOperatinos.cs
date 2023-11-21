using System.IO;

namespace WorkAnalitycsWPF.Data
{
    internal class FileOperatinos
    {
        public static string GetLocation()
        {
            string transferFileLoc = ".";

            transferFileLoc = Path.GetFullPath(transferFileLoc);

            string[] hlpr = transferFileLoc.Split('\\');
            transferFileLoc = "";
            for (int i = 0; i < hlpr.Length - 3; i++)
            {
                transferFileLoc += hlpr[i];
                transferFileLoc += "\\";
            }
            return transferFileLoc;

        }

        public static string GetOptionsPath() => GetLocation()+"Files\\options.txt";

    }
}
