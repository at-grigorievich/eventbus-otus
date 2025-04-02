namespace DefaultNamespace.Event_Bus.Events
{
    public struct RechargeFightEvent: IEvent
    {
        public readonly Entity Attacker;
        public readonly Entity Defender;

        public RechargeFightEvent(Entity attacker, Entity defender)
        {
            Attacker = attacker;
            Defender = defender;
        }
    }
}