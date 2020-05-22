using System.Collections.Generic;
namespace EntityFrameWork
{
    public class Component : Entity
    {
        public List<Event> Events = new List<Event>();
    }
}