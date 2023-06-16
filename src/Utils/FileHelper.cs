namespace Utils;

static class FileHelper
{
    public static string[] ReadFile(string filePath)
    {
        if (!File.Exists(filePath)) throw ExceptionHandler.FileNotFound();
        return File.ReadAllLines(filePath);
    }
    public static void WriteFile(string filePath, string[] data)
    {
        if (!File.Exists(filePath)) throw ExceptionHandler.FileNotFound();
        File.WriteAllLines(filePath, data);
    }
}