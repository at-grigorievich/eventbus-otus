using Cysharp.Threading.Tasks;
using DefaultNamespace.Hero;
using UI;
using VContainer;

namespace DefaultNamespace
{
    public class AttackVisualTask: EventTask
    {
        private readonly Entity _attackerEntity;
        private readonly Entity _defenderEntity;

        private readonly UIService _uiService;
        private readonly HeroTeamsService _heroTeamsService;

        public AttackVisualTask(Entity attackerEntity, Entity defenderEntity, IObjectResolver resolver)
        {
            _attackerEntity = attackerEntity;
            _defenderEntity = defenderEntity;
            
            _uiService = resolver.Resolve<UIService>();
            _heroTeamsService = resolver.Resolve<HeroTeamsService>();
        }
        
        protected override void OnStart()
        {
            TeamType defenderTeam = (TeamType)_defenderEntity.GetComponent<Team>().Value;
            TeamType attackerTeam = (TeamType)_attackerEntity.GetComponent<Team>().Value;
            
            _heroTeamsService[defenderTeam].TryGetHeroView(_uiService, _defenderEntity, out HeroView defenderView);
            _heroTeamsService[attackerTeam].TryGetHeroView(_uiService, _attackerEntity, out HeroView attackerView);
            
            AnimateAttack(attackerView, defenderView).Forget();
        }

        private async UniTask AnimateAttack(HeroView attackerView, HeroView defenderView)
        {
            await attackerView.AnimateAttack(defenderView);
            Complete();
        }
    }
}