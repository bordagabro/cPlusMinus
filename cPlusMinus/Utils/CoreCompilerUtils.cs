using System.Diagnostics;


namespace cPlusMinus.Utils;
public class CoreCompilerUtils
{
    public void runCmd()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Program output:");
        Console.ForegroundColor = ConsoleColor.Black;
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "python3";
        start.Arguments = "out.py";
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        using(Process process = Process.Start(start))
        {
            using(StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.WriteLine();
                Console.Write(result);
            }
        }
    }
    public void haltWithError(string error, string line, int lineNumber, int wordNumber)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n\x0b[ERROR]:\x1b[0m {error}");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($"{lineNumber} |  ");
        Console.ForegroundColor = ConsoleColor.Black;
        
        Console.Write($"{line}\n");

        if (wordNumber != -1)
        {
            string[] words = line.Split(" ");
            string proceedingSpaces = "";
    
            for (int i = 0; i < wordNumber; i++)
            {
                proceedingSpaces += new string(' ', words[i].Length);
                if (i < wordNumber)
                {
                    proceedingSpaces += " ";
                }
            }

            if (lineNumber < 9)
            {
                proceedingSpaces += "     ";
            }
            else if (lineNumber < 99)
            {
                proceedingSpaces += "      ";
            }
            else
            {
                proceedingSpaces += "       ";
            }

            Console.ForegroundColor = ConsoleColor.Red;
            // Add carets to underline the word
            string carets = new string('~', words[wordNumber].Length);
            Console.WriteLine($"{proceedingSpaces}{carets}");
            Console.ForegroundColor = ConsoleColor.Black;

        }
    
        Console.ForegroundColor = ConsoleColor.Black;
        
    }

    public void haltWithSimpleError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n\x0b[ERROR]:\x1b[0m {error}");
        Console.ForegroundColor = ConsoleColor.Black;
        
    }
    
    static public void compilerWarn(string warning)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\x0b[1mWARNING:\x1b[0m {warning}");
        Console.ForegroundColor = ConsoleColor.Black;
        
    }  
}