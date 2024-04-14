
using Cyborg.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cyborg.Items
{
    public class TooltipHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Item _item;

        private void Awake()
        {
            _item = GetComponent<Item>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipManager.Instance.Init(_item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Instance.Hide();
        }
    }
}