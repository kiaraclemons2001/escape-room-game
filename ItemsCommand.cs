using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class ItemsCommand : Command
    {
        public ItemsCommand() : base()
        {
            this.Name = "items";
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

                player.Items();

            }
            return false;
        }
    }
}
