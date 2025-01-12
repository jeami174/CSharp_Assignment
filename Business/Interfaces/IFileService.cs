namespace Business.Interfaces;

/// <summary>
/// Interface for file operations, providing methods to save and retrive content from file.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Saves provided content to file.
    /// </summary>
    abstract void SaveContentToFile(string content);

    /// <summary>
    /// Reads and returns content from file.
    /// Returns null if file doesn't exist or an error occurs.
    /// </summary>
    abstract string GetContentFromFile();
}
