using UnityEngine;
public abstract class AudioEvent : ScriptableObject
{
    public abstract bool TryBegin();
    public abstract void Destroy();
}
