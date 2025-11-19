using System.Text;
using Groeiproject.BL.Domain;

namespace Groeiproject.UI.CA.Extensions;

public static class BakerExtensions
{
    public static string GetInfo(this Baker baker)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(baker.Name + ", born: " + baker.DateOfBirth.ToString("d/MM/yyyy") + ". Works in: ");
        foreach (Contract contract in baker.Contracts)
        {
            stringBuilder.Append("\"" + contract.Bakery.Name + "\" ");
        }

        return stringBuilder.ToString();
    }
}