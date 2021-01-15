using System.Collections.Generic;
using DefaultNamespace.Data;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.Systems
{
    public class NpcSystem : IGameSystem
    {
        private class NPC
        {
            public Vector3 targetPosition;
            public GameCharacter character;
        }

        private List<NPC> npcs = new List<NPC>();

        public void Init()
        {
        }

        public void Start()
        {
            GameManager.UpdateEvent += OnUpdate;
            for (var i = 0; i < GameManager.Properties.NpcCount; i++)
            {
                npcs.Add(new NPC
                {
                    character = GameManager.GameCharacter.Spawn(GetRandomPosition(), GetRandomDirection())
                });
            }
        }

        private void OnUpdate()
        {
            foreach (var npc in npcs)
            {
                var movementVector = npc.targetPosition - npc.character.Position;
                var targetVector = GameManager.PlayerController.Target.Position - npc.character.Position;
                var distance = targetVector.magnitude;
                if (distance < 5)
                {
                    npc.character.Direction = targetVector;
                    // npc.character.Fire = npc.character.Aiming = true;
                }
                else
                {
                    npc.character.Direction = movementVector;
                }

                if (Vector3.Distance(npc.targetPosition, npc.character.Position) < 1)
                {
                    npc.targetPosition = GetRandomPosition();
                }
                else
                {
                    npc.character.Move = movementVector.normalized;
                }
            }
        }

        private Vector3 GetRandomDirection()
        {
            return Quaternion.Euler(0, Random.Range(0, 360f), 0) * Vector3.forward;
        }

        private Vector3 GetRandomPosition()
        {
            var offset = Quaternion.Euler(0, Random.Range(0, 360f), 0) * Vector3.forward * 8;
            if (NavMesh.SamplePosition(offset, out var hit, 10, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return Vector3.zero;
        }

        public void Destroy()
        {
            GameManager.UpdateEvent -= OnUpdate;
        }
    }
}