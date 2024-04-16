using System.Collections;
using UnityEngine;

namespace Cyborg.Items
{
    public class HitEffect : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(WaitForDestroy());
        }

        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}