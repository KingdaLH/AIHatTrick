using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HT_AI : MonoBehaviour
{
    private HT_GameController GC;
    private Vector3 targetPos;
    private float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.FindObjectOfType<HT_GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GC.list.Count != 0)
        {
            if (GC.list[0].tag == "Ball")
            {
                targetPos = new Vector3(GC.list[0].transform.position.x, 0, 0);
            }
            else
            {
                if (GC.list[0].transform.position.x > 0)
                {
                    targetPos = new Vector3(-2, 0, 0);
                }
                else
                {
                    targetPos = new Vector3(7, 0, 0);
                }
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }
}
