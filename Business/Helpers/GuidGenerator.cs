

using Business.Interfaces;

namespace Business.Helpers;

public class GuidGenerator: IIdGenerator
{
    public string NewId()
    {
        return Guid.NewGuid().ToString();
    }
}
