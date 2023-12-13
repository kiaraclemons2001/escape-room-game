using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class Furniture : IItem
    {
        private String _name;
        private double _weight;
        private double _volume;
        private String _description;
        public String Name { get { return _name; } }
        public double Weight { get { return _weight; } }
        public double Volume { get { return _volume; } }
        public bool PickupAble { get { return false; } }
        public String Description { get { return _description; } set { _description = value; } }
        public String LongName { get { return Name + " weighs " + Weight + " and has a volume of " + Volume + "."; } }
        public bool IsContainer { get { return false; } }
        public bool IsKey { get { return false; } }
        //Designated Constructor
        public Furniture(double volume, double weight, string name, string description)
        {
            _name = name;
            _volume = volume;
            _weight = weight;
            _description = description;
            _decorator = null;
        }

        private IItem _decorator;

        public void AddDecorator(IItem decorator)
        {
            if (_decorator == null)
            {
                _decorator = decorator;
            }
            else
            {
                _decorator.AddDecorator(decorator);
            }
        }
        public Furniture(double volume, double weight, string name) : this(volume, weight, name, "") { }
        public Furniture(double volume, double weight) : this(volume, weight, "", "") { }
        public Furniture(String name) : this(0, 0, name, "") { }
        public Furniture() : this(1, 0, "", "") { }



        
    }
}
