using UnityEngine;
using System.Collections.Generic;

public class VisualsBehaviour : MonoBehaviour
{
    public static List<Sprite> numberSprites = new List<Sprite>();
    public static Dictionary<PlayerID, Color> PlayerColours = new Dictionary<PlayerID, Color>()
    {
        { PlayerID.Player1, Color.lightBlue },
        { PlayerID.Player2, Color.lightSalmon },
        { PlayerID.None, Color.white   }
    };
    public static Dictionary<PlayerID, Color> SelectedColours = new Dictionary<PlayerID, Color>()
    {
        { PlayerID.Player1, Color.lightCyan },
        { PlayerID.Player2, Color.lightPink }
    };

    void Awake()
    {
        numberSprites.Add(Resources.Load<Sprite>("digit0"));
        numberSprites.Add(Resources.Load<Sprite>("digit1"));
        numberSprites.Add(Resources.Load<Sprite>("digit2"));
        numberSprites.Add(Resources.Load<Sprite>("digit3"));
        numberSprites.Add(Resources.Load<Sprite>("digit4"));
        numberSprites.Add(Resources.Load<Sprite>("digit5"));
        numberSprites.Add(Resources.Load<Sprite>("digit6"));
        numberSprites.Add(Resources.Load<Sprite>("digit7"));
        numberSprites.Add(Resources.Load<Sprite>("digit8"));
        numberSprites.Add(Resources.Load<Sprite>("digit9"));
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
