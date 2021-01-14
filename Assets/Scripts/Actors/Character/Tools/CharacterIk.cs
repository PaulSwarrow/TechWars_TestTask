using System;
using UnityEngine;

namespace DefaultNamespace.Tools
{
    public class CharacterIk : MonoBehaviour
    {
        [NonSerialized] public bool Aiming;
        [NonSerialized] public Vector3 AimPoint;

        [SerializeField] private float correctionY;
        [SerializeField] private float correctionX;
        
        private Animator _animator;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
        }


        private void OnAnimatorIK(int layerIndex)
        {
            if (!Aiming) return;
            var delta = AimPoint - _transform.position;
            if (delta.magnitude < 1.5f) delta = delta.normalized * 1.5f;
            delta = _transform.InverseTransformDirection(delta);
            delta = Quaternion.Euler(0, correctionY, 0) * delta;
            delta = Quaternion.Euler(correctionX,0, 0) * delta;
            delta = _transform.TransformDirection(delta);
            _animator.SetLookAtPosition(_transform.position + delta);
            _animator.SetLookAtWeight(1, 1);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(AimPoint, 1);
        }
    }
}