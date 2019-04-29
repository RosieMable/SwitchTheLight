using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfView : MonoBehaviour {

    //Variables


//Variables that declare how wide the angle can be 
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;

    private TextManager textManager;

    private float NextInteractionTimer;

    //Creates a list of the targets that have been detected, it will be used to understand in which state the enemy is
    public List<Transform> visibleTargets = new List<Transform>();

    private void Awake()
    {
        textManager = FindObjectOfType<TextManager>();
    }

    private void FixedUpdate()
    {
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    IEnumerator FindTargetsWithDelay (float fl_delay)
    {

            yield return new WaitForSeconds(fl_delay);
            FindVisibleTargets();


    }


    //Function that finds the targetsin the view radius placed on a certain layer mask
    void FindVisibleTargets()
    {

        visibleTargets.Clear();

        //Looks for objects with colliders inside of a sphere with a determined radius on a specified layer mask
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);


            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);

                    //Checks if there is an object with the interactable script on, if so then...
                    if (target.GetComponent<Interactable>())
                    {
                        visibleTargets.Add(target);

                        textManager.textIntName.text = target.name + " Interact (E)";

                             if (Input.GetKey(KeyCode.E)) //If pressed E then interact
                            {
                              target.GetComponent<Interactable>().Interact();
                            }

                    }
  
                }
            }

        if (visibleTargets.Count <= 0)
        {
            textManager.textIntName.text = string.Empty;

        }

    }


    //Used to calculate the angle, it is used for the visual reppresentation in the editor
    public Vector3 DirFromAngle(float fl_angleInDegrees, bool bl_angleIsGlobal)
    {
        if(!bl_angleIsGlobal)
        {
            fl_angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(fl_angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(fl_angleInDegrees * Mathf.Deg2Rad));
    }

}
