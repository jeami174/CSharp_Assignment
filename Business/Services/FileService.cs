﻿using Business.Interfaces;

namespace Business.Services;

/// <summary>
/// Manages a local file.
/// </summary>
public class FileService : IFileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;

    public FileService(string directoryPath = "Data", string fileName = "list.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
    }

    public void SaveContentToFile(string content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            try 
            {
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }

                File.WriteAllText(_filePath, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save file: {ex.Message}");
            }
        }
    }

    public string GetContentFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                return File.ReadAllText(_filePath);
            }
        }
        catch (Exception ex)
        { 
            Console.WriteLine($"Failed to read file: {ex.Message}");
        }
        
        return null!;
    }
}
