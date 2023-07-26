using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform pos;
    KingGu kingGu;

    public int count;
    void Start()
    {
        kingGu = FindAnyObjectByType<KingGu>();
        pos = kingGu.characPos;
        SoundEffectManager.Instance.PlayChSound(count);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, pos.position, 20 * Time.deltaTime);
        if (Vector2.Distance(transform.position, pos.position) < 0.5f)
        {
            kingGu.AddChar(count);
            
            Destroy(gameObject);
        }
    }
   
}
