using System.Collections.Generic;

namespace Zen
{
    public static class EntityManager
    {
        static readonly HashSet<Entity> _entities = new HashSet<Entity>();
        static readonly HashSet<Entity> _entitiesToAdd = new HashSet<Entity>();
        static readonly HashSet<Entity> _entitiesToRemove = new HashSet<Entity>();

        public static HashSet<Entity> Entities => _entities;

        public static void Update()
        {
            UpdateLists();

            foreach (Entity entity in _entities)
                entity.Update();
        }

        static void UpdateLists()
        {
            if (_entitiesToRemove.Count > 0)
            {
                foreach (var entity in _entitiesToRemove)
                {
                    _entities.Remove(entity);
                    entity.OnDestroy();
                }

                _entitiesToRemove.Clear();
            }

            if (_entitiesToAdd.Count > 0)
            {
                foreach (Entity entity in _entitiesToAdd)
                {
                    _entities.Add(entity);
                    entity.OnAdd();
                }

                _entitiesToAdd.Clear();
            }
        }

        public static void AddEntity(Entity entity)
        {
            _entitiesToAdd.Add(entity);
        }

        public static Entity AddEntity(string name)
        {
            Entity entity = new Entity(name);
            _entitiesToAdd.Add(entity);
            return entity;
        }

        public static void RemoveEntity(Entity entity)
        {
            _entitiesToRemove.Add(entity);
        }
    }
}