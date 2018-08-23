using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothUnityLerps : MonoBehaviour {

    public enum E_lerpType {Default,EaseOut,EaseIn,Exponential,Smoothstep,Smootherstep}
    public E_lerpType lerpType;

    public float lerpTime = 1f;
    private float currentLerpTime;

    private Vector3 startPos;
    private Vector3 endPos;

    //public Transform tar;

    public void StartLerp(Transform target)
    {
        startPos = this.transform.position;
        endPos = OffsetTargetZ(target);
        currentLerpTime = 0f;
        StartCoroutine(IE_Lerp());
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartLerp(tar);
        }
    }*/

    private IEnumerator IE_Lerp()
    {
        while (currentLerpTime < lerpTime)
        {
            //increment timer once per frame;
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            //Lerp method decision;
            float t = currentLerpTime / lerpTime;

            if (lerpType == E_lerpType.Default)
            {
                t = currentLerpTime / lerpTime;
            }
            else if (lerpType == E_lerpType.EaseOut)
            {
                t = Mathf.Sin(t * Mathf.PI * 0.5f);
            }
            else if (lerpType == E_lerpType.EaseIn)
            {
                t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
            }
            else if (lerpType == E_lerpType.Exponential)
            {
                t = t * t;
            }
            else if (lerpType == E_lerpType.Smoothstep)
            {
                t = t * t * (3f - 2f * t);
            }
            else if (lerpType == E_lerpType.Smootherstep)
            {
                t = t * t * t * (t * (6f * t - 15f) + 10f);
            }

            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return 0;
        }
        //Just to make sure;
        this.transform.position = endPos;
        print("Smooth transition finished");
    }

    private Vector3 OffsetTargetZ(Transform t)
    {
        Vector3 v = new Vector3(t.position.x,t.position.y,t.position.z - 10f);
        return v;
    }
}
