using System;
using System.Collections.Generic;
namespace EntityFrameWork
{
    public class System : Entity
    {
        List<Component> _interest = new List<Component>();
        public System(World world):base(world){}
        public virtual void OnAwake<T>(Component component, T arg = default(T))
        {
            if(!_interest.Contains(component))
                _interest.Add(component);
        }
        public virtual void OnRemove<T>(Component component, T arg = default(T)) 
        {
            _interest.Remove(component);
        }
        public virtual bool IsInterest(Component component){ return false;}
        public virtual void OnEvent(Component component, Object e){}
      
        
    }

    public class SingleSystem<T> : System
    {
        public SingleSystem(World world) : base(world)
        {
        }

        public override bool IsInterest(Component component){ return typeof(T) == component.GetType();}
    }

    public abstract class SingleSystem<T,A> : SingleSystem<T>
    {
        
        public  SingleSystem(World world):base(world){}
        public override void OnEvent(Component component, Object  e)
        {
            if(e.GetType() == typeof(A))
            {
                OnDoEvent((A)e);
            }
        }

        protected abstract void OnDoEvent(A e);
    }
}