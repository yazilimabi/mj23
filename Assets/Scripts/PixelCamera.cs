using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCamera : MonoBehaviour
{
    Transform camera;
    [SerializeField] float pixelSize = 1/3f;
    void Start()
    {
        camera = Camera.main.transform;
    }

    [ExecuteInEditMode]
    void Update()
    {
        transform.position = new Vector3(Mathf.Round(camera.position.x * 3f) / 3f
                                        ,Mathf.Round(camera.position.y * 3f) / 3f, -10);
    }
}
