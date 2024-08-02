

namespace DirectorySizeAnalyzer
{
    public class Program
    {
        private static void Main(string[] args)
        {
            bool validInput = false;

            while (!validInput)
            {
                DirectoryService directoryService = new ();

                Console.Write("Enter a directory path: ");
                string? dirPath = Console.ReadLine();

                if (string.IsNullOrEmpty(dirPath))
                {
                    Console.WriteLine("Directory path can't be empty or null");
                    continue;
                }

                try
                {
                    directoryService.TraverseDirectory(dirPath);
                    validInput = true;

                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("\nDirectory not found. Please enter valid path\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError occurred: {ex.Message}\n");
                }
            }
        }
    }
}