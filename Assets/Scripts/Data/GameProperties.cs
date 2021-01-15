using System;
using DefaultNamespace.Actors;

namespace DefaultNamespace.Data
{
    [Serializable]
    public class GameProperties
    {
        public GameCharacterActor characterActorPrefab;
        public ProjectileActor projectilePrefab;
        public float projectileVelocity;
    }
}