using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KingGu : MonoBehaviour
{
    public float rot;

    public Transform characPos;

    public GameObject[] characters;

    public Rigidbody2D rigid;

    public Transform legPos;
    int nextDist = 0;

    float maxAngularSpeed = 500;

    public GameObject leg;
    public GameObject body;
    public Transform bodyPos;

    public float timeMax = 1.5f;

    public float increase;
    void Start()
    {
        foreach (var character in characters)
        {
            character.SetActive(false);
        }
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = false;
        //rigid.centerOfMass = legPos.localPosition;
        nextDist = 10;

        GameManager.Instance.OnGameFinished.AddListener(ResetKingGu);
        GameManager.Instance.OnGameFinished.AddListener(StartGrow);              
    }

    float maxRot = 40;
    float time = 0;

    void StartGrow()
    {
        StartCoroutine(Grow());
    }
    private void Update()
    {
        if (GameManager.Instance.State != GameState.InGame)
        {
            return;
        }

        if (Mathf.Abs(UIManager.dist - nextDist) < 0.1f)
        {
            nextDist += 10;

            if (rot < maxRot)
            {
                rot += increase;
                timeMax -= 0.02f;
            }
            BGScroll.scrollSpeed += 0.01f;
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.State != GameState.InGame)
        {
            return;
        }
        float value = Mathf.Lerp(0.01f, 1.5f, Mathf.Abs(transform.rotation.z) / 0.7f);

        if (transform.rotation.z < 0) value *= -1;
        float gravityTorque = Physics2D.gravity.y * rigid.mass * rot * value / 10;

        float v = 1 + time / timeMax;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            rigid.AddTorque(Time.fixedDeltaTime * Physics2D.gravity.y * rigid.mass * -rot * 6f * v / 10);
          
            time += Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            
            rigid.AddTorque(Time.fixedDeltaTime * Physics2D.gravity.y * rigid.mass * rot * 6f * v / 10);
          
            time += Time.fixedDeltaTime;
        }
        else
        {
            time = 0;
        }

        //if (!(Input.GetKey(KeyCode.LeftArrow) && rigid.angularVelocity > 0) || !(Input.GetKey(KeyCode.RightArrow) && rigid.angularVelocity < 0)) 
        rigid.AddTorque(-gravityTorque * Time.fixedDeltaTime * 1.5f);

        if (rigid.angularVelocity > maxAngularSpeed)
        {
            rigid.angularVelocity = maxAngularSpeed;
        }
        else if (rigid.angularVelocity < -maxAngularSpeed)
        {
            rigid.angularVelocity = -maxAngularSpeed;
        }

    }


    public void AddChar(int count)
    {
        characters[count].SetActive(true);

        float grav = Mathf.Lerp(1, 2f, Mathf.Abs(transform.rotation.z) / 0.78f);

        float power = (1.5f + count * 0.15f * grav) / 7;

        if (transform.rotation.z > 0)
        {
            rigid.AddTorque(power, ForceMode2D.Impulse);
        }
        else
        {
            rigid.AddTorque(-power, ForceMode2D.Impulse);
        }
    }

    public void ResetKingGu()
    {
        rigid.angularVelocity = 0;
        rigid.isKinematic = true;
        transform.rotation = Quaternion.identity;
    }

    public IEnumerator Grow()
    {
        yield return new WaitForSeconds(1);
        float time = 0;
        StartCoroutine(GameManager.Instance.MoveCam());

        while (time < 2f)
        {
            leg.transform.localScale += Vector3.up * Time.deltaTime;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            body.transform.position = bodyPos.position;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
