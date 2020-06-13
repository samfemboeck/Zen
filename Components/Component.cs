namespace Zen.Components
{
    public abstract class Component
    {
        public Entity Entity;
        public T GetComponent<T>() where T : Component => Entity.GetComponent<T>();

        public T AddComponent<T>() where T : Component, new() => Entity.AddComponent<T>();

        public void AddComponent(Component component) => Entity.AddComponent(component);

        public void RemoveComponent(Component component) => Entity.RemoveComponent(component);

        public virtual void OnAddedToEntity() {}

        public virtual void AddComponents() {}

        public virtual void Awake() {}

        public virtual void Start() {}

        public virtual void OnDestroy() {}
    }
}