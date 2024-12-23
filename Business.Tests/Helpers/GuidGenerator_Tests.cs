using Business.Helpers;
using Business.Interfaces;

namespace Business.Tests.Helpers;

public class GuidGenerator_Tests
{

    //Jag kommer inte att mocka GuidGenerator här då vi kan testa den direkt.
    //Däremot mockas IIdGenerator när jag testar andra klasser som är beroende av den - se tex ContactService
    //Här kommer jag att testa att NewId() retunerar en giltlig GUID-Sträng och att varje anrop ger ett unikt värde
    

    [Fact]
    public void NewID_ShouldReturn_ValidGuidString()
    {
        //Arrange
        IIdGenerator generator = new GuidGenerator();

        //Act
        var result = generator.NewId();

        //Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(Guid.TryParse(result, out _));
    }

    [Fact]
    public void NewId_ShouldReturn_UniqueValues()
    {
        //Arrange
        IIdGenerator generator = new GuidGenerator();

        //Act
        var result1 = generator.NewId();
        var result2 = generator.NewId();

        //Assert
        Assert.NotEqual(result1, result2);
    }

}
