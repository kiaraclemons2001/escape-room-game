using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class RegularLock : ILockable
    {
        
        private bool _locked;
        public bool IsOpen { get { return true; } }
        public bool IsClosed { get { return false; } }
        public bool Open()
        {
            return true;
        }
        public bool Close()
        {
            return true;
        }
        public RegularLock()
        {
            _locked = false;
            NotificationCenter.Instance.AddObserver("PlayerPickedUpKey", PlayerPickedUpKey);
            NotificationCenter.Instance.AddObserver("PlayerDroppedKey", PlayerDroppedKey);
            //NotificationCenter.Instance.AddObserver("PlayerWillLeaveRoom", LockExitRoom);
        }

        /*
        public void LockExitRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            
            if (player!=null)
            {
                Object result = null;
                notification.UserInfo.TryGetValue(player.CurrentRoom.Tag, out result);
                Room room = (Room)result;
                player.CurrentRoom 
            }
        }
        */

        public void PlayerDroppedKey(Notification notification)
        {
            Player player = (Player)notification.Object;
            IItem item = (IItem)notification.UserInfo["key"];
            
            if (player != null)
            {
                Lock();
                Console.WriteLine("You will need " + item.Name + " later ;)");
            }
            
            
            
        }

        public void PlayerPickedUpKey(Notification notification)
        {
            Player player = (Player)notification.Object;
            
            if (player != null)
            {
                Unlock();
                //player.CurrentRoom.GetExits();
                //exit = RoomOnTheOtherSide(player.CurrentRoom);
                //Console.WriteLine();
                //Console.WriteLine(exit.Tag);

            }
            
        }
        public bool IsLocked { get { return _locked; } }
        public bool IsUnlocked { get { return !_locked; } }
        public bool Lock()
        {
            _locked = true;
            return true;
        }
        public bool Unlock()
        {
            _locked = false;
            return true;
        }
        public bool OnOpen()
        {
            //if it is locked, then it should return false
            //return _locked ? false : true;
            return !_locked;
        }
        public bool OnClosed()
        {
            return true;
        }
    }
}
