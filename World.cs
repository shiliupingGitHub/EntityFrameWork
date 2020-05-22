using System.Collections.Generic;
using System.Linq;
namespace EntityFrameWork
{
    
    public class World
    {
        Dictionary<Type, System> _systems = new Dictionary<Type, System>();

        List<Entity> _entities = new List<Entity>();

        public void AddEntity(Entity entity)
        {
            if(!_entities.Contains(entity))
                _entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
             if(_entities.Contains(entity))
                _entities.Remove(entity);
        }
        public System[] GetInterstSystems(Component c)
        {
            var result = from s  in _systems.Values where s.IsInterest(c) select s;
            return result.ToArray();
        }

        public System GetSystem<T>()
        {
            var type = typeof(T);

            if(_systems.TryGetValue(type, out var result))
            {
                return result;
            }

            return null;
        }

        public void AddSystem<T>() where T : System
        {
            var type = typeof(T);

            if(!_systems.Contains(type))
            {
                 var system = new T(this);

                _systems[typeof(T)] = system;
            }
           
         
        }

        public void RemoveSystem<T>()
        {
            var type = typeof(T);

            if(_systems.TryGetValue(type, out var system))
            {
                _systems.Remove(type);

                system.Disponse();
                
            }
        }

        public void Tick()
        {
            foreach(var system in _systems)
            {
                switch(system)
                {
                    case ITick tick:
                        tick.Tick();
                    break;
                }
            }
        }
    }
}