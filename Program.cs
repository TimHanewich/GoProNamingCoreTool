using System;
using GoProFileNaming;
using System.Collections.Generic;

namespace GoProNamingCoreTool
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //help?
            if (args.Length == 1)
            {
                if (args[0] == "help")
                {
                    Console.WriteLine("Welcome to the GoPro file naming helper tool.");
                    Console.WriteLine("To use, first navigate to the directory that contains your GoPro video files.");
                    Console.WriteLine("Use the keyword 'gpnaming' to trigger the below commands.");
                    Console.WriteLine("Commands: ");
                    Console.WriteLine("rename" + "\t" + "Renames all files in sequential order.");
                    Console.WriteLine("rename --suggest" + "\t" + "Suggest the file renames.");

                    return;
                }
            }


            #region "Set up"
            //Get list of commands
            List<string> Commands = new List<string>();
            foreach (string s in args)
            {
                Commands.Add(s);
            }
            
            //Get current directory
            string dir = Environment.CurrentDirectory;

            #endregion

            //rename?
            if (Commands.Contains("rename"))
            {

                //If it is ONLY rename
                if (Commands.Contains("rename") && Commands.Count == 1)
                {
                    string[] files = System.IO.Directory.GetFiles(dir);
                    List<string> file_names = new List<string>();
                    foreach (string s in files)
                    {
                        file_names.Add(System.IO.Path.GetFileName(s));
                    }

                    GoProFileNamingHelper helper = new GoProFileNamingHelper();
                    SuggestedFileNameChange[] changes = helper.GetNewFileNames(file_names.ToArray());


                    foreach (SuggestedFileNameChange nc in changes)
                    {
                        Console.WriteLine("Changing '" + nc.OldName + "' to '" + nc.NewName + "'...");
                        string old_path = dir + "\\" + nc.OldName;
                        string new_path = dir + "\\" + nc.NewName;
                        System.IO.File.Move(old_path, new_path);
                    }

                    Console.WriteLine("Successfully renamed " + files.Length.ToString("#,##0") + " files.");
                    return;
                }
                else if (Commands.Contains("rename") && Commands.Contains("--suggest") && Commands.Count == 2)
                {
                    string[] files = System.IO.Directory.GetFiles(dir);
                    List<string> file_names = new List<string>();
                    foreach (string s in files)
                    {
                        file_names.Add(System.IO.Path.GetFileName(s));
                    }

                    GoProFileNamingHelper helper = new GoProFileNamingHelper();
                    SuggestedFileNameChange[] changes = helper.GetNewFileNames(file_names.ToArray());

                    Console.WriteLine("Suggested name changes: ");

                    foreach (SuggestedFileNameChange nc in changes)
                    {
                        Console.WriteLine(nc.OldName + " --> " + nc.NewName);
                    }
                }
                


            }
            


        }
    }
}
