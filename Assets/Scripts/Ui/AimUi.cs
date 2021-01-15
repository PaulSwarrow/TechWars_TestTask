using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class AimUi : MonoBehaviour
    {
        private Camera camera;
        private RectTransform rectTransform;
        private Image image;

        private void Awake()
        {
            camera = Camera.main;
            rectTransform = (RectTransform) transform;
            image = GetComponent<Image>();
        }

        private void Update()
        {
            var position = camera.WorldToScreenPoint(GameManager.PlayerController.Target.AimPoint);
            rectTransform.anchoredPosition = position;
            image.enabled = GameManager.PlayerController.Target.Aiming;
        }
    }
}