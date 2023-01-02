using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InitialSpawn : MonoBehaviour
{
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var player0 = PlayerInput.Instantiate(playerPrefab,0,"WASD",0,Keyboard.current);
        var player1 = PlayerInput.Instantiate(playerPrefab, 1, "Arrows", 1, Keyboard.current);

        player0.transform.position = new Vector3(-2,0.5f,0);
        player1.transform.position = new Vector3(2, 0.5f, 0);

        player0.transform.parent.GetChild(1).GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
        player1.transform.parent.GetChild(1).GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);

        player0.transform.parent.GetChild(1).GetComponent<Camera>().GetComponent<AudioListener>().enabled= true;

        player0.GetComponent<PlayerController>().playerIndex = 0;
        player1.GetComponent<PlayerController>().playerIndex = 1;
    }
}
