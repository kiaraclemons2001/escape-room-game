using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Text;

namespace StarterGame
{
    public class GameWorld
    {
        private Room _entrance;
        public Room Entrance { get { return _entrance; } }
        private Room _exit;
        private Dictionary<string,Room> _targetRooms;
        

        public void PlayerEnteredRoom(Notification notification)
        {
            //Console.WriteLine("the player entered a room ");
            //Know that you receive notification
            Player player = (Player)notification.Object;
            //Extracting player stored in notification


            if (player != null ) 
            {
                if (player.CurrentRoom == _exit)
                {
                    player.InfoMessage("The player is in " + player.CurrentRoom.Tag);
                    


                }
                Room result = null;
                _targetRooms.TryGetValue(player.CurrentRoom.Tag,out result);
                if (result==(player.CurrentRoom))
                {
                    _targetRooms.Remove(result.Tag);
                    if (_targetRooms.Count == 0)
                    {
                        player.InfoMessage("You visited all of the target rooms");
                        player.InfoMessage("Go back to your Mom's house with all of the items");
                    }
                }
                //player.WarningMessage("The player is in " + player.CurrentRoom.Tag);
            }
        }

        public void PlayerWillLeaveRoom(Notification notification)
        {
            //Console.WriteLine("the player entered a room ");
            //Know that you receive notification
            Player player = (Player)notification.Object;
            //Extracting player stored in notification
            if (player != null)
            {
                Dictionary<string, object> userInfo = new Dictionary<string, object>();

                if (player.CurrentRoom == _exit)
                {
                    
                    userInfo.Add("exit", player.CurrentRoom);
                    notification.UserInfo = userInfo;
                }
                ;
                player.WarningMessage("The player is leaving " + player.CurrentRoom.Tag);
            }
        }

        

        

        private static GameWorld _instance = null;
        public static GameWorld Instance 
        { 
            get 
            {
                if (_instance == null) //lazy version/ only if you need it


                {
                    _instance = new GameWorld();
                }
                return _instance; 
            } 
        }
        private GameWorld()
        {
            _targetRooms = new Dictionary<string, Room>();
            _entrance = CreateWorld();
            NotificationCenter.Instance.AddObserver("PlayerDidEnterRoom", PlayerEnteredRoom);
            NotificationCenter.Instance.AddObserver("PlayerWillLeaveRoom", PlayerWillLeaveRoom);
            //NotificationCenter.Instance.AddObserver("PlayerVistedAllRooms", PlayerVistedAllRooms);
        }

        private Room CreateWorld()
        {
            Room Athens = new Room("in Athens, Greece, in Europe", "What has a head, a tail, but no body?");
            Room London = new Room("in London, England; in Europe", "What is small, brown, and lives in a museum?");
            Room Beijing = new Room("in Beijing, China; in Europe", "What's fragile yet strong, and looks like it's made of gold?");
            Room Bangkok = new Room("in Bangkok, Thailand, in Southeast Asia", "What lights up a room but can be held in your hand?");
            Room Canberra = new Room("in Canberra, Australia, in Austraila", "What can light up the room without being seen?");
            Room Weta = new Room("in Weta Cave in Wellington, New Zealand, by Australia", "What is the one ring to rule them all?");
            Room Lagos = new Room("in Lagos, Nigeria, in Africa", "What has eyes but can't see?");
            Room Cairo = new Room("in Cairo, Egypt; in Africa", "What is white, hard, and often found in buildings?");
            Room Sj = new Room("in San Jose, Costa Rica, in South America", "What is worn by many, seen by all, but known by none?");
            Room rdj = new Room("in Rio de Janeiro, Brazil, in South America", "What has a mouth but cannot speak?");
            Room Mom = new Room("in your mother's house, in Reykjavík, Iceland");


            Door door = Door.ConnectRooms("south", "north", Mom, London);
            RegularLock WinLock =new RegularLock();
            door.TheLock = WinLock;
            door.Close();
            //door.Lock();
            //Different lock
            //Will need all 10 items to get back 

            door = Door.ConnectRooms("east", "west", Sj, London);
            RegularLock LondonLock = new RegularLock();
            door.TheLock = LondonLock;
            door.Close();
            door.Lock();
            


            door = Door.ConnectRooms("north", "south", Lagos, London);
            LondonLock = new RegularLock();
            door.TheLock = LondonLock;
            door.Close();
            door.Lock();

            door = Door.ConnectRooms("west", "east", Athens, London);
            LondonLock = new RegularLock();
            door.TheLock = LondonLock;
            door.Close();
            door.Lock();

            door = Door.ConnectRooms("south", "north", Athens, Cairo);
            RegularLock AthensLock = new RegularLock();
            door.TheLock = AthensLock;
            door.Close();
            door.Lock();

            door = Door.ConnectRooms("south", "north", Sj, rdj);
            RegularLock SanJoseLock = new RegularLock();
            door.TheLock = SanJoseLock;
            door.Close();
            door.Lock();

            door = Door.ConnectRooms("east", "west", rdj, Lagos);
            RegularLock LagosLock = new RegularLock();
            door.TheLock = LagosLock;
            door.Close();
            door.Lock();

            door = Door.ConnectRooms("west", "east", Cairo, Lagos);
            LagosLock = new RegularLock();
            door.TheLock = LagosLock;
            door.Close();
            door.Lock();

            door = Door.ConnectRooms("south", "north", Lagos, Canberra);
            LagosLock = new RegularLock();
            door.TheLock = LagosLock;
            door.Close();
            door.Lock();

            door = Door.ConnectRooms("south", "north", Cairo, Bangkok);
            RegularLock CairoLock = new RegularLock();
            door.TheLock = CairoLock;
            door.Close();
            door.Lock();

            door = Door.ConnectRooms("west", "east", Beijing, Cairo);
            CairoLock = new RegularLock();
            door.TheLock = CairoLock;
            door.Close();
            door.Lock();

            door = Door.ConnectRooms("east", "west", Canberra, Bangkok );
            RegularLock CanberraLock = new RegularLock();
            door.TheLock = CanberraLock;
            door.Close();
            door.Lock();


            door = Door.ConnectRooms("south", "north", Canberra, Weta);
            CanberraLock = new RegularLock();
            door.TheLock = CanberraLock;
            door.Close();
            door.Lock();

            /*
            TrapRoom tr = new TrapRoom("Kabloom");
            Weta.Delegate = tr;
            */

            EchoRoom er = new EchoRoom(5);
            Weta.Delegate = er;




            //universityHall.SetExit("east", universityParking);
            //universityParking.SetExit("west", universityHall);
            //door = Door.ConnectRooms("east","west", universityHall, universityParking);
            //door.Close();

            //universityParking.SetExit("north", parkingDeck);
            //parkingDeck.SetExit("south", universityParking);
            //door = Door.ConnectRooms("north","south", universityParking, parkingDeck);


            //set up special rooms 
            _exit = Mom;
            _targetRooms.Add(Canberra.Tag, Canberra);
            _targetRooms.Add(Bangkok.Tag, Bangkok);
            _targetRooms.Add(Cairo.Tag, Cairo);
            _targetRooms.Add(Weta.Tag, Weta);
            _targetRooms.Add(Beijing.Tag, Beijing);
            _targetRooms.Add(Lagos.Tag, Lagos);
            _targetRooms.Add(Athens.Tag, Athens);
            _targetRooms.Add(rdj.Tag, rdj);
            _targetRooms.Add(Sj.Tag, Sj);
            _targetRooms.Add(London.Tag, London);
            //Drop items

            //Athens
            


            Item beads = new Item(5.0, 3.0, "Worry beads", "Colorful beads that are used to create peace for the wearer");
            Athens.DropItem( beads);

            Furniture chair = new Furniture(10.0, 30.0, "Chair", "A regular chair");
            Athens.DropItem(chair);
            Furniture ottoman = new Furniture(30.0, 100.0, "Ottoman", "A regular ottoman, something you can put your feet on");
            Athens.DropItem(ottoman);

            Item coin = new Item(3.0, 1.0, "Greek coin", "Silver coin that has a Greek goddess, Nike, on the back while has Olympia on the front");
            Athens.DropItem(coin);
            Item AthensKey = new Item(0.0, 0.0, "Key", "The key to unlock the door in Athens");
            coin.AddDecorator(AthensKey);

            Item tavili = new Item(5.0, 10.0, "Tavili", "A Greek backgammon game set");
            Athens.DropItem( tavili);

            //London
            

            Item hat = new Item(0.5, 3.0, "Deerstalker hat", "A small hat that could have been worn by William Shakespeare himself!");
            London.DropItem(hat);

            London.DropItem(chair);

            Item whiskey = new Item(3.0, 5.0, "London Whiskey", "Brown liquor originating from London's own distillery");
            London.DropItem(whiskey);

            
            
            London.DropItem(ottoman);

            Item statue = new Item(4.0, 4.0, "Tiny Two Mice Statue", "The world's smallest statue, made of bronze and depicts two mice looking up");
            London.DropItem(statue);
            Item LondonKey = new Item(0.0, 0.0, "Key", "The key to unlock the door in London");
            statue.AddDecorator(LondonKey);

            Item wand = new Item(1.0, 10.0, "Harry Potter wand", "A wand from the Harry Potter movie");
            London.DropItem(wand);

            //Beijing
            

            Item vase = new Item(6.0, 10.0, "Cloisonne Vase", "Vase decorated with cloisonne or decorated uniquely enamel. Very heavy but very beautiful");
            Beijing.DropItem(vase);

            Beijing.DropItem(chair);
            Beijing.DropItem(ottoman);

            Item scarf = new Item(0.5, 3.0, "Silk scarf", "Beautiful silk scarf that is light as a feather");
            Beijing.DropItem(scarf);
            Item set = new Item(5.0, 30.0, "Calligraphy set", "Beautiful calligraphy set with different inks, golden calligraphy pens, and papers");
            Beijing.DropItem(set);

            Item teacup = new Item(1.0, 3.0, "Fine China teacup", "A porcelain teacup made through the Fine China process, very delicate");
            Beijing.DropItem(teacup);
            Item BeijingKey = new Item(0.0, 0.0, "Key", "The key to unlock the door in Beijing");
            teacup.AddDecorator(BeijingKey);

            //Bangkok
            Item lantern = new Item(2.0, 5.0, "Khom lai lantern", "A beautiful sky lantern that lights up the room that is light as a feather");
            Bangkok.DropItem(lantern);

            Bangkok.DropItem(chair);

            Item BangkokKey = new Item(0.0, 0.0, "Key", "The key to unlock the door in Bangkok");
            lantern.AddDecorator(BangkokKey);

            Item amulet = new Item(1.0, 3.0, "Amulet", "A beautiful amulet that is sparkling and reflecting off of the lantern");
            Bangkok.DropItem(amulet);
            Item buddhist = new Item(5.0, 20.0, "Buddhist Statue", "A brown, heavy Buddhist Statue of Buddha");            
            Bangkok.DropItem(buddhist);
            Item silk = new Item(0.5, 3.5, "Thai silk", "A light weigh fabric made in Thailand which looks like it could be made of gold");
            Bangkok.DropItem(silk);

            
            Bangkok.DropItem(ottoman);

            //Canberra


            Item boomerang = new Item(4.0, 5.0, "Boomerang", "A curved flat piece of wood that can be thrown and will return to thrower");
            Canberra.DropItem(boomerang);

            Canberra.DropItem(chair);

            Item d = new Item(10.0, 30.0, "Didgeridoo", "An Australian Aboriginal wind instrument in the form of a long tube");
            Canberra.DropItem(d);

            
            Canberra.DropItem(ottoman);

            Item candle = new Item(2.0, 3.0, "Nora Candle", "A candle used during the Nora Candle Festival");
            Canberra.DropItem(candle);
            Item CanberraKey = new Item(0.0, 0.0, "Key", "The key to unlock the door in Canberra");
            candle.AddDecorator(CanberraKey);

            Item boots = new Item(3.0, 6.0, "Ugg boots", "Cute boots made by a boots company founded in Australia made of sheepskin");
            Canberra.DropItem(boots);

            //Weta Cave

            Weta.DropItem(chair);

            Item sword= new Item(5.0, 3.0, "Sting Sword of Frodo", "Sword used by Frodo");
            Weta.DropItem(sword);
            Item brooch= new Item(4.0, 2.0, "Elven Brooch", "A brooch made for an elf");
            Weta.DropItem(brooch);

            
           

            Item ring = new Item(1.0, 3.0, "One Ring For All", "Lord of the Rings ring");
            Weta.DropItem(ring);
            Item WetaKey = new Item(0.0, 0.0, "Key", "The key to unlock the door in Weta Cave");
            ring.AddDecorator(WetaKey);

            Item pendant = new Item(2.0, 2.0, "Pendent of Arwen", "Pendant from The Hobbit");
            Weta.DropItem(pendant);

            Weta.DropItem(ottoman);

            //Lagos


            Item udu = new Item(4.0, 10.0, "Udu", "An udu doubles as an instrument and water catcher");
            Lagos.DropItem(udu);

            Lagos.DropItem(chair);
            

            Item cash = new Item(5.0, 3.0, "Nigerian Naira", "Currency used in Nigeria");
            Lagos.DropItem(cash);
            Item LagosKey = new Item(0.0, 0.0, "Key", "The key to unlock the door in Lagos");
            cash.AddDecorator(LagosKey);


            Lagos.DropItem(ottoman);

            Item fabric = new Item(4.0, 2.0, "Asa oke fabric", "Fabric used to make Nigerian party clothes");
            Lagos.DropItem(fabric);
            Item bracelet = new Item(10.0, 20.0, "Beaded bracelet", "Made by the Yoruba tribe, this beaded bracelet is wide and has many layers");
            Lagos.DropItem(bracelet);

            //Cairo
            Cairo.DropItem(ottoman);

            Item alabaster = new Item(6.0, 5.0, "Alabaster", "A small white marble that can be found everywhere in Egypt");
            Cairo.DropItem(alabaster);
            Item CairoKey = new Item(0.0, 0.0, "Key", "The key to unlock the door in Cairo");
            alabaster.AddDecorator(CairoKey);

            Cairo.DropItem(chair);
            

            Item gold = new Item(5.0, 3.0, "Egyptian Gold", "The old currency in Egypt but worth billions!");
            Cairo.DropItem(gold);
            Item pottery = new Item(4.0, 2.0, "Fayoumi pottery", "Beautiful pottery made by the Fayuomi");
            Cairo.DropItem(pottery);
            Item agarwood = new Item(5.0, 10.0, "Agarwood", "Dark wood that produces a fragrant smell when burned");
            Cairo.DropItem(agarwood);

            //San Jose
            Item mask = new Item(8.0, 10.0, "Gigante", "A gigantic masquerade mask modeled to be a colonial Spanish woman nicknamed Gigante");
            Sj.DropItem(mask);
            Item sjKey = new Item(0.0, 0.0, "Key", "The key to unlock the door in Cairo");
            mask.AddDecorator(sjKey);

            Sj.DropItem(chair);
            
            

            Item chocolate = new Item(5.0, 6.0, "Chocolate", "Dark chcolate made in Costa Rica");
            Sj.DropItem(chocolate);
            Item coffee = new Item(6.0, 4.0, "Coffee", "Medium brew coffee made in Costa Rica");
            Sj.DropItem(coffee);

            Sj.DropItem(ottoman);

            Item carcique = new Item(15.0, 10.0, "Carcique", "Alcohol made from sugar cane made in Costa Rica");
            Sj.DropItem(carcique);


            //Rio de Janiero
            

            Item jewel = new Item(5.0, 3.0, "Jewels", "Jewels used for Rio's own Carnival");
            rdj.DropItem(jewel);
            Item ganza = new Item(6.0, 10.0, "Ganza", "Instrument used in samba dancing and used during Carnival");
            rdj.DropItem(ganza);

            rdj.DropItem(chair);
            

            Item barco = new Item(6.0, 5.0, "Barco de Yemania", "A small boat for the goddess Lemanja equipped with offerings");
            rdj.DropItem(barco);
            Item rdjKey = new Item(0.0, 0.0, "Key", "The key to unlock the door in Rio de Janiero");
            barco.AddDecorator(rdjKey);

            rdj.DropItem(ottoman);

            Item lemanja = new Item(15.0, 9.0, "Lemanja Statue", "The statue of the goddess Lemanja");
            rdj.DropItem(lemanja);

















            //Item item = new Item("sword", 1.3f);
            //Item decorator = new Item("ruby", 1.5f);
            //ItemContainer chest = new ItemContainer("chest");

            return Mom;
        }
    }
}
