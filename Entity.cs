using System.Collections.Generic;
namespace EntityFrameWork
{
    public class Entity{

        public Entity(World w)
        {
            _world = w;
        }
        List<Component> _components = new List<Component>();
        protected World _world;

        public Component AddComponent<T>() where T: Component
        {
            var c = new T(_world);

            _components.Add(c);

           OnAwake(c);

            return c;
        }

        public Component AddComponent<T, W>(W w) where T: Component
        {
            var c = new T(_world);

            _components.Add(c);
    
             OnAwake(c, w);
            return c;
        }


        void OnAwake<T>(Component c, T arg = null)
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

        
        void OnRemove<T>(Component c, T arg = null)
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
            _components.Remove(c);

            OnRemove(c);
        }

        public void RemoveComponent<T>(Component c, T arg)
        {
            _components.Remove(c);

            OnRemove(c, arg);
        }

        public void BroadCastEvent<T>(T e) where T:Event
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