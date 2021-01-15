using System.Collections.Generic;
using DefaultNamespace.Data;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class RespawnSystem : IGameSystem
    {
        private class RespawnTask
        {
            public float DeathTimestamp;
            public GameCharacter Character;
        }

        private List<RespawnTask> tasks = new List<RespawnTask>();

        public void Init()
        {
        }

        public void Start()
        {
            GameManager.GameCharacter.DeathEvent += OnCharacterDead;
            GameManager.UpdateEvent += OnUpdate;
        }

        private void OnUpdate()
        {
            for (var i = tasks.Count - 1; i >= 0; i--)
            {
                var task = tasks[i];
                if (Time.time - task.DeathTimestamp > GameManager.Properties.respawnTime)
                {
                    task.Character.Dead = false;
                    task.Character.Health = GameManager.Properties.characterHealth;
                    tasks.RemoveAt(i);
                }
            }
        }

        private void OnCharacterDead(GameCharacter character)
        {
            tasks.Add(new RespawnTask
            {
                DeathTimestamp = Time.time,
                Character = character
            });
        }

        public void Destroy()
        {
            GameManager.GameCharacter.DeathEvent -= OnCharacterDead;
            GameManager.UpdateEvent -= OnUpdate;
        }
    }
}