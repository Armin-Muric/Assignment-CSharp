namespace Shared.Interfaces;

public interface IFileService
{
    /// <summary>
    /// saves provided content to a file specified on given filepath
    /// </summary>
    /// <param name="filePath">the path to the file where content will be saved</param>
    /// <param name="content">the data to be saved in the file</param>
    /// <returns>returns true is content successfully saved to file, else false</returns>
    bool SaveToFile(string filePath, string content);

    /// <summary>
    /// gets the content from file based on filepath
    /// </summary>
    /// <param name="filePath">the path to the file that will get the data</param>
    /// <returns>returns the content from the file if it exists, else null</returns>
    string GetContentFromFile(string filePath);
}
