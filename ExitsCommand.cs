using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class ExitsCommand : Command
    {
        public ExitsCommand() : base()
        {
            this.Name = "exits";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            { 
            
                player.WarningMessage("\nCannot exits with " + SecondWord);
            }
            else
            {

                player.Exits();

            }
            return false;
        }
    }
}
