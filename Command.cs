using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{

    /*
     * Spring 2023
     */
    public abstract class Command
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        private string _secondWord;
        public string SecondWord { get { return _secondWord; } set { _secondWord = value; } }
        
        protected List<String> parameters;

        public Command()
        {
            this.Name = "";
            this.SecondWord = null;
            parameters= new List<String>();
        }

        public void AddParameter(String parameter)
        {
            parameters.Add(parameter);
        }

        public void ClearParameters()
        {
            parameters.Clear();
        }

        public Command Clone()
        {
            Command clone = (Command)this.MemberwiseClone();
            clone.parameters = new List<string>(parameters);
            clone.Name = Name + "";

            return clone;
        }

        public bool HasSecondWord()
        {
            return this.SecondWord != null;
        }

        public abstract bool Execute(Player player);
    }
}
