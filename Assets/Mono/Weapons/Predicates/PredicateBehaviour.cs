using System.Collections.Generic;
using UnityEngine;

namespace Mono.Weapons.Predicates
{
    public abstract class PredicateBehaviour
    {
        protected readonly List<GameObject> Objects;
        private readonly float _divider = 5;

        protected PredicateBehaviour(List<GameObject> objects)
        {
            Objects = objects;
        }

        public abstract void Predict(Vector2 startPoint, Vector2 direction);
        
        public void Hide()
        {
            foreach (GameObject projectile in Objects) 
                projectile.SetActive(false);
        }
        
        protected Vector2 CalculatePhysicDirection(Vector2 startPoint, Vector2 direction, int factor)
        {
            direction /= _divider;
            return startPoint + (direction + Physics2D.gravity * (Time.fixedDeltaTime * factor)) * factor;
        }
        
        protected void Show()
        {
            foreach (GameObject projectile in Objects) 
                projectile.SetActive(true);
        }
    }
}