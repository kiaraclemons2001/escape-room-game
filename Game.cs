using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    /*
     * Spring 2023
     */
    public class Game
    {
        
        private Player _player;
        private Parser _parser;
        private bool _playing;

        public Game()
        {
            
            _playing = false;
            _parser = new Parser(new CommandWords());
            _player = new Player(GameWorld.Instance.Entrance);
            NotificationCenter.Instance.AddObserver("PlayerEnteredRoom", PlayerWonTheGame);
            //NotificationCenter.Instance.AddObserver("PlayerLostTheGame", PlayerLostTheGame);
        }

       

        /**
     *  Main play routine.  Loops until end of play.
     */
        public void Play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.

            bool finished = false;
            while (!finished)
            {
                Console.Write("\n>");
                Command command = _parser.ParseCommand(Console.ReadLine());
                if (command == null)
                {
                    _player.ErrorMessage("I don't understand...");
                }
                else
                {
                    finished = command.Execute(_player);
                }
            }
        }


        public void Start()
        {
            _playing = true;
            _player.InfoMessage(Welcome());
        }

        public void End()
        {
            _playing = false;
            _player.InfoMessage(Goodbye());
        }

        public void PlayerLostTheGame(Notification notification) //Lose notification, when the player loses the game it will tell them they lost and end the game 
        {
            Player player = (Player)notification.Object;
            if (player!=null)
            {
                _player.LostMessage("\nOops! You've tried guessing three times. This means you lost");
                End();
            }
            
        }

        public void PlayerWonTheGame(Notification notification) //Win notification, when player wins the game it will tell them congratulations and end the game 
        {
            Player player = (Player)notification.Object;
            if (player!=null)
            {
                _player.WinMessage("\nCongratulations! You have won the game! \nYou have made it back to your mother's house with all of the items!");
                End();
            }
            
        }

        public string Welcome()
        {
            String game = "Welcome to the International Escape Room Game.";
            String objective = "Your mom wants you to go around the world and get specific items. \nShe has left you with riddles along the way to figure out what item she wants. \nTo leave that city, you must have the item. and open the door of the city \nIf you try to leave the city without the item 3 times, you lose the game. \nGood luck!";
            String hint = "\n\nYou only can carry up to a certain amount of weight and volume so be mindful of what you pick up ;)";
            return game+ " \n"+objective+ hint+"\n\nType 'help' if you need help. \n" + _player.CurrentRoom.Description();
        }

        public string Goodbye()
        {
            return "\nThank you for playing, Goodbye. \n";
        }

    }
}
