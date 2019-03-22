using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ----- Movement Variables     -------------------------------------------
    public float fl_speed = 4.0F;
    public float fl_jump_force = 20;
    public float fl_gravity = 10.0F;
    public float fl_rotation_rate = 180;
    private Vector3 v3_move_direction = Vector3.zero;
    private CharacterController cc_PC;
    private float fl_initial_speed;
    public bool iswalking = false;
    public Interactable currentInteractable;

    private bool bl_climbing;
    public GameObject go_PC_camera;

    //-------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {   // get a reference to the attached Character Controller
        cc_PC = GetComponent<CharacterController>();
        go_PC_camera = transform.Find("PC_Camera").gameObject;

        fl_initial_speed = fl_speed;
    }//-----

    //-------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        MovePC();
    }//-----

    //-------------------------------------------------------------------------
    //  PC Movement control
    void MovePC()
    {
        // Rotate PC with Mouse 
        transform.Rotate(0, fl_rotation_rate * Time.deltaTime * Input.GetAxis("Mouse X"), 0);

        //  PC Ground Movement
        if (cc_PC.isGrounded)
        {
            // Add X & Z movement to the direction vector based input axes (W,S or Cursor) 
            v3_move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Convert world coordinates to local for the PC and multiply by speed
            v3_move_direction = fl_speed * transform.TransformDirection(v3_move_direction);

            if (cc_PC.velocity.x != 0)
            {

                iswalking = true;
            }
            else
            {
                iswalking = false;
            }
        }

            v3_move_direction.y -= fl_gravity * Time.deltaTime;

        // Move the character controller with the direction vector
        cc_PC.Move(v3_move_direction * Time.deltaTime);



    }// ----- 
}
