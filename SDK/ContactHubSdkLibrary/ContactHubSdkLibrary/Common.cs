using ContactHubSdkLibrary;
using System.Diagnostics;
using System.Text.RegularExpressions;


public static class Common
{
    public static string makeValidFileName(string name)
    {
        name = name.Replace("-", "Minus");
        Regex illegalInFileName = new Regex(@"[^a-zA-Z0-9]");
        name = illegalInFileName.Replace(name, "");
        return name;
    }


}

