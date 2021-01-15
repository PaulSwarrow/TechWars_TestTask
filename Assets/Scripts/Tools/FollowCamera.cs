using DefaultNamespace.Data;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameCharacter target;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float moveSpeed = 2;

    private void Start()
    {
        target = GameManager.PlayerController.Target;
    }

    private void LateUpdate()
    {
        var cameraPosition = transform.position;
        var position = target.Position;
        var targetPosition = position + offset;

        transform.position = Vector3.Slerp(cameraPosition, targetPosition, moveSpeed * Time.fixedDeltaTime);
    }
}