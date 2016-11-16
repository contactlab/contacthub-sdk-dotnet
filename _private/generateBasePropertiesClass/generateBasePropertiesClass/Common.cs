using Microsoft.CSharp;
using System.Text.RegularExpressions;


public static class Common
{

    public static string NO_VALUE = "NoValue";

    static CSharpCodeProvider cs = new CSharpCodeProvider();

    public static string makeValidFileName(string name)
    {
        name = name.Replace("-", "Minus");
        Regex illegalInFileName = new Regex(@"[^a-zA-Z0-9]");
        name = illegalInFileName.Replace(name, "");
        if (!cs.IsValidIdentifier(name))
        {
            name = "@" + name;
        }

        return name;
    }
}
