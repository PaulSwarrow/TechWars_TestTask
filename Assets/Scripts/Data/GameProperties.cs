using System;
using Actors;
using Actors.Character;

namespace Data
{
    [Serializable]
    public class GameProperties
    {
        public GameCharacterActor characterActorPrefab;
        public ProjectileActor projectilePrefab;
        public GameVFXActor BulletImpactPrefab;
        public float projectileVelocity;
        public float projectileLifetime = 1;
        public float characterHealth = 100;
        public float damage = 10;
        public float respawnTime = 5;
        public int npcCount = 5;
        public float npcTravelDistance = 15;
        public float npcAttackDistance = 4;
        public float npcMovementAccurancy = .4f;
    }
}