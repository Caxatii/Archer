using System.Collections.Generic;
using UnityEngine;

namespace Mono.Weapons.Predicates
{
    public class RaycastPredicateBehaviour : PredicateBehaviour
    {
        public RaycastPredicateBehaviour(List<GameObject> objects) : base(objects) { }
        
        public override void Predict(Vector2 startPoint, Vector2 direction, float density, float gravityScale)
        {
            Show();
            PredicateInfo info = new PredicateInfo(direction, 0, startPoint, density, gravityScale);

            foreach (GameObject gameObject in Objects)
            {
                if (RaycastPredict(info))
                    return;

                info.Next();
            }
        }
        
        private bool RaycastPredict(PredicateInfo info)
        {
            if (info.Index == 0)
                return HandleFirstIndex(info);

            return HandleRaycastPrediction(info);
        }

        private bool HandleFirstIndex(PredicateInfo info)
        {
            SimplePredict(info);
            return false;
        }

        private bool HandleRaycastPrediction(PredicateInfo info)
        {
            Vector2 obstacleCheckPoint = Objects[info.Index - 1].transform.position;
            
            Vector2 rayDirection = CalculatePhysicDirection(info.StartPoint,
                info.Direction,
                info.Index * info.Density,
                info.GravityScale);

            RaycastHit2D obstacle = HasObstacle(obstacleCheckPoint, rayDirection);

            if (obstacle.collider != null)
            {
                Objects[info.Index].transform.position = obstacle.point;
                HideAfter(info.Index);
                return true;
            }

            Objects[info.Index].transform.position = rayDirection;
            return false;
        }
        
        private void SimplePredict(PredicateInfo info)
        {
            Objects[info.Index].transform.position = CalculatePhysicDirection(info.StartPoint,
                info.Direction,
                info.Index * info.Density,
                info.GravityScale);
        }
        
        private void HideAfter(int index)
        {
            index++;
            for (var i = index; i < Objects.Count; i++) 
                Objects[i].SetActive(false);
        }
        
        private RaycastHit2D HasObstacle(Vector2 startPoint, Vector2 endPoint)
        {
            return Physics2D.Linecast(startPoint, endPoint);
        }
    }
}