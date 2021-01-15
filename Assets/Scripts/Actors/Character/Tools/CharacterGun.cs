using UnityEngine;

namespace Actors.Character.Tools
{
    public class CharacterGun : MonoBehaviour
    {
        public bool Fire;
        public Vector3 AimPoint;

        [SerializeField] private float cooldownDuration = 0.5f;
        [SerializeField] private Transform projectileSpawner;


        private float cooldown = 0;


        private void Update()
        {
            if (Fire)
            {
                if (cooldown <= 0)
                {
                    var position = projectileSpawner.position;
                    GameManager.Projectiles.Spawn(position, AimPoint - position);
                    cooldown = cooldownDuration;
                }
                else
                {
                    cooldown -= Time.deltaTime;
                }
            }
            else
            {
                cooldown = 0;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(AimPoint, 0.3f);
        }
    }
}