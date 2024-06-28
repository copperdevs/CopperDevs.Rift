namespace CopperDevs.Rift.Launcher.Utility;

public class DisplayNameAttribute(string description) : Attribute
{
    public readonly string Description = description;
}