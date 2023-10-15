using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace WorkAnalitycsWPF.Data
{
    public class Options
    {
        public string FileLoc { get; private set; }
        public string ModelDir { get; private set; }


        private List<string> optionLines = new List<string>();


        public Options(string fileLoc)
        {
            StreamReader sr = File.OpenText(fileLoc);
            while (!sr.EndOfStream)
            {
                optionLines.Add(sr.ReadLine());
            }
            sr.Close();


            FileLoc = fileLoc;
            if (optionLines.Count > 0)
            {
                ModelDir = optionLines[GetOptionLine("ModelDir")].Split(':').Length > 1 ? optionLines[GetOptionLine("ModelDir")].Split(':')[1].Trim() : "";
            }
            else ModelDir = "";

        }

        public void ChangeModelDir(string newModelDir) => ModelDir = newModelDir;

        public void Save()
        {
            StreamWriter sw = File.CreateText(FileLoc);
            sw.WriteLine();
            sw.WriteLine($"ModelDir : {ModelDir}");
            sw.Close();
        }

        private int GetOptionLine(string optionName)
        {
            for (int i = 0; i < optionLines.Count; i++)
            {
                if (optionLines[i].Split(':')[0].Trim() == optionName) return i;
            }

            return 0;
        }
    }
}
