using System;
using System.Collections.Generic;
namespace EntityFrameWork
{
  
    public class System : IDisponse
    {
        List<Component> _interest = new List<Component>();
        private World _world;
        public virtual void OnInit(World world)
        {
            _world = world;
        }
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

        public virtual void Disponse()
        {
            _interest.Clear();
        }
    }

    public class BindSystem<T> : System
    {
        public override bool IsInterest(Component component){ return component.GetType() == typeof(T);}
    }

   
}