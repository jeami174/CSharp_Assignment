namespace Business.Interfaces;

public interface IFileService
{
    abstract void SaveContentToFile(string content);
    abstract string GetContentFromFile();
}
