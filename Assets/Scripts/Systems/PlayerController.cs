using DefaultNamespace.Data;
using Tools;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class PlayerController : IGameSystem
    {
        private GameCharacter character;
        private Camera camera;
        private int floorMask;
        public GameCharacter Target => character;

        public void Init()
        {
            floorMask = LayerMask.GetMask("Floor");
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
            character.Aiming = Input.GetButton("Fire2");
            var moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveVector = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * moveVector;
            character.Move = moveVector;

            if (character.Aiming)
            {
                character.Fire = Input.GetButton("Fire1");
                if (InputTools.MouseToFloorPoint(camera, 40, floorMask, out var point))
                {
                    point += Vector3.up; //do not shoot the floor)
                    character.AimPoint = point;
                }

                var direction = character.AimPoint - character.Position;
                direction.y = 0;
                character.Direction = direction;
            }
            else if (moveVector.magnitude > 0)
            {
                character.Fire = false;
                character.Direction = moveVector;
            }
        }
    }
}