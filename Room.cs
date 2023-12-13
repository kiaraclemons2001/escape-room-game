using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;

namespace StarterGame
{
    /*
     * Spring 2023
     */
    public class Room
    {
        private Dictionary<string, Door> _exits;
        private string _tag;
        public string Tag { get { return _delegate == null ? _tag : _delegate.OnTag(_tag); } set { _tag = value; } }
        //private Dictionary<string, IItem> _items;
        //private IItem _item;
        private String _riddle;
        public String Riddle { get { return _riddle; } set { _riddle = value; } }
        public String ItemsInRoom {
            get
            {
                return _items.Description;

            }
        }

        private IItemContainer _items;

        
            
            
        public IItemContainer Items
        {
            get
            {
                return _items;

            }
        }

        private IRoomDelegate _delegate;
        public IRoomDelegate Delegate 
        { set 
            { 
                if ( value!=null && value.ContainingRoom!=null)
                {
                    value.ContainingRoom.Delegate = null;
                }
                _delegate = value; 
                if (_delegate!= null)
                {
                    _delegate.ContainingRoom = this;
                }
            } 
            get { return _delegate; }
        }
        public Room() : this("No Tag"){}

        // Designated Constructor
        public Room(string tag, string riddle)
        {
            _riddle = riddle;
            _delegate = null;
            _exits = new Dictionary<string, Door>();
            this.Tag = tag;
            _items = new ItemContainer("Floor", 0.0, 0.0, "All of the items in the room");
        }

        public Room(string tag): this(tag, ""){  }

        

        public void SetExit(string exitName, Door door)
        {
            _exits[exitName] = door;
        }

        public Door GetExit(string exitName)
        {
            Door door = null;
            _exits.TryGetValue(exitName, out door);
            return _delegate==null?door:_delegate.OnGetExit(door);
        }

        public string GetExits()
        {
            string exitNames = "Exits: ";
            Dictionary<string, Door>.KeyCollection keys = _exits.Keys;
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }

            return _delegate==null?exitNames:_delegate.OnGetExits(exitNames);
        }

        public string Description()
        {
            return "You are " + this.Tag + ".\n *** " + this.GetExits();
        }

        
        public void DropItem(IItem item)
        {
            _items.Insert(item);
            
        }

        public IItem PickUpItem(String name)

        {
            IItem item = _items.Remove(name);
            return item;
        }

        /*
        public void AddItem(string itemName, IItem item)
        {
            _items[itemName] = item;
        }

        public IItem GetItem(string itemName)
        {
            String _itemName=itemName.ToLower();
            IItem item = null;
            _items.TryGetValue(_itemName, out item);
            return item;
        }

        public string GetItems()
        {
            string itemNames = "Items: ";
            //Dictionary<string, IItem>.KeyCollection keys = _items.Keys;
            Dictionary<string, IItem>.ValueCollection items= _items.Values;
            foreach (IItem item in items)
            {
                itemNames += " " + item.Name;
            }

            return itemNames;
        }

        public void RemoveItem(string itemName)
        {
            _items.Remove(itemName);
        }
        */
    }

    /*
     * IRoomDelegates are defined here
     * 
     */

    /*
    public class TrapRoom : IRoomDelegate
    {

        private bool _active;
        private String _password;
        private Room _containingRoom;
        public Room ContainingRoom
        {
            set
            {
                _containingRoom = value;
            }
            get { return _containingRoom; }

        }
        public TrapRoom(String ps)
        {
            _password = ps;
            _active = true;
            NotificationCenter.Instance.AddObserver("PlayerDidSayAWord", OnPlayerDidSayAWord);
        }        

        public String OnTag(String fromRoom)
        {

            return _active?"in a TRAP ROOM":fromRoom;
        }
        public Door OnGetExit(Door door)
        {
            return _active?null:door;
        }
        public String OnGetExits(String fromRoom)
        {
            return _active?"Muahahaha! \n":"" + fromRoom;
        }

        public void OnPlayerDidSayAWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null) 
            {
                if (player.CurrentRoom == ContainingRoom)
                {
                    String word = (String)notification.UserInfo["word"];
                    if (word.Equals(_password))
                    {
                        _active = false;
                        player.InfoMessage("The trap has been disabled");
                        player.InfoMessage(player.CurrentRoom.Description());
                    }
                    else
                    {
                        player.ErrorMessage("Ah, ah, ah, you didn't say the magic word.");
                    }
                }
                
            }
        }
    }
    */

    public class EchoRoom : IRoomDelegate

    {
        private int _times;
        private Room _containingRoom;
        public Room ContainingRoom
        {
            set
            {
                _containingRoom = value;
            }
            get { return _containingRoom; }

        }
        public EchoRoom(int times)
        {
            _times = times;
            NotificationCenter.Instance.AddObserver("PlayerDidSayAWord", OnPlayerDidSayAWord);

        }

        public void OnPlayerDidSayAWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                if (player.CurrentRoom == ContainingRoom)
                {
                    String word = (String)notification.UserInfo["word"];
                    String echo = "";
                    for (int i = 0; i < _times; i++)
                    {
                        echo += word + " ";
                    }
                    player.NormalMessage(echo);
                }

            }
        }

        public String OnTag(String fromRoom)
        {

            return "*" + fromRoom;
        }
        public Door OnGetExit(Door door)
        {
            return door;
        }
        public String OnGetExits(String fromRoom)
        {
            return fromRoom;
        }
    }
}

