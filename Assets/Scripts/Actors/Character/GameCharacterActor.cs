using Actors.Character.Tools;
using DefaultNamespace.Data;
using DefaultNamespace.Interfaces;
using DefaultNamespace.Tools;
using Tools;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class GameCharacterActor : MonoBehaviour, IPoolable, IDamagable
    {
        private static readonly int ForwardKey = Animator.StringToHash("forward");
        private static readonly int StrafeKey = Animator.StringToHash("strafe");
        private static readonly int DeadKey = Animator.StringToHash("dead");

        [SerializeField] private float directionAlignVelocity = 6;
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterIk ik;
        [SerializeField] private CharacterGun gun;
        [SerializeField] private VirtualCollider collider;
        private NavMeshAgent agent;

        public GameCharacter Character { get; private set; }

        public Transform transform { get; private set; }

        private void Awake()
        {
            transform = base.transform;
            agent = GetComponent<NavMeshAgent>();
        }

        public void Activate(GameCharacter character)
        {
            this.Character = character;
            ik.Character = character;
        }

        public void Reset()
        {
            Character = null;
        }

        private void Update()
        {
            animator.SetBool(DeadKey, Character.Dead);
            animator.SetLayerWeight(1, Character.Dead ? 0 : 1);
            collider.enabled = !Character.Dead;
            agent.enabled = !Character.Dead;
            if (Character.Dead)
            {
                gun.Fire = false;
                return;
            }

            var lookAtTarget = Vector3.Angle(transform.forward, Character.AimPoint - transform.position) < 30;
            gun.Fire = Character.Fire && lookAtTarget;
            gun.AimPoint = Character.AimPoint;
            var movement = Character.Move * agent.speed;
            var localMovement = transform.InverseTransformDirection(Character.Move);
            //animate
            animator.SetFloat(ForwardKey, localMovement.z);
            animator.SetFloat(StrafeKey, localMovement.x);

            agent.Move(movement * Time.deltaTime);
            var direction = Character.Direction;
            direction.y = 0;
            if (direction.magnitude > 0)
            {
                var angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
                transform.rotation *= Quaternion.Euler(0, directionAlignVelocity * angle * Time.deltaTime, 0);
            }
        }

        public void TakeDamage()
        {
            Character.TakeDamage();
        }
    }
}