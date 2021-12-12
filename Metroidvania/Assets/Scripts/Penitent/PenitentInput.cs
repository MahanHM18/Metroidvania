using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentInput : MonoBehaviour
{
    public bool LeftKey { get { return Input.GetKey(KeyCode.LeftArrow); } }
    public bool RighttKey { get { return Input.GetKey(KeyCode.RightArrow); } }
    
    public bool Jump { get { return Input.GetKeyDown(KeyCode.V);  } }

}
