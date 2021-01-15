using System;
using System.Collections.Generic;
using DefaultNamespace.Data;
using Tools;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class GameCharacterSystem : IGameSystem
    {
        public event Action<GameCharacter> DeathEvent;
        
        private GameCharacterActor prefab;
        private HashSet<GameCharacter> list = new HashSet<GameCharacter>();

        public void Init()
        {
            prefab = GameManager.Properties.characterActorPrefab;
        }

        public void Start()
        {
        }

        public void Destroy()
        {
        }

        public GameCharacter Spawn(Vector3 position, Vector3 lookDirection)
        {
            var character = new GameCharacter(prefab);
            character.SetPosition(position, lookDirection);
            character.Health = GameManager.Properties.characterHealth;
            character.DestroyEvent += OnCharacterDestroyed;
            list.Add(character);
            character.DeathEvent += OnCharacterDead;
            return character;
        }

        private void OnCharacterDead(GameCharacter character)
        {
            DeathEvent?.Invoke(character);

        }

        private void OnCharacterDestroyed(GameCharacter character)
        {
            character.DeathEvent -= OnCharacterDead;
            list.Remove(character);
        }
    }
}