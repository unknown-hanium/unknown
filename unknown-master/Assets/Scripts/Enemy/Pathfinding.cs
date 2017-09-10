using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour {

    PathRequestManager requestManager;
    Grid grid;

    void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }
    
    public void StartFindPath(Vector3 startPos,Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos,Vector3 targetPos)
    {

        Vector3[] waypoints=new Vector3[0];
        bool pathSuccess = false;

        //시작 노드와 타겟 노드를 그리드로 부터 스타트 위치와 타겟 위치를 받아와 지정
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        
        if (startNode.walkable && targetNode.walkable)
        {
            // Open_Node & Close_Node
            List<Node> openSet = new List<Node>();
            HashSet<Node> closeSet = new HashSet<Node>();
            // OpenList에 처음 시작노드 추가
            openSet.Add(startNode);
            
            //OpenList가 비어있을 때까지 반복
            while (openSet.Count > 0)
            {
                // OpenList로 부터 첫 번째 노드를 현재노드로 설정
                Node currentNode = openSet[0];
                for(int i = 1; i < openSet.Count; i++)
                {
                    if(openSet[i].fCost<currentNode.fCost || openSet[i].fCost==currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }
                openSet.Remove(currentNode);
                closeSet.Add(currentNode);
                // 현재노드위치가 타겟노드위치와 같다면 역추적함수 실행
                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }
                // 현재노드 기준으로 8방향의 위치를 그리드로부터 얻는다.
                foreach (Node neighbour in grid.GetNeighboursNode(currentNode))
                {
                    // 주변 노드가 움직일 수 있는지와 ClostList에 포함되는지 검사
                    if (!neighbour.walkable || closeSet.Contains(neighbour))
                    {
                        continue;
                    }
                    // F = G + H
                    int newMovementCostNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    // F가 주변노드 코스트보다 낮고 OpenList에 포함되지 않을 경우
                    if (newMovementCostNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints=RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] RetracePath(Node startNode,Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for(int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld=directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA,Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeA.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridX);

        if (dstX > dstY)
            return 14*dstY + 10*(dstX-dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
