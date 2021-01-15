using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class VfxSystem : IGameSystem
    {
        public void Init()
        {
            
        }

        public void Start()
        {
        }

        public void BulletImpact(Vector3 point, Vector3 bulletVector)
        {
            GameManager.ObjectSpawner.Spawn(GameManager.Properties.BulletImpactPrefab, point,
                Quaternion.LookRotation(-bulletVector, Vector3.up));
            
            //TODO vfx pool
        }
        public void Destroy()
        {
            
        }
    }
}