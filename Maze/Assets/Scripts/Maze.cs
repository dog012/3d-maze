using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    public IntVector3 size;

    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;

    public float generationStepDelay;

    private MazeCell[,,] cells;

    public IntVector3 RandomCoord
    {
        get
        {
            return new IntVector3(Random.Range(0, size.x), Random.Range(0, size.y), Random.Range(0, size.z));
        }
    }

    public bool ContainsCoord(IntVector3 coord)
    {
        return coord.x >= 0 && coord.x < size.x && 
            coord.y >=0 && coord.y < size.y &&
            coord.z >= 0 && coord.z < size.z;
    }

    public MazeCell GetCell(IntVector3 coord)
    {
        return cells[coord.x, coord.y, coord.z];
    }

    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.y, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoord));
    }

    [Range(0f, 1f)]
    public float wallProbability = 1f;

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        //get last cell in active list
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        //remove fullyInitialized cell
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        //go random direction
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector3 coord = currentCell.coord + direction.ToIntVector3();
        //is coord in grid?
        if (ContainsCoord(coord))
        {
            MazeCell neighbor = GetCell(coord);
            //is coord visited?
            if (neighbor == null)
            {
                neighbor = CreateCell(coord);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                //wall probability
                if (Random.value < wallProbability)
                {
                    CreateWall(currentCell, neighbor, direction);
                }
                else
                {
                    CreatePassage(currentCell, neighbor, direction); 
                }
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }
    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private MazeCell CreateCell(IntVector3 coord)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coord.x, coord.y, coord.z] = newCell;
        newCell.coord = coord;
        newCell.name = "Maze Cell" + coord.x + ", " + coord.y + ", " + coord.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coord.x - size.x * 0.5f + 0.5f, coord.y - size.y * 0.5f + 0.5f, coord.z - size.z * 0.5f + 0.5f);
        return newCell;
    }
}
