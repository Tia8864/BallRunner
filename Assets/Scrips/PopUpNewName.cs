using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpNewName : MonoBehaviour
{
    public void _Show(Animator animator)
    {
        animator.SetBool("turnon", true);
    }

    public void _Hide(Animator animator)
    {
        animator.SetBool("turnon", false);
    }
}
