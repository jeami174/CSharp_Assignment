namespace Business.Interfaces;

/// <summary>
/// Interface for generating unique identifiers.
/// </summary>
public interface IIdGenerator
{
    /// <summary>
    /// Generates a new unique identifier as a string.
    /// </summary>
    public abstract string NewId();
}
