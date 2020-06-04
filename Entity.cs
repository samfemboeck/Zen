using System;
using System.Collections.Generic;
using Zen.Components;

namespace Zen
{
    public class Entity
    {
        public string Name;
        public Transform Transform;
        Machine _machine;
        List<Component> _components = new List<Component>();
        List<Component> _componentsToAdd = new List<Component>();
        List<Component> _componentsToRemove = new List<Component>();
        public List<IDrawable> ToDraw = new List<IDrawable>();
        List<IUpdatable> _toUpdate = new List<IUpdatable>();

        public Entity(string name)
        {
            Name = name;
            Transform = new Transform();
        }

        public void Update() 
        {
            UpdateLists();

            foreach (IUpdatable updatable in _toUpdate)
                updatable.Update();
        }

        public void Draw()
        {
            foreach (IDrawable drawable in ToDraw)
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
                        _toUpdate.Remove(updatable);
                    if (component is IDrawable drawable)
                        ToDraw.Remove(drawable);

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
                        _toUpdate.Add(updatable);
                    if (component is IDrawable drawable)
                        ToDraw.Add(drawable);

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
                component.Mount();
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

        public Component[] GetComponents()
        {
            return _components.ToArray();
        }

        public virtual void OnDestroy() { }
    }
}