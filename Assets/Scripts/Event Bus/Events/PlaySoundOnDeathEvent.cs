namespace DefaultNamespace.Event_Bus.Events
{
    public struct PlaySoundOnDeathEvent: IEvent
    {
        public readonly Entity Entity;

        public PlaySoundOnDeathEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}