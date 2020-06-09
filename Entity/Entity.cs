using System;
using System.Collections.Generic;
using Zen.Components;

namespace Zen
{
    public class Entity
    {
        public string Name;
        Machine _machine;
        List<Component> _components = new List<Component>();
        List<Component> _componentsToAdd = new List<Component>();
        List<Component> _componentsToRemove = new List<Component>();
        List<IDrawable> _drawables = new List<IDrawable>();
        List<IUpdatable> _updatables = new List<IUpdatable>();

        public Entity(string name)
        {
            Name = name;
        }

        public void Update() 
        {
            UpdateLists();

            foreach (IUpdatable updatable in _updatables)
                updatable.Update();
        }

        public void Draw()
        {
            foreach (IDrawable drawable in _drawables)
                drawable.Draw();
        }

        public void UpdateLists()
        {
            if (_componentsToRemove.Count > 0)
            {
                foreach (Component component in _componentsToRemove)
                {
                    _components.Remove(component);

                    if (component is IUpdatable updatable)
                        _updatables.Remove(updatable);
                    if (component is IDrawable drawable)
                        _drawables.Remove(drawable);

                    component.Destroy();
                }

                _componentsToRemove.Clear();
            }

            if (_componentsToAdd.Count > 0)
            {
                foreach (Component component in _componentsToAdd)
                {
                    _components.Add(component);

                    if (component is IUpdatable updatable)
                        _updatables.Add(updatable);
                    if (component is IDrawable drawable)
                        _drawables.Add(drawable);

                    component.Register(this);
                }

                _componentsToAdd.Clear();
            }
        }

        public void Mount(Machine machine)
        {
            _machine = machine;
            UpdateLists();
            
            foreach (Component component in _components)
            {
                component.PreMount();
            }

            foreach (Component component in _components)
            {
                component.Mount();
            }
        }

        public void UnMount()
        {
            _machine = null;
        }

        public void AddComponent(Component component)
        {
            _componentsToAdd.Add(component);
        }

        public T AddComponent<T>() where T : Component, new()
        {
            var component = new T();
            _componentsToAdd.Add(component);
            return component;
        }

        public void RemoveComponent(Component component)
        {
            _componentsToRemove.Add(component);
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in _components)
            {
                if (component is T)
                    return component as T;
            }
            
            return null;
        }

        public List<Component> GetComponents()
        {
            return _components;
        }

        public void UpdateTransform()
        {

        }

        public List<IDrawable> Drawables { get => _drawables; }

        public virtual void OnDestroy() { }
    }
}