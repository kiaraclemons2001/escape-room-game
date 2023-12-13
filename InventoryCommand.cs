using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class InventoryCommand : Command
    {
        public InventoryCommand() : base()
        {
            this.Name = "inventory";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.WarningMessage("\nCannot inventory "+ SecondWord);
            }
            else
            {
                
                player.Inventory();
                
            }
            return false;
        }
    }
}
