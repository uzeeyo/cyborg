using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterObject : MonoBehaviour
{
    [SerializeField] Transform obj1;
    [SerializeField] Transform obj2;
    [Range(0f, 1f)]
    [SerializeField] float Ratio;
    private void Awake()
    {
        GoCenter();
    }


    void Update()
    {
        GoCenter();
    }

    private void GoCenter()
    {
        transform.position = Vector3.Lerp(obj1.position, obj2.position, Ratio);
    }
}
