using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace StarterGame
{
    public class Item : IItem 
    {
        private String _name;
        private double _weight;
        private double _volume;
        private String _description;
        public String Name { get { return _name; } }
        public double Weight { get { return _weight; } }
        public double Volume { get {return _volume; } }
        public bool PickupAble { get { return true; } }
        public bool IsContainer { get { return false; } }
        public bool IsKey { get { return _decorator == null ? false : true; } }
        public String Description { get { return _description; } set {_description=value; } }
        public String LongName { get { return Name + " weighs " + Weight + " and has a volume of " + Volume + "."; } }
        //Designated Constructor
        public Item(double weight, double volume, string name, string description) 
        {
            _name=name;
            _volume = volume;
            _weight = weight;
            _description = description;
            _decorator = null;
        }

        private IItem _decorator;

        public void AddDecorator(IItem decorator)
        {
            if (_decorator==null)
            {
                _decorator= decorator;
            }
            else
            {
                _decorator.AddDecorator(decorator);
            }
        }
        public Item(string name, double weight, double volume) :this(weight, volume, name, "DESCRIPTIONLESS") { }
        public Item( double weight, double volume) : this( weight, volume, "NAMELESS", "DESCRIPTIONLESS") { }
        public Item(String name) :this(0,0,name,"") { }
        public Item(String name, double weight) : this(weight, 0, name, "DESCRIPTIONLESS") { }
        public Item():this(0,0, "NAMELESS","DESCRIPTIONLESS") { }



        
    }

    public class ItemContainer : Item, IItemContainer
    {
        private Dictionary<String, IItem> _items = new Dictionary<string, IItem>();
        public new bool IsKey { get { return false; } }
        private double _weight;
        private double _volume;
        public ItemContainer():base() { }
        public ItemContainer(String name) : base(name) {}

        public ItemContainer(string name, double weight) : base(name, weight) { }
        public ItemContainer(string name, double weight, double volume) : base(name, weight, volume) { }
        public ItemContainer(string name, double weight, double volume, String description) : base(weight, volume, name, description) { }

        public new double Weight
        {
            get
            {
                if (_items.Count == 0.0)
                {
                    return _weight;
                }
                else
                {
                    _weight = 0;
                    foreach (IItem item in _items.Values)
                    {
                        _weight += item.Weight;
                        
                    }
                    return _weight;
                }
                
            }
            set { _weight = value; }
        }

        
        public new double Volume
        {
            get
            {
                if (_items.Count == 0.0)
                {
                    return _volume;
                }
                else
                {
                    _volume = 0;
                    foreach (IItem item in _items.Values)
                    {
                        _volume += item.Volume;
                        
                    }
                    return _volume;
                }
                
            }
            set { _volume = value; }
        }

        
        public String ShortDescription
        {
            get
            {
                String output = "";
                foreach (IItem item in _items.Values)
                {
                    output += item.LongName + "\n";
                }
                return LongName + "\n" + output;
            }
        }

        public new String LongName { get { return Name + " weighs " + Weight + " and has a volume of " + Volume + "."; } }

        public new String Description
        {
            get
            {
                String output = "";
                if (_items.Count==0)
                {
                    return "There are no items in the room"; //For Container in room
                }
                else
                {
                    foreach (IItem item in _items.Values)
                    {
                        output += item.Name + " : " + item.Description + "\n";
                    }

                    return "The items in the room are: " + "\n" + output;
                }
                
            }
        }

        public new bool IsContainer { get { return true; } }

        public void Insert(IItem item)
        {
            _items[item.Name] = item;
            
        }
        public IItem Remove(String itemName)
        {
            IItem item = null;
            _items.TryGetValue(itemName, out item);
            _items.Remove(itemName);
            return item;            
        }
    }
}
