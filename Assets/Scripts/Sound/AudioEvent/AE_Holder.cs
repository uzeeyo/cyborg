using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Event/AE_Holder")]
public class AE_Holder : ScriptableObject
{
    public List<AudioEvent> events;
}
