using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
	public string suit;
	public int rank;
	public Color color = Color.black;
	public string colS = "Black";

	public List<GameObject> decoGOs = new List<GameObject>();

	public List<GameObject> pipGOs = new List<GameObject>();

	public GameObject back; 
	public CardDefinition def; 

	public SpriteRenderer[] spriteRenderers;

	void Start()
	{
		SetSortOrder(0);
	}

	public bool faceUp
	{
		get
		{
			return (!back.activeSelf);
		}
		set
		{
			back.SetActive(!value);
		}
	}


	public void PopulateSpriteRenderers()
	{
		if (spriteRenderers == null || spriteRenderers.Length == 0)
		{
			spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		}
	}

	public void SetSortingLayerName(string tSLN)
	{
		PopulateSpriteRenderers();

		foreach (SpriteRenderer tSR in spriteRenderers)
		{
			tSR.sortingLayerName = tSLN;
		}
	}

	public void SetSortOrder(int sOrd)
	{
		PopulateSpriteRenderers();

		foreach (SpriteRenderer tSR in spriteRenderers)
		{
			if (tSR.gameObject == this.gameObject)
			{
				tSR.sortingOrder = sOrd;
				continue;
			}
			switch (tSR.gameObject.name)
			{
				case "back":
					tSR.sortingOrder = sOrd + 2;
					break;
				case "face":
				default:
					tSR.sortingOrder = sOrd + 1;
					break;
			}
		}
	}

	//Virtual methods can be overriden by subclass methods with the same name
	virtual public void OnMouseUpAsButton()
	{
		//print(name);
	}
}

[System.Serializable]
public class Decorator
{
	//This class stores information about each decorator or pop from DeckXML
	public string type; //For card pips, type = "pip"
	public Vector3 loc; //The location of the Sprite on the card
	public bool flip = false; //Whether to flip the Sprite vertically
	public float scale = 1f; //The scale of the sprite
}

[System.Serializable]
public class CardDefinition
{
	//This class stores information for each rank of card.
	public string face; //Sprite to use for each face card
	public int rank; //The rank (1-13) of this card

	//Pips used. Because decorators (from the XML) are used the same way on every card in the deck, 
	//pips only stores information about the pips on numbered cards
	public List<Decorator> pips = new List<Decorator>();
}