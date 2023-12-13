using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.ConstrainedExecution;
using System.Diagnostics.Contracts;

namespace StarterGame
{
    /*
     * Spring 2023
     */
    public class Player
    {
        private Room _currentRoom = null;
        public Room CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }

        readonly private double _weightCapacity = 100.0;
        readonly private double _volumeCapacity = 100.0;

        private Stack<String> _directions;

        public Player(Room room)
        {
            _currentRoom = room;
            _inventory = new ItemContainer("Inventory", 0.0, 0.0, "Keeps all of your items");
            _directions = new Stack<string>();
            
        }

        private IItemContainer _inventory;





        public void WaltTo(string direction)
        {
            Door door = this.CurrentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.IsOpen)
                {

                    Room nextRoom = door.RoomOnTheOtherSide(CurrentRoom);
                    Notification notification = new Notification("PlayerWillLeaveRoom", this);
                    
                    NotificationCenter.Instance.PostNotification(notification);
                    CurrentRoom = nextRoom; //event
                    notification = new Notification("PlayerDidEnterRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    NormalMessage("\n" + this.CurrentRoom.Description());
                    RiddleMessage("\nThis room's riddle:\n\t" + this.CurrentRoom.Riddle);
                    NormalMessage(CurrentRoom.ItemsInRoom);
                    _directions.Push(direction);
                    //notification = new Notification("PlayerWillLeaveRoom", this);
                }
                else
                {
                    ErrorMessage("\nThe door on " + direction + " is closed.");
                }

            }
            else
            {
                ErrorMessage("\nThere is no door on " + direction);
            }
        }

        public void Say(string word)
        {
            Notification notification = new Notification("PlayerWillSayAWord", this);
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            //userInfo.Add("word" , word); Same thing 
            notification.UserInfo = userInfo;
            NotificationCenter.Instance.PostNotification(notification);
            NormalMessage(word);
            notification = new Notification("PlayerDidSayAWord", this);
            notification.UserInfo = userInfo;
            NotificationCenter.Instance.PostNotification(notification);

        }

        public void Open(string direction)
        {
            Door door = this.CurrentRoom.GetExit(direction);
            if (door != null)
            {
                
               

                door.Open();
                if (door.IsOpen)
                {
                    InfoMessage("\nThe door on " + direction + " is now opened");
                    
                }
                else
                {
                    ErrorMessage("\nThe door on " + direction + " is closed.");
                    RiddleMessage("\nYou must have the item that solves the riddle to open the door");
                }

            }
            else
            {
                ErrorMessage("\nThere is no door on " + direction);
                
            }
        }

        public IItem Take(String itemName)
        {
            return _inventory.Remove(itemName);
        }

        public void Back()
        {
            if (_directions.Count == 1) //only 1 direction in stack
            {
                WarningMessage("You cannot go back any further");
            }
            else //More than 1 direction in stack
            {
                String direction = _directions.Pop();
                //String[] directionsArray = _directions.ToArray();
               // Console.WriteLine("The directions stored in the stack is: ");
               /* foreach (String i in directionsArray)
                {
                    Console.WriteLine(i);
                }
               */
                String oppositeDirection;
                //Console.WriteLine("The direction for the stack is: " + direction);
                switch (direction)
                {
                    case "south":
                        oppositeDirection = "north";
                        break;
                    case "north":
                        oppositeDirection = "south";
                        break;
                    case "east":
                        oppositeDirection = "west";
                        break;
                    case "west":
                        oppositeDirection = "east";
                        break;
                    default:
                        oppositeDirection = "";
                        break;

                }
                if (oppositeDirection.Equals(""))
                {
                    ErrorMessage("There was an error in going back");

                }
                else
                {
                    WaltTo(oppositeDirection);
                    //Console.WriteLine("The opposite direction is: " + oppositeDirection);
                    _directions.Pop(); //gets rid of the last direction from Walt To
                }


            }

        }

        public void Inventory()
        {
            NormalMessage(_inventory.ShortDescription);
        }

        public void PickUp(String itemName)
        {
            
            IItem item = CurrentRoom.PickUpItem(itemName);
            if (item != null)
            {
                if (item.PickupAble)
                {

                    if ((_inventory.Weight + item.Weight) < _weightCapacity && (_inventory.Volume + item.Volume) < _volumeCapacity)
                    {
                        Give(item);
                        NormalMessage("You picked up " + itemName);
                        _inventory.Insert(item);
                        if (item.IsKey)
                        {
                            Notification notification = new Notification("PlayerPickedUpKey", this); //open the door  
                            Dictionary<string, object> userInfo = new Dictionary<string, object>();
                            userInfo["key"] = item;
                            
                            notification.UserInfo = userInfo;
                            NotificationCenter.Instance.PostNotification(notification);
                            

                        }
                        else
                        {

                            //increase counter for losing the game
                            //if lost, call notification 
                        }
                        
                    }
                    else
                    {
                        WarningMessage("\n You can no longer hold anymore items, please drop some items to be able to hold more");
                    }
                }
                else
                {
                    ErrorMessage("\n" + itemName + " is not able to be picked up");
                }

            }
            else
            {
                ErrorMessage("There is no item named " + itemName + " in the room");
            }
        }

        public void Examine(string itemName)
        {
            //String _itemName = itemName.ToLower();
            IItem item = CurrentRoom.PickUpItem(itemName);
            if (item != null)
            {
                InfoMessage(item.LongName);
                CurrentRoom.DropItem(item);
            }
            else
            {
                ErrorMessage("There is no item named " + itemName);
            }
        }



        public void Drop(String itemName)
        {
            IItem item = Take(itemName);
            if (item != null)
            {
                CurrentRoom.DropItem(item);
                NormalMessage("You dropped " + itemName);
                if (item.IsKey)
                {
                    Notification notification = new Notification("PlayerDroppedKey", this); //close the door

                    NotificationCenter.Instance.PostNotification(notification);
                }
            }
            else
            {
                ErrorMessage("You don't have an item named " + itemName);
            }
        }

        public void Give(IItem item)
        {
            _inventory.Insert(item);
        }

        public void Exits()
        {
            NormalMessage("\n" + this.CurrentRoom.Description());
            
        }

        public void Items()
        {
            NormalMessage("\n" + this.CurrentRoom.ItemsInRoom);
        }

        /*
        public void Extract(String itemName, String containerName)
        {
            IItem item = CurrentRoom.PickUpItem(containerName);
            if (item != null)
            {
                if (item.IsContainer)
                {
                    IItemContainer container = (IItemContainer)item;
                    item = container.Remove(itemName);
                    if (item != null)
                    {
                        Give(item);
                        NormalMessage("You extracted " + itemName + " from " + containerName);
                    }
                    else
                    {
                        ErrorMessage("The item " + itemName + "is not in " + containerName);


                    }
                    CurrentRoom.DropItem(container);
                }
                else
                {
                    ErrorMessage(containerName + " is not a container");
                }
            }
            else
            {
                ErrorMessage("There is no container named " + containerName);
            }
        }
        */



        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ColoredMessage(string message, ConsoleColor newColor)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = newColor;
            OutputMessage(message);
            Console.ForegroundColor = oldColor;
        }

        public void NormalMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.White);
        }

        public void RiddleMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Green);
        }

        public void InfoMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Blue);
        }

        public void WarningMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.DarkYellow);
        }

        public void ErrorMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Red);
        }

        public void LostMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Yellow);
        }

        public void WinMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Magenta);
        }

    }

}
