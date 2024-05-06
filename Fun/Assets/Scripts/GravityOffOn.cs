using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOffOn : MonoBehaviour
{
    public Rigidbody Cube;
    public Transform Target;
    public Transform Cubepos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float abstandX = (Target.position.x - Cubepos.position.x );
        float abstandY = (Target.position.y - Cubepos.position.y);
        float abstandZ = (Target.position.z - Cubepos.position.z);
        float abstand = Mathf.Sqrt(Mathf.Pow(abstandX, 2)) + Mathf.Sqrt(Mathf.Pow(abstandY, 2)) + Mathf.Sqrt(Mathf.Pow(abstandZ, 2));
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (Cube.useGravity)
            {
                Cube.useGravity = false;
            }
            else
            {
                Cube.useGravity = true;
            }
        }
        if (Input.GetKey(KeyCode.F))
        {
            Cube.AddForce(new Vector3 (abstandX / abstand, abstandY / abstand, abstandZ / abstand) * 2);
        }
    }
}
