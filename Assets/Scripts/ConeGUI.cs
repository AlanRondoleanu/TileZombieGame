using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(VisionCone))]
public class FieldOfViewEditor : Editor
{

	//void OnSceneGUI()
	//{
	//	VisionCone fow = (VisionCone)target;
	//	Handles.color = Color.white;
	//	Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.right, 360, fow.viewRadius);
	//	Vector3 viewAngleA = fow.DirectionOfAngle(-fow.viewAngle / 2, false);
	//	Vector3 viewAngleB = fow.DirectionOfAngle(fow.viewAngle / 2, false);

	//	Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
	//	Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

 //       Handles.color = Color.red;
 //       foreach (Transform visibleTarget in fow.visibleTargets)
 //       {
 //           Handles.DrawLine(fow.transform.position, visibleTarget.position);
	//		Debug.Log("found");
 //       }
 //   }

}