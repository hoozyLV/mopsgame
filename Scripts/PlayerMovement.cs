using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Rigidbody2D rb2d;
    Animator animator;
    Rigidbody rb;

    public float speed = 20f;

    //Vector2 movement;

    Vector3 mov;
    Vector3 prevMov = new Vector3(0,0,0);

    private void Awake() {
        //rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update() {
        mov.x = Input.GetAxisRaw("Horizontal");
        mov.z = Input.GetAxisRaw("Vertical");
        mov = mov.normalized;

        animator.SetFloat("Speed", mov.sqrMagnitude);

        if (mov != Vector3.zero) {
            animator.SetFloat("X axis", mov.x);
            animator.SetFloat("Z axis", mov.z);
        }

    }

    void FixedUpdate() {
        //rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);

        rb.MovePosition(rb.position + mov * speed * Time.fixedDeltaTime);
    }
}
