using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CursorData")]
public class CursorData : ScriptableObject
{
    [field:SerializeField] public Texture2D cursorTexture {  get; private set; }
    [field: SerializeField] public Vector2 hotSpot { get; private set; }
   
}
