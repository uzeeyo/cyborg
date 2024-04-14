using System.Collections;
using UnityEngine;

namespace Cyborg.Items
{
    public class Projectile : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(DestroyAfterTime());
        }

        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}