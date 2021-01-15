using System.Collections.Generic;
using DefaultNamespace.Data;
using Tools;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class GameCharacterSystem : IGameSystem
    {
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
            character.DestroyEvent += OnCharacterDestroyed;
            list.Add(character);
            return character;
        }

        private void OnCharacterDestroyed(GameCharacter character)
        {
            list.Remove(character);
        }
    }
}