using System;
using UnityEngine;

namespace DefaultNamespace.Data
{
    public class GameCharacter
    {
        public event Action<GameCharacter> DeathEvent;
        public event Action<GameCharacter> DestroyEvent;
        private GameCharacterActor actor;

        public GameCharacter(GameCharacterActor prefab)
        {
            actor = GameManager.ObjectSpawner.Spawn(prefab, Vector3.zero, Quaternion.identity);
            actor.Activate(this);
        }

        public Vector3 Position => actor.transform.position;
        public bool Fire;
        public bool Aiming;
        public Vector3 AimPoint;
        public Vector3 Move;
        public Vector3 Direction;
        public float Health;
        public bool Dead;
        public float MaxHealth;


        public void Destroy()
        {
            GameManager.ObjectSpawner.Destroy(actor);
            actor = null;
            DestroyEvent?.Invoke(this);
        }

        public void SetPosition(Vector3 position, Vector3 lookDirection)
        {
            actor.transform.position = position;
            actor.transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }

        public void TakeDamage()
        {
            if (Dead) return;
            Health -= GameManager.Properties.damage;
            if (Health <= 0)
            {
                DeathEvent?.Invoke(this);
                Health = 0;
                Dead = true;
            }
        }
    }
}