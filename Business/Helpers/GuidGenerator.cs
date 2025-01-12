

using Business.Interfaces;

namespace Business.Helpers;

/// <summary>
/// Generates unique identifiers as GUID; Globally Unique Identifiers.
/// </summary>
public class GuidGenerator: IIdGenerator
{
    public string NewId()
    {
        return Guid.NewGuid().ToString();
    }
}
