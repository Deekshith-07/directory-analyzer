using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectorySizeAnalyzer
{
    public class DirectoryService
    {
        private long totalSize = 0;
        private long largestFileSize = 0;
        private string largestFilePath = "";
        private long largestFolderSize = 0;
        private string largestFolderPath = "";
        private int totalDirectories = 0;
        private int totalFiles = 0;
        private Dictionary<string, int> fileTypeCounts = new();

        public void TraverseDirectory(string dirPath)
        {
            Console.WriteLine($"Directory Structure for: {dirPath}");
            TraverseDirectory(dirPath, "", true);
            PrintSummary();
        }

        private void TraverseDirectory(string dirPath, string indent, bool isLast)
        {
            try
            {
                var subDirectories = Directory.GetDirectories(dirPath).ToList();
                var files = Directory.GetFiles(dirPath).ToList();

                for (int i = 0; i < files.Count; i++)
                {
                    bool lastFile = i == files.Count - 1 && subDirectories.Count == 0;
                    ProcessFile(files[i], indent, lastFile);
                }

                for (int i = 0; i < subDirectories.Count; i++)
                {
                    bool lastSubDir = i == subDirectories.Count - 1;
                    ProcessDirectory(subDirectories[i], indent, lastSubDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing directory {dirPath}: {ex.Message}");
            }
        }

        private void ProcessDirectory(string dirPath, string indent, bool isLast)
        {
            totalDirectories++;
            long directorySize = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories).Sum(file => new FileInfo(file).Length);

            if (directorySize > largestFolderSize)
            {
                largestFolderSize = directorySize;
                largestFolderPath = dirPath;
            }

            Console.WriteLine($"{indent}{(isLast ? "└── " : "├── ")}{Path.GetFileName(dirPath)}/");

            TraverseDirectory(dirPath, $"{indent}{(isLast ? "    " : "│   ")}", isLast);
        }

        private void ProcessFile(string filePath, string indent, bool isLast)
        {
            totalFiles++;
            long fileSize = new FileInfo(filePath).Length;
            totalSize += fileSize;

            if (fileSize > largestFileSize)
            {
                largestFileSize = fileSize;
                largestFilePath = filePath;
            }

            string extension = Path.GetExtension(filePath).TrimStart('.').ToLower();
            if (string.IsNullOrEmpty(extension))
            {
                extension = "unknown";
            }

            if (!fileTypeCounts.ContainsKey(extension))
            {
                fileTypeCounts[extension] = 0;
            }
            fileTypeCounts[extension]++;

            Console.WriteLine($"{indent}{(isLast ? "└── " : "├── ")}{Path.GetFileName(filePath)} (Size: {FormatSize(fileSize)})");
        }

        private void PrintSummary()
        {
            Console.WriteLine("\nSummary\n-------------------");
            Console.WriteLine($"Total directories: {totalDirectories}");
            Console.WriteLine($"Total files: {totalFiles}");
            Console.WriteLine($"Total size: {FormatSize(totalSize)}");
            Console.WriteLine($"Largest file: {largestFilePath} (Size: {FormatSize(largestFileSize)})");
            Console.WriteLine($"Largest folder: {largestFolderPath} (Size: {FormatSize(largestFolderSize)})");

            Console.WriteLine("\nFile count based on type\n---------------------");
            foreach (var fileType in fileTypeCounts.OrderBy(kv => kv.Key))
            {
                Console.WriteLine($"{fileType.Key} : {fileType.Value}");
            }
        }

        private string FormatSize(long size)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double formattedSize = size;
            int order = 0;

            while (formattedSize >= 1024 && order < sizes.Length - 1)
            {
                order++;
                formattedSize /= 1024;
            }

            return $"{formattedSize:0.##} {sizes[order]}";
        }
    }
}
