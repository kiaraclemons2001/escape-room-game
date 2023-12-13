using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class Door : ILockable
    {
        private Room _room1;
        private Room _room2;
        private bool _open;
        private ILockable _lock; //like a delegate
        public ILockable TheLock { get { return _lock; } set { _lock = value; } }
        public bool IsLocked { get { return _lock == null ? false : _lock.IsLocked; } }
        public bool IsUnlocked { get { return _lock == null ? true : _lock.IsUnlocked; } }
        public bool IsOpen
        {
            get
            {
                return _open;
            }
        }
        public bool IsClosed
        {
            get
            {
                return !_open;
            }
        }

        public Door(Room room1, Room room2)
        {
            _lock = null; 
            _open = true;
            _room1 = room1;
            _room2 = room2;
            
        }

        public Room RoomOnTheOtherSide(Room from)
        {
            if (from == _room1)
            {
                return _room2;
            }
            else
            {
                return _room1;
            }
        }

        

        public bool Open()
        {
            /*
            if (_lock == null)
            {
                _open = true;
                return true;
            }
            else
            {
                _open = _lock.OnOpen();
                return _open;
            }
            */
            _open = _lock == null ? true : _lock.OnOpen();
            return _open;
        }

        public bool Close() 
        {
            _open = _lock == null ? false : !_lock.OnClosed() ;
            return !_open;
        }

        
        public bool Lock()
        {
            if (_lock!=null)
            {
                _lock.Lock();
                return true;
            }
            
            
            return false;
            
        }
        public bool Unlock()
        {
            if (_lock != null)
            {
                _lock.Unlock();
                return true;
            }
            return false;
            
        }

        
        public bool OnOpen()
        {
            //Can I open?
            return _lock == null ? true : _lock.OnOpen();
        }
        public bool OnClosed()
        {
            return _lock == null ? true : _lock.OnClosed();
        }

        
        public static Door ConnectRooms(String label1, String label2, Room room1, Room room2)
        {
            Door door = new Door(room1, room2);
            room1.SetExit(label1, door);
            room2.SetExit(label2, door);

            return door;
        }

        
    }
}
