namespace ProductApi.Tests;

internal static class FileUtils
{
    private const string SolutionFileSearchPattern = "*.sln";
    internal static string GetDockerComposePath()
    {
        DirectoryInfo directory = GetProjectDirectory();
        return Path.Combine(
            directory.FullName,
            "docker-compose.yaml");
    }
    private static DirectoryInfo GetProjectDirectory()
    {
        DirectoryInfo? directory = new(Directory.GetCurrentDirectory());
        while (directory?
                   .GetFiles(SolutionFileSearchPattern)
                   .Length == 0)
        {
            directory = directory.Parent;
        }
        if (directory == null)
        {
            throw new ArgumentException("No project directory found.");
        }
        return directory;
    }
}