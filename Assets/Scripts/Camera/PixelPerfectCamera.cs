using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour
{

    public static float pixelToUnits = 1f;
    public static float scale = 1f;

    public Vector2 nativeResolution = new Vector2(160, 144);

    void Awake()
    {
        var camera = GetComponent<Camera>();

        if (camera.orthographic)
        {
            var direction = Screen.height;
            var resolution = nativeResolution.y;

            scale = direction / resolution;
            pixelToUnits *= scale;

            camera.orthographicSize = (direction / 2.0f) / pixelToUnits;
        }
    }

}
