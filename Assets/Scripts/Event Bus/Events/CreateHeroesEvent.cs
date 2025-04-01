namespace DefaultNamespace.Event_Bus.Events
{
    public struct CreateHeroesEvent: IEvent
    {
        public Hero.Team TeamType;
        public int HeroCount;
    }
}