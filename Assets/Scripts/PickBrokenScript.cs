using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBrokenScript : MonoBehaviour
{
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private Rigidbody rigidbody;

    public int forceDirection;
    public float forceAmount;
    // Start is called before the first frame update

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickBroke();
        }
        else if (Input.GetKeyDown(KeyCode.F))
            ResetPick();
    }

    public void PickBroke()
    {
        rigidbody.isKinematic = false;
        rigidbody.AddForce(Vector3.right * forceDirection * forceAmount, ForceMode.Impulse);
    }

    public void ResetPick()
    {
        rigidbody.isKinematic = true;
        transform.position = startingPosition;
        transform.rotation = startingRotation;
    }

}
