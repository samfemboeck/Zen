using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zen
{
    public abstract class Machine
    {
        private readonly HashSet<Entity> _entities = new HashSet<Entity>();
        private readonly HashSet<Entity> _entitiesToAdd = new HashSet<Entity>();
        private readonly HashSet<Entity> _entitiesToRemove = new HashSet<Entity>();
        protected ContentManager Content;

        public void Update()
        {
            UpdateLists();

            foreach (Entity entity in _entities)
                entity.Update();
        }

        public void UpdateLists()
        {
            if (_entitiesToRemove.Count > 0)
            {
                foreach (var entity in _entitiesToRemove)
                {
                    _entities.Remove(entity);
                    entity.UnMount();
                }

                _entitiesToRemove.Clear();
            }

            if (_entitiesToAdd.Count > 0)
            {
                foreach (Entity entity in _entitiesToAdd)
                {
                    _entities.Add(entity);
                    entity.Mount(this);
                }

                _entitiesToAdd.Clear();
            }
        }

        public void Init(Core core)
        {
            Content = core.Content;
            UpdateLists();
            FireUp();
        }

        public void Draw()
        {
            Core.Batcher.Begin();

            foreach (Entity entity in _entities)
                entity.Draw();

            Core.Batcher.End();
        }

        public void AddEntity(Entity entity)
        {
            _entitiesToAdd.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            _entitiesToRemove.Add(entity);
        }

        protected abstract void FireUp();
    }
}