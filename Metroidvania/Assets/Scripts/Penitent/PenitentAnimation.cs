using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentAnimation : MonoBehaviour
{
    private Animator _anim;
    private PenitentInput _input;

    private string ISRUNNIG = "IsRunning";
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _input = GetComponent<PenitentInput>();

    }

    void Start()
    {

    }
    private void FixedUpdate()
    {
        _anim.SetBool(ISRUNNIG, PlayerRun());
    }
    bool PlayerRun()
    {
        if (_input.LeftKey && _input.RighttKey)
            return false;
        else if (_input.LeftKey || _input.RighttKey)
            return true;

        return false;
    }
}
