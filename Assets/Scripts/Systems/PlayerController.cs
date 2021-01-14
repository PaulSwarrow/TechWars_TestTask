using DefaultNamespace.Data;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class PlayerController : IGameSystem
    {
        private GameCharacter character;
        private Camera camera;
        public GameCharacter Target => character;

        public void Init()
        {
            camera = Camera.main;
        }

        public void Start()
        {
            character = GameManager.GameCharacter.Spawn(Vector3.zero, Vector3.forward);
            GameManager.UpdateEvent += OnUpdate;
        }

        public void Destroy()
        {
            GameManager.UpdateEvent -= OnUpdate;
        }

        private void OnUpdate()
        {
            var moveVector = new Vector3(
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical"));
            moveVector = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * moveVector;
            character.Move = moveVector;

            if (moveVector.magnitude > 0)
            {
                character.Direction = moveVector;
            }
        }
    }
}