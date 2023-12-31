using Shared.Interfaces;
using System.Diagnostics;

namespace Shared.Services;

public class FileService : IFileService
{
    public string GetContentFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                using (var sr = new StreamReader(filePath))
                {
                    return sr.ReadToEnd();
                }
            }
            return null!;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    public bool SaveToFile(string filePath, string content)
    {
        try
        {
            using (var sw = new StreamWriter(filePath))
            {
                sw.WriteLine(content);
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return false;
        }
    }
}
