using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class DropCommand : Command
    {
        public DropCommand() : base()
        {
            this.Name = "drop";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Drop(this.SecondWord);
            }
            else if (parameters[0] != null)
            {

                player.Drop(parameters[0]); //Gets name of item 
            }
            else
            {
                player.WarningMessage("\nPickUp What?");
            }
            return false;
        }
    }
}
