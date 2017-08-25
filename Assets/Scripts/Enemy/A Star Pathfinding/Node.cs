using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IComparable {
    public float nodeTotalCost;
    public float estimatedCost;
    public bool bObstacle;
    public Node parent;
    public Vector3 position;

    public Node()
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
    }

    public Node(Vector3 pos)
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
        this.position = pos;
    }

    public void MarkAsObstacle()
    {
        this.bObstacle = true;
    }

    public int CompareTo(object obj)
    {
        Node node = (Node)obj;
        // A negative value means that the object is aligned and is ahead of the current
        if (this.estimatedCost < node.estimatedCost)
            return -1;
        // A positive value means that the object is sorted and is behind of the current
        if (this.estimatedCost > node.estimatedCost) return 1;
        return 0;
    }
}
