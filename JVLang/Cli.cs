namespace JVLang;

public class CliException(string message) : Exception(message);

public class FileNotProvidedException(string[] args)
    : CliException($"No file provided. Args: {string.Join(", ", args)}");

public class ProvidedFileNotFoundException(string fileName)
    : CliException($"Provided file not found. File: {fileName}");

public class ReadFileException(Exception cause)
    : CliException($"Read file error. Cause: {cause}");

public class Cli(string[] args)
{
    private string FileName => args.Length > 0 ? args[0] : throw new FileNotProvidedException(args);

    public char[] ReadFile()
    {
        if (!File.Exists(FileName)) throw new ProvidedFileNotFoundException(FileName);
        try
        {
            return File.ReadAllText(FileName).ToCharArray();
        }
        catch (Exception e)
        {
            throw new ReadFileException(e);
        }
    }
}