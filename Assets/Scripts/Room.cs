using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject virtualCamera;

    public void ActivateRoom() {
        virtualCamera.SetActive(true);
    }

    public void DisableRoom() {
        virtualCamera.SetActive(false);
    }
}
