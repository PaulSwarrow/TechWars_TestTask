using Actors.Character.Tools;
using DefaultNamespace.Data;
using DefaultNamespace.Interfaces;
using DefaultNamespace.Tools;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class GameCharacterActor : MonoBehaviour, IPoolable, IDamagable
    {
        private static readonly int ForwardKey = Animator.StringToHash("forward");
        private static readonly int StrafeKey = Animator.StringToHash("strafe");

        [SerializeField] private float directionAlignVelocity = 6;
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterIk ik;
        [SerializeField] private CharacterGun gun;
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
            ik.Aiming = character.Aiming;
            ik.AimPoint = character.AimPoint;

            agent.Move(movement * Time.deltaTime);
            if (character.Direction.magnitude > 0)
            {
                var angle = Vector3.SignedAngle(transform.forward, character.Direction, Vector3.up);
                transform.rotation *= Quaternion.Euler(0, directionAlignVelocity * angle * Time.deltaTime, 0);
            }

            gun.Fire = character.Fire;
            gun.AimPoint = character.AimPoint;
        }

        public void TakeDamage()
        {
        }
    }
}