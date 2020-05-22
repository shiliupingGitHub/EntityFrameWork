/*
世间万物都有粒子组成=>所有对象都是粒子=>所有都是实体
*/
using System.Collections.Generic;
namespace EntityFrameWork
{
    public class Entity : IDisponse {
        List<Component> _components = new List<Component>();
        protected World _world;
        public Entity(World w)
        {
            _world = w;
            _world.AddEntity(this);
        }

        public virtual void Disponse()
        {
            foreach(var component in _components)
            {
                component.Disponse();
                OnRemove(component, 0);
            }
            _world = null;
        }
        public Component AddComponent<T>() where T: Component
        {
            
            
            var c = global::System.Activator.CreateInstance(typeof(T), _world, this) as Component;
            
            _components.Add(c);

           OnAwake(c, 0);

            return c;
        }

        public Component AddComponent<T, W>(W w) where T: Component, new()
        {
            var c = global::System.Activator.CreateInstance(typeof(T), _world, this) as Component;

            _components.Add(c);
    
             OnAwake(c, w);
            return c;
        }


        void OnAwake<T>(Component c, T arg = default(T))
        {
             var systems = _world.GetInterstSystems(c);

            if(null != systems)
            {
                foreach(var system in systems)
                {
                     system.OnAwake(c, arg);
                       
                }
            }

        }

        
        void OnRemove<T>(Component c, T arg = default(T))
        {
             var systems = _world.GetInterstSystems(c);

            if(null != systems)
            {
                foreach(var system in systems)
                {
                     system.OnRemove(c, arg);
                       
                }
            }
        }



        public void RemoveComponent(Component c)
        {
            c.Disponse();
            _components.Remove(c);
  
        }

        public void RemoveComponent<T>(Component c, T arg)
        {
             c.Disponse();
            _components.Remove(c);
           
        }

        public void BroadCastEvent<T>(T e)
        {
         
           foreach(var component in _components)
           {
               var systems = _world.GetInterstSystems(component);

               foreach(var system in systems)
               {
                   system.OnEvent(component, e);
               }

               component.BroadCastEvent(e);
           }
        }

 

    }
}