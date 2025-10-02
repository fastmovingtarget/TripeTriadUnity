using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.Properties;

public class GameStateManager : MonoBehaviour
{
    public int Player1Score { get; private set; } = 5;
    public int Player2Score { get; private set; } = 5;

    public string PlayerTurnString;
    public string Player1ScoreString;
    public string Player2ScoreString;

    public string ButtonLabelString;
    public StyleEnum<DisplayStyle> ButtonEnabled;

    public GameObject Board;
    public GameObject selectionPanel;
    public GameObject PlayerHand;
    public GameObject OpponentHand;

    private List<Card> Player1Deck = new List<Card>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VisualElement root = FindFirstObjectByType<UIDocument>().rootVisualElement;
        root.dataSource = this;

        VisualElement turnLabel = root.Q<Label>("TurnLabel");
        turnLabel.SetBinding("text", new DataBinding
        {
            dataSourcePath = new PropertyPath(nameof(PlayerTurnString)),
            bindingMode = BindingMode.ToTarget,
        }); 
        root.Q<Label>("P2Score").SetBinding("text", new DataBinding
        {
            dataSourcePath = new PropertyPath(nameof(Player2ScoreString)),
            bindingMode = BindingMode.ToTarget,
        });
        root.Q<Label>("P1Score").SetBinding("text", new DataBinding
        {
            dataSourcePath = new PropertyPath(nameof(Player1ScoreString)),
            bindingMode = BindingMode.ToTarget,
        });
        Button startButton = root.Q<Button>("StartButton");
        startButton.RegisterCallback<ClickEvent>(evt => StartButtonCallback());
        startButton.SetBinding("text", new DataBinding
        {
            dataSourcePath = new PropertyPath(nameof(ButtonLabelString)),
            bindingMode = BindingMode.ToTarget,
        });
        startButton.SetBinding("style.display", new DataBinding
        {
            dataSourcePath = new PropertyPath(nameof(ButtonEnabled)),
            bindingMode = BindingMode.ToTarget,
        });
        ButtonEnabled = DisplayStyle.Flex;
        ButtonLabelString = "Select Cards";

        Board.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonCallback()
    {
        if (ButtonLabelString.Contains("Select"))
        {
            InitialiseSelectionStage();
        }
        else if (ButtonLabelString == "Start Game")
        {
            InitialiseGame();
        }
        else if (ButtonLabelString == "Start New Game")
        {
            InitialiseSelectionStage();
        }
    }

    public void CardSelected(Card card)
    {
        Player1Deck.Add(card);
        if (Player1Deck.Count == 5)
        {
            ButtonLabelString = "Start Game";
        }
        else { 
            ButtonLabelString = $"{5 - Player1Deck.Count} more card{(Player1Deck.Count < 4 ? "s" : "" )}";
        }
    }
    public void CardDeselected(Card card)
    {
        Player1Deck.Remove(card);
        ButtonLabelString = $"{5 - Player1Deck.Count} more card{(Player1Deck.Count < 4 ? "s" : "")}";
    }

    void InitialiseSelectionStage()
    {
        selectionPanel.SetActive(true);
        Player1Deck.Clear();
        Board.SetActive(false);
        Board.GetComponent<BoardManager>().ResetBoard();

        ResetPlayerHands();

        PlayerTurnString = "";
        Player1Score = 5;
        Player1ScoreString = "";
        Player2Score = 5;
        Player2ScoreString = "";

        selectionPanel.GetComponent<SelectionPanelManager>().InitialiseSelectionPanel();
        ButtonLabelString = $"{5 - Player1Deck.Count} more card{(Player1Deck.Count < 4 ? "s" : "")}";
    }

    void InitialiseGame()
    {
        selectionPanel.SetActive(false);

        PlayerTurnString = "Player 1's Turn";
        Player1ScoreString = Player1Score.ToString();
        Player2ScoreString = Player2Score.ToString();

        InitialisePlayerHands();
        Board.SetActive(true);
        ButtonEnabled = DisplayStyle.None;
    }

    void InitialisePlayerHands()
    {
        List<Card> player2HandCards = new List<Card>();
        for (int i = 0; i < 5; i++)
        {
            player2HandCards.Add(new Card(PlayerID.Player2));
        }

        HandManager player1HandManager = PlayerHand.GetComponent<HandManager>();
        if (player1HandManager == null)
            player1HandManager = PlayerHand.AddComponent<HandManager>();

        player1HandManager.InitialisePlayerHand(PlayerID.Player1, false, Player1Deck);

        HandManager player2HandManager = OpponentHand.GetComponent<HandManager>();
        if (player2HandManager == null)
            player2HandManager = OpponentHand.AddComponent<HandManager>();

        player2HandManager.InitialisePlayerHand(PlayerID.Player2, true, player2HandCards);
    }

    void ResetPlayerHands()
    {
        PlayerHand.GetComponent<HandManager>().ResetPlayerHand();
        OpponentHand.GetComponent<HandManager>().ResetPlayerHand();
    }


    public void PlayerTurnEnded(PlayerID player)
    {
        string nextPlayerTag = player == PlayerID.Player1 ? "Player2" : "Player1";
        if(player == PlayerID.Player1)
        {
            PlayerTurnString = "Player 2's Turn";
        }
        else
        {
            PlayerTurnString = "Player 1's Turn";
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).CompareTag(nextPlayerTag))
            {
                if(transform.GetChild(i).GetComponent<ComputerOpponentAI>() != null)
                {
                    transform.GetChild(i).gameObject.GetComponent<ComputerOpponentAI>().TakeTurn();
                }
                else
                {
                    transform.GetChild(i).gameObject.transform.BroadcastMessage("EnableTurn");
                }

                break;
            }
        }
    }
    public void EndGame(PlayerID player)
    {
        PlayerTurnString = Player1Score > Player2Score ? "Player 1 Wins" : Player2Score > Player1Score ? "Player 2 Wins" : "No one wins, it's a tie";
        ButtonLabelString = "Start New Game";
        ButtonEnabled = DisplayStyle.Flex;
    }
    public void AddScore(PlayerID player)
    {
        if (player == PlayerID.Player1) 
        {
            Player1Score++;
            Player2Score--;
            Player1ScoreString = Player1Score.ToString();
            Player2ScoreString = Player2Score.ToString();
        }
        else
        {
            Player1Score--;
            Player2Score++; 
            Player1ScoreString = Player1Score.ToString();
            Player2ScoreString = Player2Score.ToString();
        }
    }
}

public class Card : IEquatable<Card>
{
    public int Top { get; set; }
    public int Right { get; set; }
    public int Bottom { get; set; }
    public int Left { get; set; }

    public PlayerID CurrentOwner { get; set; }

    public Card(PlayerID player, int top, int right, int bottom, int left)
    {
        Top = top;
        Right = right;
        Bottom = bottom;
        Left = left;
        CurrentOwner = player;
    }

    public Card( PlayerID player )
    {
        CurrentOwner = player;
        Top = UnityEngine.Random.Range(1, 10);
        Right = UnityEngine.Random.Range(1, 10);
        Bottom = UnityEngine.Random.Range(1, 10);
        Left = UnityEngine.Random.Range(1, 10);
    }

    public Card( PlayerID player, int total)
    {
        CurrentOwner = player;
    }
    public Card(PlayerID player, int maxTotal, int minTotal)
    {
        CurrentOwner = player;
    }

    public override string ToString()
    {
        return $"Top: {Top}, Right: {Right}, Bottom: {Bottom}, Left: {Left}, Owner: {CurrentOwner}";
    }

    public bool Equals(Card otherCard)
    {

        return Top == otherCard.Top &&
                Right == otherCard.Right &&
                Bottom == otherCard.Bottom &&
                Left == otherCard.Left &&
                CurrentOwner == otherCard.CurrentOwner;
    }
}

public enum PlayerID
{
    Player1,
    Player2,
    None
}

public enum CurrentStage
{
    Selection,
    InGame,
    GameOver
}
