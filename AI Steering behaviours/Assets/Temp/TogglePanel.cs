using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    Animator _anim;
	// Use this for initialization
	void Start () {
        _anim = GetComponent<Animator>();
	}
	
    public void ToggleBool()
    {
        _anim.SetBool("In", !_anim.GetBool("In"));
    }
}
