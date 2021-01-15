using DefaultNamespace.Data;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Ui
{
    public class CharacterStatusUi : MonoBehaviour
    {
        public GameCharacter character;
        public Color color;

        [SerializeField] private Slider healthBar;
        [SerializeField] private Text statusField;
        [SerializeField] private Image healthColor;
        private RectTransform rectTransform;
        private Camera camera;


        private void Awake()
        {
            rectTransform = (RectTransform) transform;
            camera = Camera.main;
        }

        private void Start()
        {
            healthColor.color = new Color(color.r, color.g, color.b, .5f);
            statusField.color = color;
        }

        private void Update()
        {
            healthBar.gameObject.SetActive(!character.Dead);
            statusField.gameObject.SetActive(character.Dead);
            if (character.Dead)
            {
                statusField.text = $"Respawn: {GameManager.RespawnSystem.GetTimeLeft(character):0.00}s";
            }
            else
            {
                healthBar.maxValue = character.MaxHealth;
                healthBar.value = character.Health;
            }

            var position = camera.WorldToScreenPoint(character.Position);
            rectTransform.anchoredPosition = position;
        }
    }
}