using Shared.Interfaces;
using Shared.Services;

namespace CSharpAddressBookProject_Test.FileServices;

public class FileService_Tests
{
    //integrationstest
    [Fact]
    public void SaveToFile_Should_ReturnTrue_IfFilePathExists()
    {
        //Arrange
        IFileService fileService = new FileService();
        string filePath = @"c:\Projects\AdressbookConsoleProject\test.json";
        string content = "Testing";

        //Act
        bool result = fileService.SaveToFile(filePath, content);

        //Assert
        Assert.True(result);
    }


    [Fact]
    public void SaveToFile_Should_ReturnFalse_IfFilePathDoNotExists()
    {
        //Arrange
        IFileService fileService = new FileService();
        string filePath = @$"c:\{Guid.NewGuid()}\test.json";
        string content = "Testing";

        //Act
        bool result = fileService.SaveToFile(filePath, content);

        //Assert
        Assert.False(result);
    }
}
