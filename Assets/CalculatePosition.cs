using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CalculatePosition : MonoBehaviour
{
    public Transform Body;

    public Transform targetPoint1;
    public Transform Point2;
    public Transform Point3;
    Vector3 initalvector;

    Vector3 getPoint1;
    Vector3 getPoint2;
    Vector3 getPoint3;

    //Rotation 보정
    Vector3 setvector;


    //Scale 보정
    float k;




    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void BodyMaking()
    {
        getPoint1 = new Vector3(JetController.pointPosition[0].Item1, JetController.pointPosition[0].Item2, JetController.pointPosition[0].Item3);
        getPoint2 = new Vector3(JetController.pointPosition[1].Item1, JetController.pointPosition[1].Item2, JetController.pointPosition[1].Item3);
        getPoint3 = new Vector3(JetController.pointPosition[2].Item1, JetController.pointPosition[2].Item2, JetController.pointPosition[2].Item3);

        float L1 = Vector3.Distance(targetPoint1.position, Point2.position);
        float L2 = Vector3.Distance(targetPoint1.position, Point3.position);
        float L3 = Vector3.Distance(Point2.position, Point3.position);

        float l1 = Vector3.Distance(getPoint1, getPoint2);
        float l2 = Vector3.Distance(getPoint1, getPoint3);
        float l3 = Vector3.Distance(getPoint2, getPoint3);

        k = (L1 * l1 + L2 * l2 + L3 * l3) / (L1 * L1 + L2 * L2 + L3 * L3);


        initalvector = Vector3.Cross(targetPoint1.position - Point2.position, targetPoint1.position - Point3.position).normalized;
        setvector = Vector3.Cross(getPoint1 - getPoint2, getPoint1 - getPoint3).normalized;

        SetScale(k);

        SetRotation(initalvector, setvector);

        SetnewlocalPosition();


    }

    public void SetScale(float k)
    {
        Vector3 currentScale = Body.transform.localScale;

        Body.transform.localScale = new Vector3(currentScale.x * k, currentScale.y * k, currentScale.z * k);
    }

    public void SetRotation(Vector3 initalvector, Vector3 setvector)
    {
        Quaternion rotation = Quaternion.FromToRotation(initalvector, setvector);
        //Quaternion origin = Body.transform.rotation;

        Body.transform.rotation = rotation;
    }

    public void SetnewlocalPosition()
    {
        Vector3 objectPosition = Vector3.zero;
        GameObject targetObject = GameObject.Find("TargetPoint1");
        if (targetObject != null)
        {
            objectPosition = targetObject.transform.position;
        }
        else
        {
            Debug.Log("There is no TargetPoint1");
        }


        Body.transform.position += (getPoint1 - objectPosition);


    }
}
