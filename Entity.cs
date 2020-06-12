using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Zen.Components;

namespace Zen
{
    public class Entity
    {
        public string Name;
        List<Component> _components = new List<Component>();
        List<Component> _componentsToAdd = new List<Component>();
        List<Component> _componentsToRemove = new List<Component>();
        List<Component> _tmpComponentList = new List<Component>();
        List<IDrawable> _drawables = new List<IDrawable>();
        List<IUpdatable> _updatables = new List<IUpdatable>();
        ContentManager _content;

        public List<IDrawable> Drawables => _drawables;

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
                for(int i = _componentsToRemove.Count - 1; i >= 0; i--)
                {
                    _components.Remove(_componentsToRemove[i]);

                    if (_componentsToRemove[i] is IUpdatable updatable)
                        _updatables.Remove(updatable);
                    if (_componentsToRemove[i] is IDrawable drawable)
                        _drawables.Remove(drawable);
                }

                _componentsToRemove.Clear();
            }

            if (_componentsToAdd.Count > 0)
            {
                foreach (Component component in _componentsToAdd)
                {
                    _tmpComponentList.Add(component);
                    component.Entity = this;
                }

                foreach (Component component in _tmpComponentList)
                {
                    component.AddComponents();
                }

                _tmpComponentList.Clear();

                foreach (Component component in _componentsToAdd)
                {
                     _components.Add(component);
                     _tmpComponentList.Add(component);
                     component.Entity = this;

                    if (component is IUpdatable updatable)
                        _updatables.Add(updatable);
                    if (component is IDrawable drawable)
                        _drawables.Add(drawable);
                }

                foreach (Component component in _tmpComponentList)
                {
                    component.Awake();
                }

                foreach (Component component in _tmpComponentList)
                {
                    component.Start();
                }

                _tmpComponentList.Clear();
                _componentsToAdd.Clear();
            }
        }

        public void OnAdd()
        {
            UpdateLists();
        }

        public void OnDestroy()
        {
        }

        public void AddComponent(Component component)
        {
            _componentsToAdd.Add(component);
            component.Entity = this;
            component.OnAddedToEntity();
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
    }
}