using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Systems
{
    public class RespawnSystem : IGameSystem
    {
        private class RespawnTask
        {
            public float DeathTimestamp;
            public GameCharacter Character;
            public float Duration;
        }

        private List<RespawnTask> tasks = new List<RespawnTask>();
        private Dictionary<GameCharacter, RespawnTask> map = new Dictionary<GameCharacter, RespawnTask>();

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
                if (Time.time - task.DeathTimestamp > task.Duration)
                {
                    task.Character.Dead = false;
                    task.Character.Health = task.Character.MaxHealth;
                    tasks.RemoveAt(i);
                    map.Remove(task.Character);
                }
            }
        }

        private void OnCharacterDead(GameCharacter character)
        {
            var task = new RespawnTask
            {
                DeathTimestamp = Time.time,
                Character = character,
                Duration = GameManager.Properties.respawnTime
            };
            tasks.Add(task);
            map.Add(character, task);
        }

        public void Destroy()
        {
            GameManager.GameCharacter.DeathEvent -= OnCharacterDead;
            GameManager.UpdateEvent -= OnUpdate;
        }

        public float GetTimeLeft(GameCharacter character)
        {
            if (map.TryGetValue(character, out var task)) return task.Duration - (Time.time - task.DeathTimestamp);
            return 0;
        }
    }
}