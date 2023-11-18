using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomController : MonoBehaviour
{
    PlayerMovement playerMovement;

    HashSet<Room> activeRooms;

    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        activeRooms = new HashSet<Room>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Room")) {
            var room = col.GetComponent<Room>();
            room.ActivateRoom(transform);
            activeRooms.Add(room);
            if(activeRooms.Count > 1){
                playerMovement.DisableMovement(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if(col.CompareTag("Room")) {
            var room = col.GetComponent<Room>();
            room.DisableRoom();
            activeRooms.Remove(room);
            if(activeRooms.Count <= 1){
                playerMovement.EnableMovement();
            }
        }
    }
}
