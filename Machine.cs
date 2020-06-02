using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Zen.Graphics;

namespace Zen.EC
{
    public class Machine
    {
        HashSet<Entity> _entities = new HashSet<Entity>();
        HashSet<Entity> _entitiesToAdd = new HashSet<Entity>();
        HashSet<Entity> _entitiesToRemove = new HashSet<Entity>();
        List<IDrawable> _drawables = new List<IDrawable>();
        List<IUpdatable> _updatables = new List<IUpdatable>();
        SpriteBatch _spriteBatch;
        Renderer _renderer;

        public Renderer Renderer
        {
            get => _renderer;
            set
            {
                _renderer = value;
                _renderer.SpriteBatch = _spriteBatch;
            }
        }

        public ContentManager Content;

        public void Update()
        {

            if (_entitiesToRemove.Count > 0)
            {
                foreach (Entity entity in _entitiesToRemove)
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

            if (_renderer == null)
                Renderer = new Renderer();

            LoadContent(core.Content);
            OnContentLoaded();
        }

        public void Draw()
        {
            if (!Renderer.BeginCalled)
                Renderer.Begin();

            foreach (IDrawable drawable in _drawables)
                Renderer.Draw(drawable);

            Renderer.End();
        }

        public void AddEntity(Entity entity)
        {
            _entitiesToAdd.Add(entity);
        }

        public void RegisterComponent(Component component)
        {
            if (component is IUpdatable)
                _updatables.Add(component as IUpdatable);
            if (component is IDrawable)
                _drawables.Add(component as IDrawable);
        }

        public void UnRegisterComponent(Component component)
        {
            if (component is IUpdatable)
                _updatables.Remove(component as IUpdatable);
            if (component is IDrawable)
                _drawables.Remove(component as IDrawable);
        }

        public void RemoveEntity(Entity entity)
        {
            _entitiesToRemove.Add(entity);
        }

        public virtual void LoadContent(ContentManager content) { }

        public virtual void OnContentLoaded() { }
    }
}