namespace cPlusMinus.Utils;

public class CompilerChecks
{
    public bool fileHasLineCloseSymbolError(List<string> lines)
    {
        bool isFailing = false;

        for (int i = 0; i < lines.Count() && !isFailing; i++)
        {
            string line = lines[i];

            if (line.Length > 5 && (line.IndexOf(":D") == -1 && line.IndexOf(":)") == -1))
            {
                isFailing = true;
            }
        }

        return isFailing;
    }

    public bool fileHasLineNumber(List<string> lines)
    {
        
        bool isFailing = false;

        for (int i = 0; i < lines.Count() && !isFailing; i++)
        {
            string line = lines[i];

            if (line.IndexOf("|") != -1)
            {
                if ($"{line.Split("|")[0]}" != $"{i+1}" && $"{line.Split("|")[0]}" != $"{i+1} ")
                {
                    isFailing = true;
                }
            }
        }

        return isFailing;
    }
}