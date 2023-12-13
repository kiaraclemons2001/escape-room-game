using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    /*
     * Spring 2023
     */
    public class HelpCommand : Command
    {
        private CommandWords _words;

        public HelpCommand() : this(new CommandWords()){}

        // Designated Constructor
        public HelpCommand(CommandWords commands) : base()
        {
            _words = commands;
            this.Name = "help";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.WarningMessage("\nI cannot help you with " + this.SecondWord);
            }
            else
            {
                string objective = "Your mom wants you to go around the world and get specific items. \nShe has left you with riddles along the way to figure out what item she wants. \nTo leave that city, you must have the item. \nIf you try to leave the city without the item 3 times, you lose the game. \nGood luck!";
                player.InfoMessage(objective+ "\n\nYour available commands are " + _words.Description());
            }
            return false;
        }
    }
}
