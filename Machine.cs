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
        List<IDrawable> drawables = new List<IDrawable>();
        List<IUpdatable> updatables = new List<IUpdatable>();
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
            foreach (Entity entity in _entities)
                entity.Update();
                
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
                    foreach (Component component in entity.GetComponents())
                    {
                        if (component is IDrawable)
                            drawables.Add(component as IDrawable);
                        if (component is IUpdatable)
                            updatables.Add(component as IUpdatable);
                    }
                }

                _entitiesToAdd.Clear();
            }
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

            foreach (IDrawable drawable in drawables)
                Renderer.Draw(drawable);

            Renderer.End();
        }

        public void AddEntity(Entity entity)
        {
            _entitiesToAdd.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            foreach (Component component in entity.GetComponents())
            {
                if (component is IDrawable)
                    drawables.Remove(component as IDrawable);
                if (component is IUpdatable)
                    updatables.Remove(component as IUpdatable);
            }

            _entities.Remove(entity);
        }

        public virtual void LoadContent(ContentManager content) { }

        public virtual void OnContentLoaded() { }
    }
}