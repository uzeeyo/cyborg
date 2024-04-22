using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float PlayerToMousePosRatio;

    [SerializeField] private float MoveSpeed;

    private Vector2 MousePosition;
    void Start()
    {
        InputGameplay.Instance.E_Rotate += SetMousePosition;
    }

    private void OnDestroy()
    {
        InputGameplay.Instance.E_Rotate -= SetMousePosition;
    }


    void Update()
    {
        SetFollowObjectPosition();
    }

    private void SetMousePosition(Vector2 mousePosition)
    {
        MousePosition = mousePosition;
    }
    private void SetFollowObjectPosition()
    {
        Vector2 MousePos = GlobalObjects.MainCamera.ScreenToWorldPoint(MousePosition);
        Vector2 PlayerPos = GlobalObjects.Player.transform.position;

        Vector2 targetpos = Vector2.Lerp(PlayerPos, MousePos, PlayerToMousePosRatio);
        
        transform.position = Vector2.Lerp(transform.position, targetpos, MoveSpeed*Time.deltaTime);
    }
}
