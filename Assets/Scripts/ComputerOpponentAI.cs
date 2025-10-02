using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class ComputerOpponentAI : MonoBehaviour
{
    private List<GameObject> Hand = new List<GameObject>();
    private BoardManager boardManager;
    public PlayerID playerID;
    public int defenseWeighting = 5; // How much to prioritize defense over attack
                                     // Higher values mean more defense, lower values mean more attack
                                     // 5 is neutral

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Because MB is added after HandManager's Start, we can populate the hand here
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialise(PlayerID playerID)
    {
        Hand.Clear();
        this.playerID = playerID;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Hand.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    public void OnCardPlaced(GameObject bestCard)
    {
        Hand.Remove(bestCard);
        gameObject.SendMessage("CardPlayed", bestCard);
    }

    public void TakeTurn()
    {
        if (Hand.Count == 0)//just in case
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Hand.Add(gameObject.transform.GetChild(i).gameObject);
            }
        }
        if (boardManager == null)
            boardManager = GameObject.Find("Board").GetComponent<BoardManager>();

        GameObject bestCard = null;
        //int bestChanges = -1;
        float bestWeighting = float.NegativeInfinity;
        int[] bestSpace = null;

        foreach (GameObject handCard in Hand)
        {
            foreach (int[] space in FindValidSpaces())
            {
                float weighting = CalculateWeighting(handCard.GetComponent<CardData>().Card, space[0], space[1]);
                
                if (weighting > bestWeighting)
                {
                    bestCard = handCard;
                    bestWeighting = weighting;
                    bestSpace = space;
                }
            }
        }
        if (bestCard != null && bestSpace != null)
        {
            bestCard.GetComponent<AICardMovement>().targetPosition = boardManager.boardSpaces[bestSpace[0], bestSpace[1]].transform;
        }
    }

    private List<int[]> FindValidSpaces()
    {   
        List<int[]> validSpaces = new List<int[]>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (boardManager.boardSpaces[j, i].OccupyingCard == null)
                {
                    validSpaces.Add(new int[] { j, i });
                }
            }
        }
        return validSpaces;
    }

    int CalculateChanges(Card card, int x, int y)
    {
        int changes = 0;
        BoardSpaceManager space = boardManager.boardSpaces[x, y];
        foreach (BoardSpaceManager adjacentSpace in boardManager.GetAdjacentSpaces(space))
        {
            if (adjacentSpace != null)
            {
                if (adjacentSpace.OccupyingCard != null)
                {
                    if (adjacentSpace.X == space.X) // Same column
                    {
                        if (adjacentSpace.Y < space.Y) // Above
                        {
                            if (card.Top > adjacentSpace.OccupyingCard.Bottom)
                            {
                                changes++;
                            }
                        }
                        else // Below
                        {
                            if (card.Bottom > adjacentSpace.OccupyingCard.Top)
                            {
                                changes++;
                            }
                        }
                    }
                    else // Same row
                    {
                        if (adjacentSpace.X > space.X) // Right
                        {
                            if (card.Right > adjacentSpace.OccupyingCard.Left)
                            {
                                changes++;
                            }
                        }
                        else // Left
                        {
                            if (card.Left > adjacentSpace.OccupyingCard.Right)
                            {
                                changes++;
                            }
                        }
                    }
                }
            }
        }
        return changes;
    }
    float CalculateWeighting(Card card, int x, int y)
    {
        float weightingAttack = 0;
        float weightingDefendTotal = 0;
        int weightingDefendNumber = 0;
        BoardSpaceManager space = boardManager.boardSpaces[x, y];
        foreach (BoardSpaceManager adjacentSpace in boardManager.GetAdjacentSpaces(space))
        {
            if (adjacentSpace != null)
            {
                if (adjacentSpace.X == space.X) // Same column
                {
                    if (adjacentSpace.Y < space.Y) // Above
                    {
                        if (adjacentSpace.OccupyingCard != null && card.Top > adjacentSpace.OccupyingCard.Bottom)
                        {
                            weightingAttack++;
                        }
                        else if (adjacentSpace.OccupyingCard == null)
                        {
                            weightingDefendTotal += card.Top - 9f;
                            weightingDefendNumber++;
                        }
                    }
                    else // Below
                    {
                        if (adjacentSpace.OccupyingCard != null && card.Bottom > adjacentSpace.OccupyingCard.Top)
                        {
                            weightingAttack++;
                        }
                        else if (adjacentSpace.OccupyingCard == null)
                        {
                            weightingDefendTotal += card.Bottom - 9f;
                            weightingDefendNumber++;
                        }
                    }
                }
                else // Same row
                {
                    if (adjacentSpace.X > space.X) // Right
                    {
                        if (adjacentSpace.OccupyingCard != null && card.Right > adjacentSpace.OccupyingCard.Left)
                        {
                            weightingAttack++;
                        }
                        else if (adjacentSpace.OccupyingCard == null)
                        {
                            weightingDefendTotal += card.Right - 9f;
                            weightingDefendNumber++;
                        }
                    }
                    else // Left
                    {
                        if (adjacentSpace.OccupyingCard != null && card.Left > adjacentSpace.OccupyingCard.Right)
                        {
                            weightingAttack++;
                        }
                        else if (adjacentSpace.OccupyingCard == null)
                        {
                            weightingDefendTotal += card.Left - 9f;
                            weightingDefendNumber++;
                        }
                    }
                }
            }
        }
        float weightingDefend = weightingDefendTotal / (weightingDefendNumber == 0 ? 1 : weightingDefendNumber);
        float weighting = weightingAttack + weightingDefend*(defenseWeighting/10f);
        return weighting;
    }
}
