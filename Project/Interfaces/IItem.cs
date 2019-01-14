using System.Collections.Generic;

namespace spookylab.Project.Interfaces
{
  public interface IItem
  {
    string Name { get; set; }
    string Description { get; set; }
    bool CanBeTaken { get; set; }
    bool CanBeUsed { get; set; }
  }
}