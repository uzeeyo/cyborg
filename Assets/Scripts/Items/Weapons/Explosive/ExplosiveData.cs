using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapon/Explosive/ExplosiveData")]
public class ExplosiveData : ScriptableObject
{
    [field: SerializeField] public float Radius { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public bool IsExplodeWithCollision { get; private set; }
    [field: SerializeField] public bool IsExplodeWithTime { get; private set; }
    [field: SerializeField] public float ExplodeTime { get; private set; }


}
