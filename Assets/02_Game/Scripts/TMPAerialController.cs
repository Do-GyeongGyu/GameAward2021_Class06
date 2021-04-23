using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPAerialController : MonoBehaviour
{
    public enum AerialState
    {
        STATE_FRONT,
        STATE_BACK,
    };

    public GameObject obj;
    public AerialState _AerialState;

    public void SetAerialState(AerialState s)
    {
        _AerialState = s;
    }

    public AerialState GetAerialState()
    {
        return _AerialState;
    }

    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("Aerial");
        _AerialState = AerialState.STATE_BACK;
        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("WorldMgr").GetComponent<WorldMgr>().GetWorldState() == WorldMgr.WorldState.STATE_FRONT)
        {
            if(_AerialState == AerialState.STATE_FRONT)
            {
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj.gameObject.SetActive(false);
            }
        }
        else
        {
            if (_AerialState == AerialState.STATE_FRONT)
            {
                obj.gameObject.SetActive(false);
            }
            else
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}
