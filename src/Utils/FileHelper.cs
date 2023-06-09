namespace Utils;

static class FileHelper
{
    public static string[] ReadFile(string filePath)
    {
        return File.ReadAllLines(filePath);
    }
    public static void WriteFile(string filePath, string[] data)
    {
        File.WriteAllLines(filePath, data);
    }
}