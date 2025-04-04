using System.Collections.Generic;
using UnityEngine;

namespace Mono.Weapons.Predicates
{
    public class RaycastPredicateBehaviour : PredicateBehaviour
    {
        public RaycastPredicateBehaviour(List<GameObject> objects) : base(objects) { }
        
        public override void Predict(Vector2 startPoint, Vector2 direction)
        {
            Show();
            
            for (int i = 0; i < Objects.Count; i++)
            {
                if(RaycastPredict(startPoint, direction, i))
                    return;
            }
        }
        
        private bool RaycastPredict(Vector2 startPoint, Vector2 direction, int index)
        {
            if (index == 0)
                return HandleFirstIndex(startPoint, direction, index);

            return HandleRaycastPrediction(startPoint, direction, index);
        }

        private bool HandleFirstIndex(Vector2 startPoint, Vector2 direction, int index)
        {
            SimplePredict(startPoint, direction, index);
            return false;
        }

        private bool HandleRaycastPrediction(Vector2 startPoint, Vector2 direction, int index)
        {
            Vector2 obstacleCheckPoint = Objects[index - 1].transform.position;
            Vector2 rayDirection = CalculatePhysicDirection(startPoint, direction, index);

            RaycastHit2D obstacle = HasObstacle(obstacleCheckPoint, rayDirection);

            if (obstacle.collider != null)
            {
                Objects[index].transform.position = obstacle.point;
                HideAfter(index);
                return true;
            }
            
            SimplePredict(startPoint, direction, index);
            return false;
        }

        private void SimplePredict(Vector2 startPoint, Vector2 direction, int index)
        {
            Objects[index].transform.position = CalculatePhysicDirection(startPoint, direction, index);
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