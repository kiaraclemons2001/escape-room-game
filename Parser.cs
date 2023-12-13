using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection.Metadata.Ecma335;

namespace StarterGame
{
    /*
     * Spring 2023
     */
    public class Parser
    {
        private CommandWords _commands;

        public Parser() : this(new CommandWords()){}

        // Designated Constructor
        public Parser(CommandWords newCommands)
        {
            _commands = newCommands;
        }

        public Command ParseCommand(string commandString)
        {
            Command command = null;
            string[] words = commandString.Split(' ');
            //foreach (string word in words) { Console.WriteLine(word); }
            if (words.Length > 0)
            {
                command = _commands.Get(words[0]);
                if (command != null)
                {
                    if (words.Length == 1)
                    {
                        command = command.Clone();
                        return command;
                    }
                    else if (words.Length == 2)
                    {
                        command = command.Clone();
                        command.SecondWord = words[1];
                    }
                    else if (words.Length > 2)
                    {
                        String name = "";
                        int i = 1;
                        while (i != words.Length)
                        {
                            if (i == words.Length - 1)
                            {
                                name += words[i];
                            }
                            else
                            {
                                name += words[i] + " ";
                            }
                            i++;
                        }
                        command = command.Clone();
                        command.AddParameter(name);
                        
                        

                    }
                    else
                    {
                        command.SecondWord = null;
                    }
                    
                }
                else
                {
                    // This is debug line of code, should remove for regular execution
                    //Console.WriteLine(">>>Did not find the command " + words[0]);
                }
            }
            else
            {
                // This is a debug line of code
                //Console.WriteLine("No words parsed!");
            }
            return command;
        }

        public string Description()
        {
            return _commands.Description();
        }
    }
}
