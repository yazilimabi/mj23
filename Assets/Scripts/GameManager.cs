using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance = null;
    public static GameManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

            return instance;
        }
    }

    public GameObject player;
}
