using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private CursorData GameplayCursor;
    [SerializeField] private CursorData UICursor;
    [SerializeField] private CursorData MenuCursor;
    public Texture2D cursorTexture; 
    public Vector2 hotSpot = Vector2.zero;

    private void Awake()
    {
        EventHub.E_InventoryOpened += UIMod;
        EventHub.E_InventoryClosed += GameplayMod;
        EventHub.E_ModGamePlay += GameplayMod;
        EventHub.E_ModMenu += MenuMod;
    }
    private void OnDestroy()
    {
        EventHub.E_InventoryOpened -= UIMod;
        EventHub.E_InventoryClosed -= GameplayMod;
        EventHub.E_ModGamePlay -= GameplayMod;
        EventHub.E_ModMenu -= MenuMod;
    }
    private void Start()
    {
        GameplayMod();
    }

    private void MenuMod()
    {
        ChangeCursor(MenuCursor);
    }
    private void GameplayMod()
    {
        ChangeCursor(GameplayCursor);
    }
    private void UIMod()
    {
        ChangeCursor(UICursor);
    }

    private void ChangeCursor(CursorData cursorData)
    {
        Cursor.SetCursor(cursorData.cursorTexture, cursorData.hotSpot, CursorMode.Auto);
    }
}
