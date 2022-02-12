using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickMovementScript : MonoBehaviour
{
    private GameObject keyHole;
    // Start is called before the first frame update
    void Start()
    {
        keyHole = GameObject.FindGameObjectWithTag("KeyHole");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = keyHole.transform.position;
    }
}
