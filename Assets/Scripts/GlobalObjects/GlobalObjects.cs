using UnityEngine;

public static class GlobalObjects 
{
    private static Camera _camera;
    public static Camera MainCamera
    {
        get
        {
            if (_camera == null)
                _camera = Camera.main;
            return _camera;
        }
    }


    private static GameObject player;
    public static GameObject Player
    {
        get
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");
            return player;
        }
    }
}
