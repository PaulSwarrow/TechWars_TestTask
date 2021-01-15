using System.Collections.Generic;
using DefaultNamespace.Actors;
using DefaultNamespace.Interfaces;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class ProjectileSystem : IGameSystem
    {
        private List<ProjectileActor> projectiles = new List<ProjectileActor>();

        public void Init()
        {
        }

        public void Start()
        {
            GameManager.UpdateEvent += OnUpdate;
        }

        public void Spawn(Vector3 position, Vector3 direction)
        {
            var projectile = GameManager.ObjectSpawner.Spawn(GameManager.Properties.projectilePrefab, position,
                Quaternion.LookRotation(direction, Vector3.up));
            projectile.timestamp = Time.time;
            projectiles.Add(projectile);
        }

        private void OnUpdate()
        {
            for (var i = projectiles.Count - 1; i >= 0; i--)
            {
                var projectile = projectiles[i];

                var step = projectile.Direction * (GameManager.Properties.projectileVelocity * Time.deltaTime);
                if (CheckHit(projectile, step) || Time.time - projectile.timestamp > GameManager.Properties.projectileLifetime)
                {
                    GameManager.ObjectSpawner.Destroy(projectile);
                    projectiles.RemoveAt(i);
                }
                else
                {
                    projectile.Position += step;
                }
            }
        }

        private bool CheckHit(ProjectileActor projectile, Vector3 movement)
        {
            if (GameManager.Colliders.Raycast(projectile.Position, movement, out var hit))
            {
                var target = hit.collider.GetComponent<IDamagable>();
                target?.TakeDamage();
                return true;
            }

            return false;
        }

        public void Destroy()
        {
            GameManager.UpdateEvent -= OnUpdate;
        }
    }
}