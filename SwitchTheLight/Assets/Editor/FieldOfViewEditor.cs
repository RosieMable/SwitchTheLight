﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (FieldOfView))]

public class FieldOfViewEditor : Editor {

    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;

        //drawing the fieldview of the targets
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 v3_viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 v3_viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + v3_viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + v3_viewAngleB * fow.viewRadius);


        Handles.color = Color.red;
        foreach(Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }
}