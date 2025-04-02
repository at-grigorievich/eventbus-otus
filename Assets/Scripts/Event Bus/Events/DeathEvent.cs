namespace DefaultNamespace.Event_Bus.Events
{
    public struct DeathEvent: IEvent
    {
        public readonly Entity Entity;

        public DeathEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}