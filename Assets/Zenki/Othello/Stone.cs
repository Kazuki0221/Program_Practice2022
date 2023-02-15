using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField]State state;
    [SerializeField] Animator anim;


    public void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ChageState(State _state)
    {
        state = _state;
        if(state == State.White)
        {
            anim.SetBool("OnBlack", true);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if(state == State.Black)
        {
            anim.SetBool("OnWhite", true);
            transform.rotation = Quaternion.Euler(-180, 0, 0);
        }
    }

    public void SetState(State _state)
    {
        state = _state;
    }

    void EndAnim()
    {
        anim.SetBool("OnBlack", false);
        anim.SetBool("OnWhite", false);
    }
}

public enum State
{
    White,
    Black,
    Empty
}


