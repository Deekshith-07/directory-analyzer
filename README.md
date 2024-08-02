# Directory Analyzer

Directory Analyzer is a C# console application that traverses a directory structure, calculates the size of each file and folder, and provides a summary of the directory contents. It prints the directory structure with indentation and symbols for better readability.

## Features

- Traverse directories and print their structure
- Calculate and display the size of each file and folder
- Identify and display the largest file and folder
- Count files based on their extensions
- Provide a summary of the directory contents

## Installation

1. Clone the repository:

    ```
    git clone [https://github.com/your-username/DirectorySizeAnalyzer.git](https://github.com/Deekshith-07/directory-analyzer.git)
    cd directory-analyzer
    ```

2. Open the project in your preferred C# IDE (e.g., Visual Studio).

3. Restore the NuGet packages:

    ```
    dotnet restore
    ```

## Usage

1. Build and run the project:

    ```
    dotnet run --project directory-analyzer
    ```

2. Enter the path of the directory you want to analyze when prompted.

## Example Output
```
Directory Structure for: C:\Your\Directory\Path
├── SubFolder1/
│ ├── File1.txt (Size: 1 KB)
│ └── File2.txt (Size: 2 KB)
└── SubFolder2/
└── File3.txt (Size: 3 KB)

Summary
----------------
Total directories: 2
Total files: 3
Total size: 6 KB
Largest file: C:\Your\Directory\Path\SubFolder2\File3.txt (Size: 3 KB)
Largest folder: C:\Your\Directory\Path\SubFolder2 (Size: 3 KB)

File count based on type
------------------
txt : 3
```


## Code Overview

### DirectoryService.cs

The `DirectoryService` class contains methods to traverse directories, process files, and print the directory structure and summary.

- `TraverseDirectory(string dirPath)`: Initializes the directory traversal and prints the directory structure.
- `TraverseDirectory(string dirPath, string indent, bool isLast)`: Recursively traverses directories and files, printing them with appropriate indentation and symbols.
- `ProcessDirectory(string dirPath, string indent, bool isLast)`: Processes a directory, calculates its size, and updates the largest folder information.
- `ProcessFile(string filePath, string indent, bool isLast)`: Processes a file, calculates its size, and updates the largest file information.
- `PrintSummary()`: Prints a summary of the directory contents, including the total number of directories and files, total size, largest file and folder, and file counts based on their extensions.
- `FormatSize(long size)`: Formats file and folder sizes into a readable format (e.g., B, KB, MB, GB, TB).

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

