using System.Collections.Generic;
using UnityEngine;

namespace Mono.Weapons.Predicates
{
    public class SimplePredicateBehaviour : PredicateBehaviour
    {
        public SimplePredicateBehaviour(List<GameObject> objects) : base(objects) { }

        public override void Predict(Vector2 startPoint, Vector2 direction, float gravityScale)
        {
            Show();
            
            for (var i = 0; i < Objects.Count; i++) 
                Objects[i].transform.position = CalculatePhysicDirection(startPoint, direction, i, gravityScale);
        }
    }
}