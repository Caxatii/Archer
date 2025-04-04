using Spine.Unity;
using UnityEngine;

namespace Mono.Archer
{
    [RequireComponent(typeof(SkeletonAnimation))]
    public class ArcherAnimator : MonoBehaviour
    {
        [SerializeField] private AnimationReferenceAsset _start;
        [SerializeField] private AnimationReferenceAsset _throw;
        
        private SkeletonAnimation _skeletonAnimation;
        
        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        private void Start()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _start.Animation, false);
        }

        public void PlayThrow()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _throw.Animation, false);
            _skeletonAnimation.AnimationState.AddAnimation(0, _start.Animation, false, 0);
        }
    }
}
