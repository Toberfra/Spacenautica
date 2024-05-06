using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public SafetyRope pm;
    public bool pressed = false;
    public Transform TheButton;
    public float X = 0;
    public float Y = 0;
    public float Z = 0;
    public float Y2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindObjectOfType(typeof(SafetyRope)) as SafetyRope;
        X = TheButton.position.x;
        Y = TheButton.position.y;
        Z = TheButton.position.z;
        Y2 = TheButton.position.y - 0.125f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseUpAsButton()
    {
        pm.Test();
        if (!pressed)
        {
            pressed = true;
            TheButton.position = new Vector3 (X, Y2, Z);
        }
        else
        {
            pressed = false;
            TheButton.position = new Vector3 (X, Y, Z);
        }
    }
}
