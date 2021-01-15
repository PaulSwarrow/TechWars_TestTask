using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.AI;

namespace Systems
{
    public class NpcSystem : IGameSystem
    {
        private class NPC
        {
            public Vector3 targetPosition;
            public GameCharacter character;
            public NavMeshPath path = new NavMeshPath();
            public int pathProgress;
        }

        private List<NPC> npcs = new List<NPC>();

        public void Init()
        {
        }

        public void Start()
        {
            GameManager.UpdateEvent += OnUpdate;
            for (var i = 0; i < GameManager.Properties.npcCount; i++)
            {
                npcs.Add(new NPC
                {
                    character = GameManager.GameCharacter.Spawn(GetRandomPosition(), GetRandomDirection()),
                    targetPosition = GetRandomPosition()
                });
            }
        }

        private void OnUpdate()
        {
            foreach (var npc in npcs)
            {
                var movementVector = npc.character.Direction;
                if (npc.pathProgress >= npc.path.corners.Length)
                {
                    npc.targetPosition = GetRandomPosition();
                    NavMesh.CalculatePath(npc.character.Position, npc.targetPosition, NavMesh.AllAreas, npc.path);
                    npc.pathProgress = 0;
                }

                if (npc.pathProgress < npc.path.corners.Length)
                {
                    var point = npc.path.corners[npc.pathProgress];
                    if (Vector3.Distance(point, npc.character.Position) < GameManager.Properties.npcMovementAccurancy)
                    {
                        npc.pathProgress++;
                    }
                    else
                    {
                        movementVector = point - npc.character.Position;
                        npc.character.Move = movementVector.normalized;
                    }
                }
                else
                {
                    npc.character.Move = Vector3.zero;
                }


                var target = GameManager.PlayerController.Target;
                var targetVector = target.Position - npc.character.Position;
                var distance = targetVector.magnitude;
                if (!target.Dead && distance < GameManager.Properties.npcAttackDistance)
                {
                    npc.character.Direction = targetVector;
                    npc.character.Fire = npc.character.Aiming = true;
                    npc.character.AimPoint = target.Position + Vector3.up;
                }
                else
                {
                    npc.character.Fire = npc.character.Aiming = false;
                    npc.character.Direction = movementVector;
                }
            }
        }

        private Vector3 GetRandomDirection()
        {
            return Quaternion.Euler(0, Random.Range(0, 360f), 0) * Vector3.forward;
        }

        private Vector3 GetRandomPosition()
        {
            var offset = Quaternion.Euler(0, Random.Range(0, 360f), 0) * Vector3.forward * GameManager.Properties.npcTravelDistance;
            if (NavMesh.SamplePosition(offset, out var hit, GameManager.Properties.npcTravelDistance, NavMesh.AllAreas))
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