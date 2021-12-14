using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentInput : MonoBehaviour
{
    public bool LeftKey { get { return Input.GetKey(KeyCode.LeftArrow); } }
    public bool RighttKey { get { return Input.GetKey(KeyCode.RightArrow); } }
    public bool CrouchKey { get { return Input.GetKey(KeyCode.DownArrow); } }
    public bool DashKey { get { return Input.GetKeyDown(KeyCode.C);} }
    public bool AttackKey { get { return Input.GetKeyDown(KeyCode.X);} }
    public bool JumpKey { get { return Input.GetKeyDown(KeyCode.V);  } }
    public bool UpKey { get { return Input.GetKey(KeyCode.UpArrow);} }


}
