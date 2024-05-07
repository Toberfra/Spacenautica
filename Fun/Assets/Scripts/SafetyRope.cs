using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SafetyRope : MonoBehaviour
{
    public bool test = false;
    public LineRenderer lr;
    public Transform Anchor;
    public Transform Player;
    public PlayerMovement pm;
    public float PullSpeed = 7;
    public float XFaktor = 0;
    public float YFaktor = 0;
    public float ZFaktor = 0;
    //private float abstand = 0;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            lr.SetPosition(0, Player.position);
            
            Vector3 abstand = Player.position - Anchor.position;

            if(abstand.sqrMagnitude > 50*50  )  

            {
                //Debug.Log("Rope zu lang");
                Player.position = Anchor.position + Vector3.ClampMagnitude(abstand,50);
                pm.Stop();
            }
            if (Input.GetKey(KeyCode.E))
            {
                Player.position -= abstand.normalized * PullSpeed * Time.deltaTime;
                pm.Stop();
            }
        }
    }
    public void Test()
    {
        if (test)
        {
            test = false;
            lr.enabled = false;
        }
        else
        {
            lr.enabled = true;
            test = true;
            lr.SetPosition(1, Anchor.position);
        }
    }
}