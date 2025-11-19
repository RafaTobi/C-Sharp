using Groeiproject.BL.Domain;

namespace Groeiproject.UI.CA.Extensions;

public static class BreadExtensions
{
    public static string GetInfo(this Bread bread)
    {
        return bread.Name + " " + bread.Price + "E, " + bread.Weight + "g";
    }
}