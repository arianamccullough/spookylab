using System.Collections.Generic;
using spookylab.Project.Models;

namespace spookylab.Project.Interfaces
{
  public interface IPlayer
  {
    string PlayerName { get; set; }
    List<Item> Inventory { get; set; }
  }
}