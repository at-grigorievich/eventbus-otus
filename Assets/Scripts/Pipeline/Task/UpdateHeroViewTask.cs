using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class UpdateHeroViewTask: EventTask
    {
        private readonly Entity _heroEntity;
        private readonly HeroView _view;

        public UpdateHeroViewTask(Entity heroEntity, HeroView view)
        {
            _heroEntity = heroEntity;
            _view = view;
        }
        
        protected override void OnStart()
        {
            //ShowName();
            //ShowDescription();
            ShowIcon();
            ShowCurrentStats();
            
            Complete();
        }

        private void ShowName()
        {
            string name = _heroEntity.TryGetComponent(out Name res) ? res.Value : "no_name";
            _view.name = name;
        }

        private void ShowDescription()
        {
            string desc = _heroEntity.TryGetComponent(out Description res) ? res.Value : "no_description";
            //...
        }

        private void ShowIcon()
        {
            Sprite icon = _heroEntity.TryGetComponent(out Icon res) ? res.Value : null;
            _view.SetIcon(icon);
        }

        private void ShowCurrentStats()
        {
            int health = _heroEntity.TryGetComponent(out Health hp) ? hp.Value : 0;
            int damage = _heroEntity.TryGetComponent(out Damage dmg) ? dmg.Value : 0;

            string statOutput = $"{damage}/{health}";
            
            _view.SetStats(statOutput);
        }
    }
}