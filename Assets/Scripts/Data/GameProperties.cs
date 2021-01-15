﻿using System;
using DefaultNamespace.Actors;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace.Data
{
    [Serializable]
    public class GameProperties
    {
        public GameCharacterActor characterActorPrefab;
        public ProjectileActor projectilePrefab;
        public float projectileVelocity;
        public float projectileLifetime = 1;
        public float characterHealth = 100;
        public float damage = 10;
        public int NpcCount = 5;
        public float respawnTime = 5;
        public GameVFXActor BulletImpactPrefab;
    }
}