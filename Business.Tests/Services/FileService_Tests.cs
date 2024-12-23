using Business.Services;
using Moq;

namespace Business.Tests.Services;

public class FileService_Tests
{
    //Här testas metoderna som rör filhanteringen.
    //Jag använder en mockad filsystem-approach för att undvika att skriva till det verkliga 
    //filsystemet under testning.

    private readonly string _testDirectory = "TestData";
    private readonly string _testFile = "test.json";
    private readonly Mock<FileService> _fileServiceMock;

    public FileService_Tests()
    {
        _fileServiceMock = new Mock<FileService>(_testDirectory, _testFile) { CallBase = true };

        //Säkerställer att testkatalogen rensas före varje test
        if (Directory.Exists(_testDirectory))
        { Directory.Delete(_testDirectory, true); }
    }

    [Fact]
    public void SaveContentToFile_ShouldCreateFile_AndSaveContent()
    {
        //Arrange
        var content = "{\"Name\":\"Test Contact\"}";

        //ACT
        _fileServiceMock.Object.SaveContentToFile(content);

        //Assert
        var filePath = Path.Combine(_testDirectory, _testFile);
        Assert.True(File.Exists(filePath));

        var savedContent = File.ReadAllText(filePath);
        Assert.Equal(content, savedContent);

    }

    [Fact]
    public void SaveContentToFile_ShouldCreateDirectory_ifNotExists()
    {
        //Arrange
        var content = "{\"Name\":\"Test Contact\"}";

        //ACT
        _fileServiceMock.Object.SaveContentToFile(content);

        //Assert
        Assert.True(Directory.Exists(_testDirectory));
    }

    [Fact]
    public void SaveContentToFile_ShouldNotWrite_IfContentIsEmpty()
    {
        //Arrange
        var emptyContent = "";

        //Act
        _fileServiceMock.Object.SaveContentToFile(emptyContent);

        //Assert
        var filePath = Path.Combine(_testDirectory, _testFile);
        Assert.False(File.Exists(filePath));
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnContent_IfFileExists()
    {
        //Arrange
        var content = "{\"Name\":\"Clark Kent\"}";
        var filePath = Path.Combine(_testDirectory, _testFile);

        //Skapar testfilen manuellt
        Directory.CreateDirectory(_testDirectory);
        File.WriteAllText(filePath, content);

        //Act
        var result = _fileServiceMock.Object.GetContentFromFile();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(content, result);
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnNull_IfFileDoesNotExist()
    {
        //Act
        var result =_fileServiceMock.Object.GetContentFromFile();

        //Assert
        Assert.Null(result);
    }

}
