using System.Collections.Generic;
using spookylab.Project.Interfaces;

namespace spookylab.Project.Models
{
  public class Item : IItem
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CanBeTaken { get; set; }
    // public string Inspect { get; set; }

    public bool CanBeUsed { get; set; }

    public Item(string name, string description, bool canbetaken)
    {
      Name = name;
      Description = description;
      CanBeTaken = canbetaken;
      // Inspect = inspect;
    }
  }
}