using System;
using spookylab.Project;
using spookylab.Project.Models;

namespace spookylab
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("\nWelcome to Spooky Lab!");
      System.Console.Write("\nPlease Enter Your Name:");
      string playername = Console.ReadLine();
      Player player = new Player(playername);
      Console.WriteLine($"\nHey {playername}... you awake? \n... \n Good. \n It's me, the trusty voice in your head that likes doing exposition.\n You've just woken up in your office at the Spooky Lab of Mythical Creatures. \nThat no good scientist Dr. Acula decided that he didn't like the fact that no one remembered his birthday was today. All of your other fellow scientists are now gone and you are trapped in the lab and needing to escape before it blows up or something. \n Good luck!\n To access this help menu later, type 'help'. \n To navigate from room to room, please type 'go [north, east, south or west]'\n To take a look at specific items, which you should totally do to inspect things(and maybe find even MORE things), type 'check[item name]. \n To check your inventory, type 'inventory'. \n To take an item with you, type 'take [item name]'. \n To use an item, type 'use [item name]' \n To quit, type 'quit'. \n To reset your game, type 'reset'.");
      Console.WriteLine("\nSo... would you like to start a new game (y/n)");
      string input = Console.ReadLine();
      if (input.ToLower() == "y")
      {
        GameService gameservice = new GameService(player, true);
        gameservice.StartGame();
      }
      System.Console.WriteLine("GoodBye.");




    }
  }
}
