using System.Collections.Generic;

namespace Zen.EC
{
    public class Entity
    {
        public string Name;
        public Transform Transform;
        public Machine Machine;
        List<Component> _components = new List<Component>();
        List<Component> _componentsToAdd = new List<Component>();
        List<Component> _componentsToRemove = new List<Component>();
        List<Component> _tempBufferList = new List<Component>();

        public Entity(string name)
        {
            Name = name;
            Transform = new Transform();
        }

        public void Update() 
        {
            if (_componentsToRemove.Count > 0)
            {
                foreach (Component component in _componentsToRemove)
                {
                    Machine.UnRegisterComponent(component);
                    _components.Remove(component);
                    component.OnDestroy();
                }

                _componentsToRemove.Clear();
            }

            if (_componentsToAdd.Count > 0)
            {
                foreach (Component component in _componentsToAdd)
                {
                    _components.Add(component);
                    _tempBufferList.Add(component);
                }

                _componentsToAdd.Clear();

                foreach (Component component in _tempBufferList)
                {
                    Machine.RegisterComponent(component);
                    component.Entity = this;
                    component.Awake();
                }

                _tempBufferList.Clear();
            }
        }

        public void AddComponent(Component component)
        {
            _componentsToAdd.Add(component);
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