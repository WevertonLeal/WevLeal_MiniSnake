using Unity.VisualScripting;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField]
    float xLimit = 16.0f;
    [SerializeField]
    float yLimit = 8.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(Random.Range(-xLimit, xLimit), Random.Range(-yLimit, yLimit), 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.gameObject.transform.parent.GetComponent<MiniSnake>().AddTail();
        transform.position = new Vector3(Random.Range(-xLimit, xLimit), Random.Range(-yLimit, yLimit), 0);
            
        }
    }
}
