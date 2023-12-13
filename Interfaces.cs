using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public interface IItem
    {
        String Name { get; }
        bool PickupAble { get; }
        double Volume { get; }
        double Weight { get; }
        String LongName { get; }
        String Description { get; }
        void AddDecorator(IItem item);
        bool IsContainer { get; }
        bool IsKey { get; }
    }

    public interface IItemContainer : IItem
    {
        void Insert(IItem item);
        IItem Remove(String itemName);
        String ShortDescription { get; }

    }

    public interface IRoomDelegate 
    {
        Room ContainingRoom { get; set; }
        String OnTag (String fromRoom);
        Door OnGetExit(Door door);
        String OnGetExits(String fromRoom);


    
    }

    public interface ICloseable
    {
        bool IsOpen { get; }
        bool IsClosed { get; }
        bool Open();
        bool Close();

    }

    public interface ILockable : ICloseable
    {
        bool IsLocked { get; }
        bool IsUnlocked { get; }
        bool Lock();
        bool Unlock();
        bool OnOpen();
        bool OnClosed();
    }

    

}
