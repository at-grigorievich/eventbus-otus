using UI;

namespace DefaultNamespace.Hero
{
    public static class HeroExtensions
    {
        public static bool TryGetHeroView(this HeroTeam team, UIService uiService,  Entity entity, out HeroView heroView)
        {
            heroView = null;
            
            int index = team.Heroes.IndexOf(entity);
            
            if (index < 0) return false;
            
            HeroListView heroListView = team.TeamType == TeamType.Blue
                ? uiService.GetBluePlayer()
                : uiService.GetRedPlayer();

            heroView = heroListView.GetView(index);
            
            return true;
        }
    }
}