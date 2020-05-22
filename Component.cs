using System.Collections.Generic;
namespace EntityFrameWork
{
    public class Component : Entity
    {
       
       public Entity Owner {get;set;}
       public Component(World world, Entity owner):base(world){Owner = owner;}
    }
}