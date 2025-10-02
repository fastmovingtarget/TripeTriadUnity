using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public BoardSpaceManager[,] boardSpaces = new BoardSpaceManager[3, 3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                // Child order is (0,0), (0,1), (0,2), (1,0), (1,1), (1,2), (2,0), (2,1), (2,2)
                // so index = 
                boardSpaces[j, i] = transform.GetChild((3*i)+j).GetComponent<BoardSpaceManager>();
                if (boardSpaces[j, i].X != j || boardSpaces[j,i].Y != i)
                    Debug.LogError($"Board space at ({j},{i}) has incorrect coordinates ({boardSpaces[j,i].X},{boardSpaces[j,i].Y})");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCardPlaced(BoardSpaceManager space)
    {
        foreach(BoardSpaceManager adjacentSpace in GetAdjacentSpaces(space)) 
        {             
            if(adjacentSpace != null)
            {
                if (adjacentSpace.OccupyingCard != null)
                {
                    if (adjacentSpace.X == space.X) // Same column
                    {
                        if(adjacentSpace.Y < space.Y) // Above
                        {
                            if (space.OccupyingCard.Top > adjacentSpace.OccupyingCard.Bottom && space.OccupyingCard.CurrentOwner != adjacentSpace.OccupyingCard.CurrentOwner)
                            {
                                adjacentSpace.ChangeOwnership(space.OccupyingCard.CurrentOwner);
                                gameObject.SendMessageUpwards("AddScore", space.OccupyingCard.CurrentOwner);
                            }
                        }
                        else // Below
                        {
                            if (space.OccupyingCard.Bottom > adjacentSpace.OccupyingCard.Top && space.OccupyingCard.CurrentOwner != adjacentSpace.OccupyingCard.CurrentOwner)
                            {
                                adjacentSpace.ChangeOwnership(space.OccupyingCard.CurrentOwner);
                                gameObject.SendMessageUpwards("AddScore", space.OccupyingCard.CurrentOwner);
                            }
                        }
                    }
                    else // Same row
                    {
                        if(adjacentSpace.X > space.X) // Right
                        {
                            if (space.OccupyingCard.Right > adjacentSpace.OccupyingCard.Left && space.OccupyingCard.CurrentOwner != adjacentSpace.OccupyingCard.CurrentOwner)
                            {
                                adjacentSpace.ChangeOwnership(space.OccupyingCard.CurrentOwner);
                                gameObject.SendMessageUpwards("AddScore", space.OccupyingCard.CurrentOwner);
                            }
                        }
                        else // Left
                        {
                            if (space.OccupyingCard.Left > adjacentSpace.OccupyingCard.Right && space.OccupyingCard.CurrentOwner != adjacentSpace.OccupyingCard.CurrentOwner)
                            {
                                adjacentSpace.ChangeOwnership(space.OccupyingCard.CurrentOwner);
                                gameObject.SendMessageUpwards("AddScore", space.OccupyingCard.CurrentOwner);
                            }
                        }
                    }
                }
            }
        }
    }

    public void ResetBoard()
    {
        foreach(BoardSpaceManager child in boardSpaces)
        {
            child.GetComponent<BoardSpaceManager>().ResetSpace();
        }
    }

    public BoardSpaceManager[] GetAdjacentSpaces(BoardSpaceManager space)
    {
        BoardSpaceManager[] adjacentSpaces = new BoardSpaceManager[4]; // Top, Right, Bottom, Left
        if(space.Y < 2) adjacentSpaces[0] = boardSpaces[space.X, space.Y + 1];
        if(space.X < 2) adjacentSpaces[1] = boardSpaces[space.X + 1, space.Y];
        if(space.Y > 0) adjacentSpaces[2] = boardSpaces[space.X, space.Y - 1];
        if(space.X > 0) adjacentSpaces[3] = boardSpaces[space.X - 1, space.Y];
        return adjacentSpaces;
    }
}
