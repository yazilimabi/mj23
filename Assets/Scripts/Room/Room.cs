using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    public void Start() {
        DisableRoom();
    }

    public void ActivateRoom(Transform follow) {
        for (int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(true);
        }
        virtualCamera.Follow = follow;
    }

    public void DisableRoom() {
        for (int i = 0; i < transform.childCount; i++){
            if(!transform.GetChild(i).GetComponent<Door>())
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
