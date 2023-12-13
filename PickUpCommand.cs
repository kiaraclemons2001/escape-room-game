using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class PickUpCommand : Command
    {
        public PickUpCommand() : base()
        {
            this.Name = "pickup";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.PickUp(this.SecondWord);
            }
            else if (parameters[0]!= null)
            {

                player.PickUp(parameters[0]); //Gets name of item 
            }
            else
            {
                player.WarningMessage("\nPickUp What?");
            }
            return false;
        }
    }
}
