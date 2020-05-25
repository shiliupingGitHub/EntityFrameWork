using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
namespace EntityFrameWork
{
    
    public class World
    {
        Dictionary<Type, System> _systems = new Dictionary<Type, System>();

        List<Entity> _entities = new List<Entity>();

        List<ITick> _ticks = new List<ITick>();

        List<IPreTick> _preTicks = new List<IPreTick>();
        List<ILateTick> _lateTicks = new List<ILateTick>();

        Dictionary<Type, IList> _store = new Dictionary<Type, IList>();
       

        public World()
        {
            _store[typeof(ITick)] = _ticks;
            _store[typeof(IPreTick)] = _preTicks;
            _store[typeof(ILateTick)] = _lateTicks;
        }

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

            if(!_systems.ContainsKey(type))
            {
               
                 var system =  global::System.Activator.CreateInstance(type, this) as System;

                _systems[typeof(T)] = system;

                foreach(var s in _store)
                {
                    if(s.Key.IsAssignableFrom(type))
                    {
                        s.Value.Add(system);
                    }
                }

            }
           
         
        }

        public void RemoveSystem<T>()
        {
            var type = typeof(T);

            if(_systems.TryGetValue(type, out var system))
            {
                _systems.Remove(type);

                system.Disponse();

                foreach(var s in _store)
                {
                    if(s.Key.IsAssignableFrom(type))
                    {
                        s.Value.Remove(system);
                    }
                }
                
            }
        }


        void PreTick()
        {
            foreach(var t in _preTicks)
            {
                t.PreTick();
            }
        }
        public void Tick()
        {
            PreTick();
            foreach(var t in _ticks)
            {
                t.Tick();
            }

            LateTick();
        }

        void LateTick()
        {
            foreach(var t in _lateTicks)
            {
                t.LateTick();
            }

        }
    }
}