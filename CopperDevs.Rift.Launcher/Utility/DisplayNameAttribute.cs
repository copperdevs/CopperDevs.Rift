namespace CopperDevs.Rift.Launcher.Utility;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class DisplayNameAttribute(string description) : Attribute
{
    public readonly string Description = description;
}