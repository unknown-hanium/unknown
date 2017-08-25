using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    public static GridManager s_Instance = null;

    private Vector3 origin = new Vector2();
    private GameObject[] obstacleList;

    public int numOfCol;
    public int numOfRows;
    public int  gridCellSize;

    public Node[,] nodes { get; set; }
    public Vector3 Origin
    {
        get { return origin; }
    }

    public static GridManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(GridManager))
                    as GridManager;
                if (s_Instance == null)
                    Debug.Log("Could not locate a GridManager " +
                        "object. \n You have to have exactly" +
                        "one GridManager in the scene.");
            }
            return s_Instance;
        }
    }

    void Awake()
    {
        obstacleList = GameObject.FindGameObjectsWithTag("Obstacle");
        CalculateObstacles();
    }
    // Find all of obstacles in the map
    void CalculateObstacles()
    {
        nodes = new Node[numOfCol, numOfRows];
        int index = 0;
        for (int i = 0; i < numOfCol; i++)
        {
            for (int j = 0; j < numOfRows; j++)
            {
                Vector2 cellPos = GetGridCellCenter(index);
                Node node = new Node(cellPos);
                nodes[i, j] = node;
                index++;
            }
        }
        if(obstacleList!=null && obstacleList.Length>0)
        {
            // List each obstacle found on the map
            foreach(GameObject data in obstacleList)
            {
                int indexCell = GetGridIndex(data.transform.position);
                int col = GetColumn(indexCell);
                int row = GetRow(indexCell);
                nodes[row, col].MarkAsObstacle();
            }
        }
    }

    public Vector3 GetGridCellCenter(int index)
    {
        Vector3 cellPosition = GetGridCellPosition(index);
        cellPosition.x += (gridCellSize / 2.0f);
        cellPosition.y += (gridCellSize / 2.0f);
        return cellPosition;
    }

    public Vector3 GetGridCellPosition(int index)
    {
        int row = GetRow(index);
        int col = GetColumn(index);
        float xPosInGrid = col * gridCellSize;
        float zPosInGrid = row * gridCellSize;
        return Origin + new Vector3(xPosInGrid, zPosInGrid);
    }

    public int GetGridIndex(Vector3 pos)
    {
        if (!IsInBounds(pos))
        {
            return -1;
        }
        pos -= Origin;
        int col = (int)(pos.x / gridCellSize);
        int row = (int)(pos.z / gridCellSize);
        return (row * numOfCol + col);
    }

    public bool IsInBounds(Vector3 pos)
    {
        float width = numOfCol * gridCellSize;
        float height = numOfRows * gridCellSize;
        return (pos.x <= Origin.x && pos.x <= Origin.x + width && pos.x <= Origin.z + height && pos.z >= Origin.z);
    }

    public int GetRow(int index)
    {
        int row = index / numOfCol;
        return row;
    }

    public int GetColumn(int index)
    {
        int col = index % numOfCol;
        return col;
    }

    public void GetNeighbours(Node node,ArrayList neighbors)
    {
        Vector3 neighborPos = node.position;
        int neighborIndex = GetGridIndex(neighborPos);

        int row = GetRow(neighborIndex);
        int col = GetColumn(neighborIndex);

        // Down
        int leftNodeRow = row - 1;
        int leftNodeCol = col;
        AssignNeighbour(leftNodeRow, leftNodeCol, neighbors);

        // Up
        leftNodeRow = row + 1;
        leftNodeCol = col;
        AssignNeighbour(leftNodeRow, leftNodeCol, neighbors);

        // Right
        leftNodeRow = row;
        leftNodeCol = col + 1;
        AssignNeighbour(leftNodeRow, leftNodeCol, neighbors);

        //Left
        leftNodeRow = row;
        leftNodeCol = col - 1;
        AssignNeighbour(leftNodeRow, leftNodeCol, neighbors);
    }

    void AssignNeighbour(int row,int col, ArrayList neighbors)
    {
        if (row != -1 && col != -1 && row < numOfRows && col < numOfCol) 
        {
            Node nodeToAdd = nodes[row, col];
            if (!nodeToAdd.bObstacle)
            {
                neighbors.Add(nodeToAdd);
            }
        }
    }

}