using cPlusMinus.Utils;
using static cPlusMinus.Utils.CoreCompilerUtils;
using static cPlusMinus.Utils.CompilerChecks;


namespace cPlusMinus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CoreCompilerUtils ccutils = new CoreCompilerUtils();
            CompilerChecks cchecks = new CompilerChecks();
            
            StreamReader scriptfile = new StreamReader("script.cpm");
            StreamWriter writefile = new StreamWriter("out.py");

            List<int> linesWithVariables = new List<int>();
            
            string? line;
            
            //Compiler checks
            
            List<string> lines = new List<string>();
            while((line = scriptfile.ReadLine()) != null)
            {
                lines.Add(line);
            }

            if (cchecks.fileHasLineNumber(lines))
            {
                ccutils.haltWithSimpleError("Missing or wrong line number prefix!");
                return;
            }
            
            if (cchecks.fileHasLineCloseSymbolError(lines))
            {
                ccutils.haltWithSimpleError("Missing line closure on one, or more lines!");
                return;
            }

            
            scriptfile.Close();
            //Actual work begins
            StreamReader scriptfile2 = new StreamReader("script.cpm");
            
            int lineNumber = 0;

            while((line = scriptfile2.ReadLine()) != null)
            {
                lineNumber++;
                string[] lineWithoutLineNumber = line.Split("| ");
                string actialLine = lineWithoutLineNumber[1];
                string[] word = lineWithoutLineNumber[1].Split(" ");
                int chIndex = 0;

                if (word[chIndex] == "declVar")
                {
                    chIndex++;
                    
                    if (word[chIndex] == "const")
                    {
                        if (word[chIndex + 1] != "const" || word[chIndex + 2] != "const" || word[chIndex + 3] != "const")
                        {
                            ccutils.haltWithError("To create an immutable variable you must use 'const const const const'!", actialLine, lineNumber, chIndex);
                            return;
                        }
                        else
                        {
                            chIndex += 4;
                        }
                    }
                    
                    
                    if (word[chIndex] != "string" && word[chIndex] != "int" && word[chIndex] != "array")
                    {
                        ccutils.haltWithError("declVar must be followed by [ string | int | array ]!", actialLine, lineNumber, chIndex);
                        return;
                    }
                    chIndex++;

                    
                    if (word[chIndex] != "->")
                    {
                        ccutils.haltWithError("Expecting: ->", actialLine, lineNumber, chIndex);
                        return;
                    }
                    chIndex++;
                    
                    
                    writefile.WriteLine($"vflvdal{lineNumber} = {word[chIndex].Replace("_s_", " ").Replace("$", "vflvdal")}");
                }
                else if (word[chIndex] == "proclaim")
                {
                    chIndex++;
                    string append = "";
                    
                    if (word[chIndex] == "nobreak")
                    {
                        append = ", end=' '";
                        chIndex++;
                    }
                    else if (word[chIndex].IndexOf("break=") != -1)
                    {
                        append = $", end='{word[chIndex].Replace("\"", "").Replace("break=", "").Replace("_s_", " ")}'";
                        chIndex++;
                    }
                    
                    if (word[chIndex] != "->")
                    {
                        ccutils.haltWithError("Expecting: ->", actialLine, lineNumber, chIndex);
                        return;
                    }

                    chIndex++;
                    
                    writefile.WriteLine($"print({word[chIndex].Replace("_s_", " ").Replace("$", "vflvdal")}{append})");
                }
            }
            writefile.Close();
            ccutils.runCmd();
        }
    }
}