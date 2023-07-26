using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMove : MonoBehaviour
{
    public Transform leftLeg;
    public Transform rightLeg;
    public Transform bodyPos;
    public GameObject body;
    Vector3 dir = new Vector3(0, 0, 1);

    public Transform origin;

    private void Start()
    {
        GameManager.Instance.OnGameFinished.AddListener(ResetAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State != GameState.InGame ) return;

        bodyPos.localPosition = origin.localPosition + 0.04f * transform.localScale.y * Vector3.up;
       
        body.transform.position = bodyPos.position;
        
        leftLeg.Rotate(60 * Time.deltaTime * dir);
        rightLeg.Rotate(60 * Time.deltaTime * -dir);
        

        if(leftLeg.localRotation.z > 0.08f)
        {
            dir = new Vector3(0, 0, -1);
        }
        else if(leftLeg.localRotation.z < -0.08f)
        {
            dir = new Vector3(0, 0, 1);
        }
        
    }

    void ResetAngle()
    {
        leftLeg.rotation = Quaternion.identity;
        rightLeg.rotation = Quaternion.identity;
    }
}
