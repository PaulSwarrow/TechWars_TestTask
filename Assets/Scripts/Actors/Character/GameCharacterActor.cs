using System;
using DefaultNamespace.Data;
using DefaultNamespace.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class GameCharacterActor : MonoBehaviour, IPoolable
    {
        private static readonly int ForwardKey = Animator.StringToHash("forward");
        private static readonly int StrafeKey = Animator.StringToHash("strafe");

        [SerializeField] private float directionAlignVelocity = 6;
        [SerializeField] private Animator animator;
        private NavMeshAgent agent;

        public GameCharacter character { get; private set; }

        public Transform transform { get; private set; }

        private void Awake()
        {
            transform = base.transform;
            agent = GetComponent<NavMeshAgent>();
        }

        public void Activate(GameCharacter character)
        {
            this.character = character;
        }

        public void Reset()
        {
            character = null;
        }

        private void Update()
        {
            var movement = character.Move * agent.speed;
            var localMovement = transform.InverseTransformDirection(character.Move);
            //animate
            animator.SetFloat(ForwardKey, localMovement.z);
            animator.SetFloat(StrafeKey, localMovement.x);
            //move
            agent.Move(movement * Time.deltaTime);
            if (character.Direction.magnitude > 0)
            {
                var angle = Vector3.SignedAngle(transform.forward, character.Direction, Vector3.up);
                transform.rotation *= Quaternion.Euler(0, directionAlignVelocity * angle * Time.deltaTime, 0);
            }
        }
    }
}