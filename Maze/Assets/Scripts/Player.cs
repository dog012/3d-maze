using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private MazeCell currentCell;

    public void SetLocation(MazeCell cell)
    {
        //if (currentCell != null)
        //{
        //    currentCell.OnPlayerExited();
        //}
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
        //currentCell.OnPlayerEntered();
    }

    private void Move(MazeDirection direction)
    {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage)
        {
            SetLocation(edge.otherCell);
        }
    }


    private MazeDirection currentDirection;

    private void Look(MazeDirection direction)
    {
        transform.localRotation = direction.ToRotation();
        currentDirection = direction;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(currentDirection);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(currentDirection.GetOpposite());
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }
}
