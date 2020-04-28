using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentScript : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void Animation()
    {
        GameObject.Find("gameController").GetComponent<GameController>().CameraZoom(true);
        animator.SetBool("Disappear", true);
    }
}
