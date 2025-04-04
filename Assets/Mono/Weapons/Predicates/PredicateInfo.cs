using UnityEngine;

namespace Mono.Weapons.Predicates
{
    public class PredicateInfo
    {
        public PredicateInfo(Vector2 direction, int index, Vector2 startPoint, float density, float gravityScale)
        {
            Direction = direction;
            Index = index;
            StartPoint = startPoint;
            Density = density;
            GravityScale = gravityScale;
        }

        public int Index { get; private set; }

        public float Density { get; }
        public float GravityScale { get; }
        
        public Vector2 StartPoint { get; }
        public Vector2 Direction { get;}

        public void Next() =>
            Index++;
        
        public void Reset() =>
            Index = 0;
    }
}