using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialController : MonoBehaviour
{
    private Transform ChangeWorld;
    private Vector3 CheckPos = Vector3.zero;
    private Vector3 Moving = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWorld = this.transform;
        CheckPos = transform.position;
        Moving = new Vector3(CheckPos.x, CheckPos.y, 0.0f);

        if(CheckPos.z == 3.5f && Input.GetKeyDown(KeyCode.C))
        {
            Moving.z = 0.0f;
            ChangeWorld.position = Moving;
        } 
        else if(CheckPos.z == 0.0f && Input.GetKeyDown(KeyCode.C))
        {
            Moving.z = 3.5f;
            ChangeWorld.position = Moving;
        }
    }
}
