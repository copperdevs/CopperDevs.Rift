using CopperDevs.Rift.Launcher.Data;

namespace CopperDevs.Rift.Launcher.Utility;

public static class Util
{

    public static T GetAttribute<T>(this Enum value) where T : Attribute
    {
        var type = value.GetType();
        var memberInfo = type.GetMember(value.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
        return (attributes.Length > 0 ? (T)attributes[0] : null)!;
    }

    // This method creates a specific call to the above method, requesting the
    // Description MetaData attribute.
    public static string ToName(this Enum value)
    {
        var attribute = value.GetAttribute<DisplayNameAttribute>();
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return attribute is null ? value.ToString() : attribute.Description;
    }
}