using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class BackCommand : Command
    {
        public BackCommand() : base()
        {
            this.Name = "back";
        }

        override
        public bool Execute(Player player)
        {
           
            if (this.HasSecondWord())
            {
                player.WarningMessage("\nI cannot back with" + this.SecondWord);
                
            }
            else
            {
                player.Back();
            }
            return false;
        }
    }

    
}
