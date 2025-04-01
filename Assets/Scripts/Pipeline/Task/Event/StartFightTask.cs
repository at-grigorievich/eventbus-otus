using DefaultNamespace.Event_Bus.Events;
using VContainer;

namespace DefaultNamespace
{
    public class StartFightTask: EventTask
    {
        private readonly EventBus _eventBus;

        public StartFightTask(IObjectResolver resolver)
        {
            _eventBus = resolver.Resolve<EventBus>();
        }
        
        protected override void OnStart()
        {
            _eventBus.Raise(new StartFightEvent());
        }
    }
}