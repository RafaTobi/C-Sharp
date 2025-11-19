using System.Text;
using Groeiproject.BL.Domain;

namespace Groeiproject.UI.CA.Extensions;

public static class BakeryExtensions
{
    public static string GetInfo(this Bakery bakery)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(bakery.Name + ", " + bakery.Adres + ". Sells: ");
        foreach (Bread bread in bakery.Breads)
        {
            stringBuilder.Append(bread.Name + " ");
        }

        return stringBuilder.ToString();
    }
}