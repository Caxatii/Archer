using Spine.Unity.Examples;
using UnityEngine;

namespace Mono
{
    public class BoneEditor : MonoBehaviour
    {
        [SerializeField] private int _minAngle;
        [SerializeField] private int _maxAngle;

        [SerializeField] private BoneLocalOverride _bone;

        public void SetRotation(float percent)
        {
            percent = Mathf.Clamp01(percent);
            _bone.rotation = Mathf.Lerp(_minAngle, _maxAngle, percent);
        }
    }
}
