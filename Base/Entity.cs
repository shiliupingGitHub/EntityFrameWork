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

        public Component AddComponent<T>() where T: Component, new()
        {
            var c = new T();

            _components.Add(c);

            return c;
        }

        public void RemoveComponent(Component c)
        {
            _components.Remove(c);

            

        }


        public Component AddComponent<T, W>(W w) where T: Component, new()
        {
            var c = new T();

            _components.Add(c);
    
             
            return c;
        }

        public void BroadCastEvent<T>(T e) where T:Event
        {
            c.Events.Add(e);
            c.BroadCaseEvent(e);
        }

    }
}