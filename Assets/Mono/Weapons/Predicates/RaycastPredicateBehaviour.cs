using System.Collections.Generic;
using UnityEngine;

namespace Mono.Weapons.Predicates
{
    public class RaycastPredicateBehaviour : PredicateBehaviour
    {
        public RaycastPredicateBehaviour(List<GameObject> objects) : base(objects) { }
        
        public override void Predict(Vector2 startPoint, Vector2 direction, float gravityScale)
        {
            Show();
            
            for (int i = 0; i < Objects.Count; i++)
            {
                if(RaycastPredict(startPoint, direction, i, gravityScale))
                    return;
            }
        }
        
        private bool RaycastPredict(Vector2 startPoint, Vector2 direction, int index, float gravityScale)
        {
            if (index == 0)
                return HandleFirstIndex(startPoint, direction, index, gravityScale);

            return HandleRaycastPrediction(startPoint, direction, index, gravityScale);
        }

        private bool HandleFirstIndex(Vector2 startPoint, Vector2 direction, int index, float gravityScale)
        {
            SimplePredict(startPoint, direction, index, gravityScale);
            return false;
        }

        private bool HandleRaycastPrediction(Vector2 startPoint, Vector2 direction, int index, float gravityScale)
        {
            Vector2 obstacleCheckPoint = Objects[index - 1].transform.position;
            Vector2 rayDirection = CalculatePhysicDirection(startPoint, direction, index, gravityScale);

            RaycastHit2D obstacle = HasObstacle(obstacleCheckPoint, rayDirection);

            if (obstacle.collider != null)
            {
                Objects[index].transform.position = obstacle.point;
                HideAfter(index);
                return true;
            }

            Objects[index].transform.position = rayDirection;
            return false;
        }
        
        private void SimplePredict(Vector2 startPoint, Vector2 direction, int factor, float gravityScale)
        {
            Objects[factor].transform.position = CalculatePhysicDirection(startPoint, direction, factor, gravityScale);
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