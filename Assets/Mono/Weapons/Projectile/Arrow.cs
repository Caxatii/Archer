using System;
using Cysharp.Threading.Tasks;
using Spine.Unity;
using UnityEngine;

namespace Mono.Weapons.Projectile
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(SkeletonAnimation))]
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private AnimationReferenceAsset _blast;
        [SerializeField] private AnimationReferenceAsset _attack;

        private Transform _transform;
        private SkeletonAnimation _skeletonAnimation;
        
        public event Action<Arrow> Blasted;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            _transform.right = _rigidbody.velocity;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            PlayAnimation().Forget();
            _transform.position = other.contacts[0].point;
        }

        public float GetGravityScale()
        {
            return _rigidbody.gravityScale;
        }
        
        public void Throw(float force)
        {
            _rigidbody.velocity = _transform.right * force;
        }
        
        public void Reset()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _attack.Animation, false);
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        private async UniTask PlayAnimation()
        {
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _rigidbody.velocity = Vector2.zero;
            await _skeletonAnimation.AnimationState.SetAnimation(0, _blast.Animation, false).Wait();
            
            Blasted?.Invoke(this);
        }
    }
}
