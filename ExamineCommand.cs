using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class ExamineCommand : Command
    {
        public ExamineCommand() : base()
        {
            this.Name = "examine";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Examine(this.SecondWord);
            }
            else if (parameters[0] != null)
            {

                player.Examine(parameters[0]); //Gets name of item 
            }
            else
            {
                player.WarningMessage("\nPickUp What?");
            }
            return false;
        }
    }
}
