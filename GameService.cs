using System;
using System.Collections.Generic;
using spookylab.Project.Interfaces;
using spookylab.Project.Models;

namespace spookylab.Project
{
  public class GameService : IGameService
  {
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    private bool Playing { get; set; }

    public void GetUserInput()
    {
      System.Console.WriteLine("What would you like to do now?");
      string input = Console.ReadLine();
      string[] parsedInput = input.ToLower().Split(" ");
      string command = parsedInput[0];
      string value = "";
      if (input.Length > 1)
      {
        value = input.Replace(command + " ", "");
      }

      switch (command)
      {
        case "check":
          Check(value);
          break;
        case "quit":
          Quit();
          break;
        case "help":
          Help();
          break;
        case "inventory":
          Inventory();
          break;
        case "reset":
          Reset();
          break;
        case "take":
          TakeItem(value);
          break;
        case "use":
          UseItem(value);
          break;
        case "go":
          Go(value);
          break;
        case "guess":
          Guess();
          break;

      }
    }

    public void Go(string direction)
    {

      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        CurrentRoom = CurrentRoom.Exits[direction];
      }
      System.Console.WriteLine("Can't go that way.");
      Console.Clear();

    }

    public void Help()
    {
      Console.Clear();
      System.Console.WriteLine($"Looks like you're needing some help.\n To navigate from room to room, please type 'go [north, east, south or west]'\n To take a look at specific items, which you should totally do to inspect things(and maybe find even MORE things), type 'check[item name]. \n To check your inventory, type 'inventory'. \n To take an item with you, type 'take [item name]'. \n To use an item, type 'use [item name]' \n To quit, type 'quit'. \n To reset your game, type 'reset'.");
    }

    public void Inventory()
    {
      System.Console.WriteLine("\n");
      System.Console.WriteLine("Inventory:");
      foreach (var item in CurrentPlayer.Inventory)
      {
        System.Console.WriteLine($"Item: {item.Name}. {item.Description}");
      }
      System.Console.WriteLine("\n");
    }

    public void Check(string itemName)
    {
      Console.Clear();
      System.Console.WriteLine($"You take a closer look at the {itemName}.");
      {
        Item checkedItem = CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName.ToLower());

        if (checkedItem != null)
        {
          System.Console.WriteLine($"{checkedItem.Description}");
        }
        else
        {
          System.Console.WriteLine("Please ask again, but better.");
        }
      }
      Console.Clear();

    }


    public void Quit()
    {
      System.Console.WriteLine("You have quit the game. You're a quitter.");
      Playing = false;
      Console.Clear();
      return;
    }

    public void Reset()
    {
      {
        CurrentPlayer.Inventory.Clear();

        StartGame();
      }
    }

    public void Setup()
    {
      #region CREATING ROOMS
      Room o = new Room("Office", "You're in your office. In the corner is your desk, a mini-fridge, and a medicine cabinet.");

      Room h1 = new Room("Hallway 1", "You're in Hallway 1. To your left is Lab 1 and to your right is Lab 2. Up ahead is the door to Hallway 2. There's a paperclip on the foor.");
      Room l1 = new Room("Lab 1", "You're in the lab where the Dr. Acula keeps the Sasquatch. There's a tree with a knot in the middle. Your blurry friend sits under the tree.");
      Room l2 = new Room("Lab 2", "You're in the lab where Dr. Acula keeps the Golem. It's a room that's filled with one sentient rock and many non-sentient rocks. In the corner is a bougie CD player.");

      Room h2 = new Room("Hallway 2", "You're in Hallway 2. To your left is Lab 3. To your right is Lab 4. There are a pair of sunglasses on the ground. Up ahead is Hallway 3.");
      Room l3 = new Room("Lab 3", "You're in the lab with the Dragon. It's really hot in here. The dragon is peering at you hungrily from the edge of a synthetic volcano.");
      Room l4 = new Room("Lab 4", "You're in the lab where Dr. Acula keeeps the Banshee. She's over by the synthetic lake. She's singing, and she sounds terrible.");

      Room h3 = new Room("Hallway 3", "You're in Hallway 3. To your left is Lab 5. To your right is Lab 4. Behind you is Hallway 2. Up ahead is the exit. There's a Fire Extinguisher on the wall.");
      Room l5 = new Room("Lab 5", "You're in the lab where Dr. Acula keeps the creepy mutant child. The child looks at you and immediately starts screaming.");
      Room l6 = new Room("Lab 6", "You're in the lab where Dr. Acula keeps the dreaded SpaGhoulie. A cold breeze from nowhere blows through the room, sending chills up your spine. The SpaGhoulie trudges slowly towards you, hand outstretched.");
      Room e = new Room("Exit", "You approach the exit when you see it's blocked by the Sphynx.\n It asks you for the password. If you think you might know it, type 'guess'. If not, go back to the labratory to look for more clues.");

      CurrentRoom = o;
      #endregion

      #region ADDING ROOMS
      o.Exits.Add("north", h1);

      h1.Exits.Add("west", l1);
      h1.Exits.Add("east", l2);
      h1.Exits.Add("north", h2);
      h1.Exits.Add("south", o);

      l1.Exits.Add("east", h1);

      l2.Exits.Add("west", h1);

      h2.Exits.Add("west", l3);
      h2.Exits.Add("east", l4);
      h2.Exits.Add("north", h3);
      h2.Exits.Add("south", h1);

      l3.Exits.Add("east", h2);

      l4.Exits.Add("west", h2);

      h3.Exits.Add("west", l5);
      h3.Exits.Add("east", l6);
      h3.Exits.Add("north", e);
      h3.Exits.Add("south", h2);

      l5.Exits.Add("east", h3);

      l6.Exits.Add("west", h3);

      e.Exits.Add("south", h3);
      #endregion

      #region CREATE ITEMS

      //office
      Item minifridge = new Item("Mini-Fridge", "It's like a fridge, but smaller. The only thing inside it is some old spaghetti.", false);
      Item oldspaghetti = new Item("Old Spaghetti", "They're your ancient leftovers.", true);

      Item desk = new Item("Desk", "It's your cluttered desk. On it, you find a pair of scissors, a Biggie CD, a Brookes and Dunn CD, a Rush CD, a clothespin, a strawhat and a hawaiian shirt.", false);
      Item scissors = new Item("Scissors", "A pair of scissors you  borrowed from Susan.", true);
      Item biggiecd = new Item("Biggie CD", "It's an awesome album.", true);
      Item bndcd = new Item("Brookes and Dunn CD", "It's a pretty good album.", true);
      Item rushcd = new Item("Rush CD", "It's an uh-maze-ing album.", true);
      Item clothespin = new Item("Clothespin", "It's a clothespin", true);
      Item strawhat = new Item("Strawhat", "It's your favorite hat", true);
      Item hawaiianshirt = new Item("Hawaiian Shirt", "It's a stylish shirt", true);

      Item firstaidkit = new Item("Medicine Cabinet", "Most of the items are expired, but they probably still work okay. There are bandaids, coughdrops, and some cottonballs", false);
      Item bandaids = new Item("BandAids", "It's a pack of Band-Aids.", true);
      Item coughdrops = new Item("CoughDrops", "They're lemon-flavored!", true);
      Item cottonball = new Item("CottonBalls", "Super-soft and super versatile.", true);

      //h1
      Item paperclip = new Item("Paperclip", "It's a paperclip.", true);

      //l1
      Item tree = new Item("Tree", "A tree with a knot on it. Inside the knot there's a piece of Paper with the letter 'E' written on it.", false);
      Item candy = new Item("Candy", "It's quite delicious.", true);
      Item ePaper = new Item("Paper", "It's a piece of paper with the letter 'E' written on it.", true);


      //l2
      Item cdplayer = new Item("CD Player", "It appears to play CDs", false);
      Item ePaper2 = new Item("Paper", "It's a small piece of paper with the letter 'E' written on it.", true);

      //h2
      Item sunglasses = new Item("Sunglasses", "These shades will make you look really cool.", true);

      //l3
      Item sPaper = new Item("Paper", "It's a piece of paper with the letter 'S' written on it.", true);

      //l4
      Item lPaper = new Item("Paper", "It's a piece of paper with the letter 'L' written on it.", true);

      //h3
      Item fireextinguisher = new Item("Fire Extinguisher", "It lookes like it extinguishes fires.", true);

      //l5
      Item aPaper = new Item("Paper", "It's a piece of paper with the letter 'A' written on it.", true);

      //l6
      Item pPaper = new Item("Paper", "It's a piece of paper with the letter 'P' written on it.", true);

      //e
      Item magicspoon = new Item("Magic Spoon", "It's a really cool spoon.", true);

      #endregion

      #region ADD ITEMS
      //office
      o.Items.Add(minifridge);
      o.Items.Add(oldspaghetti);

      o.Items.Add(desk);
      o.Items.Add(scissors);
      o.Items.Add(biggiecd);
      o.Items.Add(biggiecd);
      o.Items.Add(bndcd);
      o.Items.Add(rushcd);
      o.Items.Add(clothespin);
      o.Items.Add(strawhat);
      o.Items.Add(hawaiianshirt);



      o.Items.Add(firstaidkit);
      o.Items.Add(bandaids);
      o.Items.Add(coughdrops);
      o.Items.Add(cottonball);

      //h1
      h1.Items.Add(paperclip);

      //l1
      l1.Items.Add(tree);
      l1.Items.Add(candy);
      l1.Items.Add(ePaper);

      //l2
      l2.Items.Add(cdplayer);
      l2.Items.Add(ePaper2);

      //h2 
      h2.Items.Add(sunglasses);

      //l3
      l3.Items.Add(sPaper);

      //l4
      l1.Items.Add(lPaper);

      //h3
      h3.Items.Add(fireextinguisher);

      //l5
      l5.Items.Add(aPaper);

      //l6
      l6.Items.Add(pPaper);

      //e
      e.Items.Add(magicspoon);
      #endregion
    }
    public void StartGame()
    {
      Setup();
      while (Playing)
      {
        System.Console.WriteLine($"{CurrentRoom.Description}");
        GetUserInput();
      }
    }

    public void Guess()
    {

      if (CurrentRoom.Name == "Exit")
      {
        System.Console.Write("Guess Password:");
        string correctGuess = Console.ReadLine().ToLower();

        if (correctGuess == "please")
        {
          System.Console.WriteLine("... 'Please?' you say to the Sphynx hesitantly. \n It looks at you appraisingly. \n 'Also, you have lovely eyes.' you add on for good measure. \n The Sphynx nods its massive head in approval. It almost looks like its blushing a little, like it's totally flattered or something. \n 'Congratulations', it says, 'you've given the correct password. I'll allow you to go.\n But might I request that you release all of us and take us with you? None of us know how to drive. (y/n)?\n");
          string response = Console.ReadLine().ToLower();
          if (response == "n")
          {
            Console.Clear();
            System.Console.WriteLine("'Nope!' You reply defiantly. \n The Sphynx looks as disapproving as a dad whose kid doesn't mow the lawn. \n 'Very well.' He says with a certain sort of determination before proceeding to maul you. \n \n So basically, you blew it. You're totes dead now.\n");
            Reset();
          }
          Console.Clear();
          System.Console.WriteLine("Yes, I'll definitely do that.' You say. \n '(except for t3h SpaGhoulie)You add on under your breath. That creature should only exist in this console game!'\nThe Sphynx nods majestically. \n'Well then,' it continues, 'you have our thanks, stalwart warrior. You've saved the day. Sorry about that password stuff.\n I'm a Sphynx. Riddles and passwords are implicit.\n \n You shrug, and somewhere in the distance you hear an alarm buzzing. It's probably yours. You're always falling asleep on the job.\n");
          Reset();
        }

      }
    }

    public void TakeItem(string itemName)
    {
      Item foundItem = CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName.ToLower());

      if (foundItem != null)
      {
        if (foundItem.CanBeTaken)
        {
          System.Console.WriteLine($"{itemName} has been added to your inventory... thief.");
          CurrentPlayer.Inventory.Add(foundItem);
          CurrentRoom.Items.Remove(foundItem);
        }
        else
        {
          System.Console.WriteLine("You can't take that with you, ya dingus.");
        }
      }
      else
      {
        System.Console.WriteLine($"Are you drunk? There is no such thing as {itemName}.");
      }
    }

    public void UseItem(string itemName)
    {
      Item actualItem = CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName.ToLower());


      if (actualItem == null)
      {
        System.Console.WriteLine("\nCan't use what you don't have");
        GetUserInput();

      }

      #region hawaiianshirt
      else if (actualItem.Name == "Hawaiian Shirt")
      {
        System.Console.WriteLine("\nYou put on your favorite shirt. Lookin' sharp there, slick.");
        CurrentPlayer.Inventory.Remove(actualItem);
      }
      #endregion

      #region o.strawhat

      else if (actualItem.Name == "Strawhat")
      {
        System.Console.WriteLine("\nYou put on your finest hat. You look fantastic.");
        CurrentPlayer.Inventory.Remove(actualItem);
      }

      #endregion

      #region l1.scissors
      else if (CurrentRoom.Name == "Lab 1" && actualItem.Name == "Scissors")
      {
        System.Console.WriteLine("\nYou hold out the scissors and Squatch glances at the sound. He seems shy... embarrassed even? \n He approaches you and gesticulates at his hair. There's some candy stuck in it and he honestly looks like garbage.\n \n You spend the next several minutes cutting the sticky knots out of his hair. As a thank you, he offers you some candy. You should probably take it.");
        CurrentPlayer.Inventory.Remove(actualItem);
      }
      #endregion

      #region l2.biggiecd
      else if (CurrentRoom.Name == "Lab 2" && actualItem.Name == "Biggie CD")
      {
        System.Console.WriteLine("\n You place the Biggie CD into the CD player and 'It was all a dream' starts to play. The Golem wakes, looking super grumpy. It throws a rock at you. You try to dodge, but you can't. \n \n Long story short, you totally died.\n");
        Reset();
      }
      #endregion

      #region l2.brookesanddunncd
      else if (CurrentRoom.Name == "Lab 2" && actualItem.Name == "Brookes and Dunn CD")
      {
        System.Console.WriteLine("\n You place the Brookes and Dunn CD into the CD player and some groovy country tunes start playing. The Golem wakes, looking really disgruntled. It throws a massive rock at you. You try to roll out of the way, but you can't. \n \n Long story short, you totally died.\n");
        Reset();
      }
      #endregion

      #region l2.rushcd
      else if (CurrentRoom.Name == "Lab 2" && actualItem.Name == "Rush CD")
      {
        System.Console.WriteLine("\nYou place the Rush CD in the CD player and the perfect vocals of Geddy Lee start echoing through the room. \n The Golem wakes up and approaches you, looking super stoked. Golems are rock people. OF COURSE they love rock music.\n\n You decide you have a few minutes to spare to rock out with the Golem.\n Looking happier than ever, the Golem offers you a scrap of Paper with the letter 'E' on it. You should probably take it.");
        CurrentPlayer.Inventory.Remove(actualItem);
      }

      #endregion

      #region h2.sunglasses
      else if (actualItem.Name == "Sunglasses")
      {
        System.Console.WriteLine("\nYou put on your shades. +10 Cool Points.");
        CurrentPlayer.Inventory.Remove(actualItem);
      }

      #endregion

      #region l5.candy

      else if (CurrentRoom.Name == "Lab 5" && actualItem.Name == "Candy")
      {
        System.Console.WriteLine("\nThe mutant child shrieks and opens it jaws wide, yellowed teeth glistening in the light as it runs at you. You throw the candy in its mouth with a Steph Curry-worthy shot. Its temporarily stunned, probably long enough for you to take that scrap of Paper on the floor. It has the letter 'A'.");
        CurrentPlayer.Inventory.Remove(actualItem);
      }

      #endregion

      #region l3.fireextinguisher

      else if (CurrentRoom.Name == "Lab 3" && actualItem.Name == "Fire Extinguisher")
      {
        System.Console.WriteLine("\nThe dragon welcomes you with a nasty breath of fire. You dodged, but you definitely singed your eyebrows. You grab the fire extinguisher out of your pack and aim it at the dragon. Before it can attack again., you spray it, rendering the monster as just an adorable giant lizard. With the situation handled, you notice a piece of Paper with the letter 'S' written on it pinned to the wall behind you. You should probably take it.");
        CurrentPlayer.Inventory.Remove(actualItem);
      }

      #endregion

      #region l4.coughdrops

      else if (CurrentRoom.Name == "Lab 4" && actualItem.Name == "Cough Drops")
      {
        System.Console.WriteLine("\nYour ears are about to bleed, so you offer the Banshee some cough drops. She takes them, clearing her throat a little. Her shrieking song still sounds bad, but at least your ears aren't bleeding. As a token of gratitude, she offers you a piece of Paper with the letter 'L' written on it.");
        CurrentPlayer.Inventory.Remove(actualItem);
      }

      #endregion

      #region l6.oldspaghetti

      else if (CurrentRoom.Name == "Lab 6" && actualItem.Name == "Old Spaghetti")
      {
        System.Console.WriteLine("\nThe fearsome Spaghoulie lumbers towards you. You're quivering like a leaf. Nothing is as frightening as this moment. The Spaghoulie absorbs the old spaghetti, increasing drastically in size and power. Thankfully this slows it down. You spot a piece of Paper on the ground with a bit of marinara on it. You can still see it has the letter 'P' written on it. You should probably take it and get out of there as fast as you can.");
        CurrentPlayer.Inventory.Remove(actualItem);
      }

      #endregion

      #region l6.everythingnotspaghetti

      else if (CurrentRoom.Name == "Lab 6" && actualItem.Name != "Old Spaghetti")
      {
        System.Console.WriteLine("\nYou've made a terrible choice. You are absorbed by the Spaghoulie. \n Rest in Pasta");
        Reset();
      }

      #endregion

      #region itwasalladream

      else if (actualItem.Name == "Clothespin")
      {
        System.Console.WriteLine("\nYou pull out the clothespin you found and pinch yourself with it.\n 'Ouch.'\n Suddenly, the room starts to swirl around you while Biggie's 'It was all a Dream' starts playing.\n You fell alseep on the job. AGAIN.");
        Reset();
      }

      #endregion
      else
      {
        System.Console.WriteLine("\nUsing that here doesn't seem to be effective.");

      }
    }

    public GameService(Player currentplayer, bool playing)
    {
      CurrentPlayer = currentplayer;
      Playing = playing;
    }

  }
}
