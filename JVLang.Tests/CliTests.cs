namespace JVLang.Tests;

public class CliTests
{
    private const string TempFileContent = "content";

    private string _tempFilePath;

    [SetUp]
    public void Setup()
    {
        _tempFilePath = Path.GetTempFileName();
        File.WriteAllText(_tempFilePath, TempFileContent);
    }

    [TearDown]
    public void Teardown()
    {
        if (File.Exists(_tempFilePath)) File.Delete(_tempFilePath);
    }

    [Test]
    public void ReadFile_ThrowsFileNotProvidedException()
    {
        Assert.Throws<FileNotProvidedException>(() => new Cli([]).ReadFile());
    }

    [Test]
    public void ReadFile_ProvidedFileNotFoundException()
    {
        Assert.Throws<ProvidedFileNotFoundException>(() => new Cli(["inexistent.txt"]).ReadFile());
    }

    [Test]
    public void ReadFile_ReturnsFileCharacterArray()
    {
        Assert.That(new Cli([_tempFilePath]).ReadFile(), Is.EquivalentTo(TempFileContent.ToCharArray()));
    }
}