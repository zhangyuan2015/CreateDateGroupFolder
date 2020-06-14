using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.IO;

namespace CreateDateGroupFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootFolderPath = GetFolderPath();
            //var folderPath = "D:\\Users\\zhangyuan\\Desktop\\120_FUJI";

            var filePaths = Directory.GetFiles(rootFolderPath);

            //Dictionary<string, DateTime> dicFileDate = new Dictionary<string, DateTime>();

            foreach (var filePath in filePaths)
            {
                ShellObject obj = ShellObject.FromParsingName(filePath);
                var takenDate = obj.Properties.System.ItemDate.Value;
                if (takenDate.HasValue)
                {
                    string folderPath = rootFolderPath + "\\" + takenDate.Value.ToString("yyyy-MM-dd");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    FileInfo file = new FileInfo(filePath);
                    file.MoveTo(folderPath + "\\" + file.Name);
                }
            }

            Console.WriteLine("OK");
            Console.ReadLine();
        }

        private static string GetFolderPath()
        {
            Console.Write("输入文件夹路径：");
            var folderPath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(folderPath))
                GetFolderPath();

            return folderPath;
        }
    }
}