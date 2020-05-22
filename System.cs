using System.Collections.Generic;
namespace EntityFrameWork
{
    public class System : Entity
    {
        public System(World world):base(world){}
        public virtual void OnAwake<T>(Component component, T arg = null){}
        public virtual void OnRemove<T>(Component component, T arg = null){}
        public virtual bool IsInterest(Component component){ return false;}
        public virtual void OnEvent(Component component, Event e){}
        

    }
}