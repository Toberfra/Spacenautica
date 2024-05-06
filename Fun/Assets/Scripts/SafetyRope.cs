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
    private float abstand = 0;

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
            //if (Mathf.Sqrt((Rope.position.x - Anchor.position.x) * (Rope.position.x - Anchor.position.x) + (Rope.position.y - Anchor.position.y) * (Rope.position.y - Anchor.position.y) + (Rope.position.z - Anchor.position.z) * (Rope.position.z - Anchor.position.z)) > 50)
            //{
            //    Debug.Log("Rope zu lang");
            //}
            float abstandX = Player.position.x - Anchor.position.x;
            float abstandY = Player.position.y - Anchor.position.y;
            float abstandZ = Player.position.z - Anchor.position.z;
            abstand = Mathf.Sqrt(abstandX*abstandX + abstandY*abstandY + abstandZ*abstandZ );
            if (abstand > 50)

            {
                Debug.Log("Rope zu lang");
                float faktor = 50 / abstand;
                float NewX =  Anchor.position.x + abstandX * faktor;
                float NewY =  Anchor.position.y + abstandY * faktor;
                float NewZ =  Anchor.position.z + abstandZ * faktor;
                Player.position = new Vector3 (NewX, NewY, NewZ);
                pm.Stop();
            }
            if (Input.GetKey(KeyCode.E))
            {
                XFaktor = Player.position.x - PullSpeed * (abstandX / abstand) * Time.deltaTime;
                YFaktor = Player.position.y - PullSpeed * (abstandY / abstand) * Time.deltaTime;
                ZFaktor = Player.position.z - PullSpeed * (abstandZ / abstand) * Time.deltaTime;
                Player.position = new Vector3 (XFaktor, YFaktor, ZFaktor);
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
