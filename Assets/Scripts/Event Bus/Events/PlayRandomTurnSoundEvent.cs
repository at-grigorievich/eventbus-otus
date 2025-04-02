namespace DefaultNamespace.Event_Bus.Events
{
    public struct PlayRandomTurnSoundEvent: IEvent
    {
        public readonly Entity Entity;

        public PlayRandomTurnSoundEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}