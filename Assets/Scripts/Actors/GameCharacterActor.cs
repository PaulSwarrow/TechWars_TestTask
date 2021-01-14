using System;
using DefaultNamespace.Data;
using DefaultNamespace.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class GameCharacterActor : MonoBehaviour, IPoolable
    {
        public event Action ActivatedEvent;
        public event Action DeactivatedEvent;
        
        private static readonly int MoveKey = Animator.StringToHash("Move");

        [SerializeField] private Animator animator;
        [SerializeField] private float MoveSpeed = 3.5f;
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
            ActivatedEvent?.Invoke();
        }
        
        public void Reset()
        {
            character = null;
            DeactivatedEvent?.Invoke();
        }

        private void Update()
        {
            var movement = character.Move * MoveSpeed;
            animator.SetFloat(MoveKey, movement.magnitude);
            agent.Move(movement * Time.deltaTime);
            if (character.Direction.magnitude > 0)
            {
                transform.rotation = Quaternion.LookRotation(character.Direction, Vector3.up);
            }
        }
    }
}