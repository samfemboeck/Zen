namespace Zen.Components
{
    public abstract class Component
    {
        public Entity Entity;
        protected T GetComponent<T>() where T : Component => Entity.GetComponent<T>();

        protected T AddComponent<T>() where T : Component, new() => Entity.AddComponent<T>();

        protected void AddComponent(Component component) => Entity.AddComponent(component);

        public virtual void OnAddedToEntity() {}

        public virtual void AddComponents() {}

        public virtual void Awake() {}

        public virtual void Start() {}

        public virtual void Destroy() => Entity.RemoveComponent(this);
    }
}