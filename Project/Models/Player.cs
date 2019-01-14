using System.Collections.Generic;
using spookylab.Project.Interfaces;

namespace spookylab.Project.Models
{
  public class Player : IPlayer
  {
    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; }


    public Player(string playername)
    {
      PlayerName = playername;
      Inventory = new List<Item>();
    }
  }
}