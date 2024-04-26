using UnityEngine;

namespace Cyborg.Items
{
    public class Battery : MonoBehaviour
    {
        [SerializeField] private float _energyAmount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerMovement _))
            {
                EnergyManager.Instance.AddEnergy(_energyAmount);
                Destroy(gameObject);
            }
        }
    }
}