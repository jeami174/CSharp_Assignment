using Business.Helpers;
using Business.Interfaces;

namespace Business.Tests.Helpers;

public class GuidGenerator_Tests
{

    // I will not mock GuidGenerator here as it can be tested directly.
    // However, I mock IIdGenerator when testing other classes that depend on it – see, for example, ContactService.
    // Here, I will test that NewId() returns a valid GUID string and that each call generates a unique value.


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
