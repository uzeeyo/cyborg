using Cyborg.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Cyborg.Enemies
{
    public class BatteryDropper : MonoBehaviour
    {
        [SerializeField] private Battery _batterySPrefab;
        [SerializeField] private Battery _batteryMPrefab;
        [SerializeField] private Battery _batteryLPrefab;

        private Dictionary<Battery, float> _dropChance;

        private void Awake()
        {
            _dropChance = new()
            {
                { _batteryLPrefab, 0.05f },
                { _batteryMPrefab, 0.1f },
                { _batterySPrefab, 0.2f },
            };
        }

        public void DropBattery()
        {
            var rand = Random.Range(0f, 1f);
            float currentChance = 0f;

            foreach (var kvp in _dropChance)
            {
                currentChance += kvp.Value;
                if (rand < currentChance)
                {
                    Instantiate(kvp.Key, transform.position, Quaternion.identity);
                    break;
                }
            }
        }
    }
}