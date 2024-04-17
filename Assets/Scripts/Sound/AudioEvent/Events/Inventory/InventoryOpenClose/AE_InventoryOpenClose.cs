using UnityEngine;
using FMODUnity;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Event/Inventory/InventoryOpenClose")]
public class AE_InventoryOpen : AudioEvent
{
    [SerializeField] EventReference InventoryOpenEventReference;

    [SerializeField] EventReference InventoryClosedEventReference;

    public override bool TryBegin()
    {
        if (InventoryOpenEventReference.IsNull || InventoryClosedEventReference.IsNull)
            return false;

        EventHub.E_InventoryOpened += PlayOpen;
        EventHub.E_InventoryClosed += PlayClose;

        return true;
    }
    private void PlayOpen()
    {
        RuntimeManager.PlayOneShot(InventoryOpenEventReference);
    }

    private void PlayClose()
    {
        RuntimeManager.PlayOneShot(InventoryClosedEventReference);
    }
}
