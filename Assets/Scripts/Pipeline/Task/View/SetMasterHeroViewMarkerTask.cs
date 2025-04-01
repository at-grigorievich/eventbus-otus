using DefaultNamespace.Hero;
using UI;

namespace DefaultNamespace
{
    public class SetMasterHeroViewMarkerTask: EventTask
    {
        private readonly HeroTeamsService _heroTeamsService;
        private readonly UIService _uiService;

        public SetMasterHeroViewMarkerTask(HeroTeamsService heroTeamsService, UIService uiService)
        {
            _heroTeamsService = heroTeamsService;
            _uiService = uiService;
        }
        
        protected override void OnStart()
        {
            ChangeMarkerByTeam(TeamType.Blue);
            ChangeMarkerByTeam(TeamType.Red);
            Complete();
        }

        private void ChangeMarkerByTeam(TeamType teamType)
        {
            var entities = _heroTeamsService[teamType].Heroes;
            var views = teamType == TeamType.Blue ? _uiService.GetBluePlayer() : _uiService.GetRedPlayer();

            for (int i = 0; i < entities.Count; i++)
            {
                bool isMaster = entities[i].TryGetComponent(out MasterMarker _);
                views.GetView(i).SetActive(isMaster); 
            }
        }
    }
}