using System;
using System.Collections.Generic;
using Actors.Character;
using Model;
using UnityEngine;

namespace Systems
{
    public class GameCharacterSystem : IGameSystem
    {
        public event Action<GameCharacter> DeathEvent;

        private GameCharacterActor prefab;
        private List<GameCharacter> list = new List<GameCharacter>();

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
            character.Health = character.MaxHealth = GameManager.Properties.characterHealth;
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

        public void Foreach(Action<GameCharacter> handler)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                handler(list[i]);
            }
        }
    }
}