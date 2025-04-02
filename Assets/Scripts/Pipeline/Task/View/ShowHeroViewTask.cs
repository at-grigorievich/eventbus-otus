using DefaultNamespace.Hero;
using UI;
using UnityEngine;
using VContainer;

namespace DefaultNamespace
{
    public class ShowHeroViewTask: EventTask
    {
        private readonly IObjectResolver _resolver;
        private readonly Entity _heroEntity;
        
        public ShowHeroViewTask(Entity heroEntity, IObjectResolver resolver)
        {
            _heroEntity = heroEntity;
            _resolver = resolver;
        }
        
        protected override void OnStart()
        {
            UIService uiService = _resolver.Resolve<UIService>();
            HeroTeamsService heroTeamsService = _resolver.Resolve<HeroTeamsService>();

            if (_heroEntity.TryGetComponent(out Team teamType) == false)
            {
                Complete();
                return;
            }

            HeroTeam team = heroTeamsService[(TeamType)teamType.Value];

            if (team.TryGetHeroView(uiService, _heroEntity, out HeroView view) == false)
            {
                Complete();
                return;
            }
            //ShowName();
            //ShowDescription();
            
            ShowIcon(view);
            ShowCurrentStats(view);
            
            Complete();
        }

        private void ShowName(HeroView view)
        {
            string name = _heroEntity.TryGetComponent(out Name res) ? res.Value : "no_name";
            view.name = name;
        }

        private void ShowDescription(HeroView view)
        {
            string desc = _heroEntity.TryGetComponent(out Description res) ? res.Value : "no_description";
            //...
        }

        private void ShowIcon(HeroView view)
        {
            Sprite icon = _heroEntity.TryGetComponent(out Icon res) ? res.Value : null;
            view.SetIcon(icon);
        }

        private void ShowCurrentStats(HeroView view)
        {
            int health = _heroEntity.TryGetComponent(out Health hp) ? hp.Value : 0;
            int damage = _heroEntity.TryGetComponent(out Damage dmg) ? dmg.Value : 0;

            string statOutput = $"{damage}/{health}";
            
            view.SetStats(statOutput);
        }
    }
}