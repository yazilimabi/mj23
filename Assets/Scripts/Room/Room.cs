using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    public void ActivateRoom(Transform follow) {
        virtualCamera.gameObject.SetActive(true);
        virtualCamera.Follow = follow;
    }

    public void DisableRoom() {
        virtualCamera.gameObject.SetActive(false);
    }
}
