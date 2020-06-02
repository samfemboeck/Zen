using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Zen.EC;
using Zen.Graphics;

namespace Zen
{
    public class Machine
    {
        private readonly HashSet<Entity> _entities = new HashSet<Entity>();
        private readonly HashSet<Entity> _entitiesToAdd = new HashSet<Entity>();
        private readonly HashSet<Entity> _entitiesToRemove = new HashSet<Entity>();
        private readonly List<IDrawable> _drawables = new List<IDrawable>();
        private readonly List<IUpdatable> _updatables = new List<IUpdatable>();
        private SpriteBatch _spriteBatch;
        private Renderer _renderer;

        public ContentManager Content;

        public void Update()
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
                    entity.Machine = this;
                }

                _entitiesToAdd.Clear();
            }

            foreach (Entity entity in _entities)
                entity.Update();

            foreach (IUpdatable updatable in _updatables)
                updatable.Update();
        }

        public void Init(Core core)
        {
            _spriteBatch = core.SpriteBatch;
            _renderer = new Renderer(_spriteBatch);

            LoadContent(core.Content);
            OnContentLoaded();
        }

        public void Draw()
        {
            _renderer.Begin();

            foreach (var drawable in _drawables)
                _renderer.Draw(drawable);

            _renderer.End();
        }

        public void AddEntity(Entity entity)
        {
            _entitiesToAdd.Add(entity);
        }

        public void RegisterComponent(Component component)
        {
            if (component is IUpdatable updatable)
                _updatables.Add(updatable);
            if (component is IDrawable drawable)
                _drawables.Add(drawable);
        }

        public void UnRegisterComponent(Component component)
        {
            if (component is IUpdatable updatable)
                _updatables.Remove(updatable);
            if (component is IDrawable drawable)
                _drawables.Remove(drawable);
        }

        public void RemoveEntity(Entity entity)
        {
            _entitiesToRemove.Add(entity);
        }

        protected virtual void LoadContent(ContentManager content) { }

        protected virtual void OnContentLoaded() { }
    }
}