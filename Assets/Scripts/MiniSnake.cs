using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

enum Dir
{
    Up,Right,Down,Left
}
public class MiniSnake : MonoBehaviour
{
    GameManager gameManager;
    Dir currentDir = Dir.Right;
    public GameObject headPrefab;
    public GameObject TailPrefab;
    List<GameObject> tail = new List<GameObject>();
    public float baseSpeed = 5;
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject head = Instantiate(headPrefab, transform.position, quaternion.identity);
        tail.Add(head);
        head.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameHasEnded)
        {
            return;
        }
        HandleRotationIput();
        MoveForward();
        UpdateTailPositions();
    }

    void RotateCounterClockwise()
    {
        currentDir = (Dir)(((int)currentDir + 3) % 4);
    }

    void RotateClockwise()
    {
        currentDir = (Dir)(((int)currentDir + 1) % 4);

    }
    void MoveForward()
    {
        Vector3 moveDir = Vector3.zero;
        switch (currentDir)
        {
            case Dir.Up:
                moveDir = Vector3.up;
                break;
            case Dir.Down:
                moveDir = Vector3.down;
                break;
            case Dir.Left:
                moveDir = Vector3.left;
                break;
            case Dir.Right:
                moveDir = Vector3.right;
                break;
        }
        transform.position += moveDir * speed * Time.deltaTime;
    }
    void HandleRotationIput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            RotateCounterClockwise();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            RotateClockwise();
        }

    }

    public void AddTail()
    {
        Vector3 newPosition = Vector3.zero;
        switch (currentDir)
        {
            case Dir.Up:
                newPosition = tail[tail.Count - 1].transform.position - new Vector3(0, 1, 0);
                break;
            case Dir.Down:
                newPosition = tail[tail.Count - 1].transform.position + new Vector3(0, 1, 0);
                break;
            case Dir.Left:
                newPosition = tail[tail.Count - 1].transform.position + new Vector3(1, 0, 0);
                break;
            case Dir.Right:
                newPosition = tail[tail.Count - 1].transform.position - new Vector3(1, 0, 0);
                break;
        }
        GameObject newTail = Instantiate(TailPrefab, newPosition, quaternion.identity);
        tail.Add(newTail);
    }
    void UpdateTailPositions()
    {
        float segmentDistance = 1.2f;
        for (int i = 1; i < tail.Count; i++)
        {
            GameObject currentSegment = tail[i];
            GameObject segmentInFront = tail[i - 1];
            Vector3 newPosition = segmentInFront.transform.position - (segmentInFront.transform.position - currentSegment.transform.position).normalized * segmentDistance;
            currentSegment.transform.position = Vector3.Lerp(currentSegment.transform.position, newPosition, Time.deltaTime * 30);
        }
    }
    
}
