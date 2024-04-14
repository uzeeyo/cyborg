using Cyborg.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cyborg.UI
{
    public class TooltipManager : MonoBehaviour
    {
        [SerializeField] private GameObject _tooltip;
        [SerializeField] private TextMeshProUGUI _nameLabel;
        [SerializeField] private TextMeshProUGUI _descriptionLabel;
        [SerializeField] private Image _icon;

        private static TooltipManager _instance;

        public static TooltipManager Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Init(Item item)
        {
            var data = item.Data;
            _nameLabel.text = data.ItemName;
            _descriptionLabel.text = data.Description;
            _icon.sprite = data.Sprite;

            var tooltipRectTransform = _tooltip.GetComponent<RectTransform>();
            tooltipRectTransform.SetParent(item.transform);
            tooltipRectTransform.anchoredPosition = new Vector2(50, 20);
            tooltipRectTransform.SetParent(transform, true);
            _tooltip.SetActive(true);
        }

        public void Hide()
        {
            _tooltip.SetActive(false);
        }
    }
}