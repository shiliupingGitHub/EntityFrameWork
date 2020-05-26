using System;
using System.Collections.Generic;
namespace EntityFrameWork
{
  
    public class System : Entity
    {
        List<Component> _interest = new List<Component>();
        public System(World world):base(world){}
        public virtual void OnAwake<T>(Entity entity,Component component, T arg = default(T))
        {
            if(!_interest.Contains(component))
                _interest.Add(component);
        }

        public virtual void OnEnable(Entity entity, Component component)
        {
             if(!_interest.Contains(component))
                _interest.Add(component);
        }
        public virtual void OnRemove<T>(Component component, T arg = default(T)) 
        {
            _interest.Remove(component);
        }
        public virtual bool IsInterest(Component component){ return false;}
  
    }

    public class BindSystem<T> : System
    {
        public BindSystem(World world) : base(world)
        {
        }

        public override bool IsInterest(Component component){ return component.GetType() == typeof(T);}
    }

   
}