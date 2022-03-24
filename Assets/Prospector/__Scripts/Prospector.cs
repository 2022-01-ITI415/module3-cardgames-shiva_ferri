using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Prospector : MonoBehaviour {

	static public Prospector 	S;

	[Header("Set in Inspector")]
	public TextAsset			deckXML;
	public TextAsset layoutXML;
	public float xOffset = 3;
	public float yOffset = -2.5f;
	public Vector3 layoutCenter;


	[Header("Set Dynamically")]
	public Deck					deck;
	public Layout layout;
	public List<CardProspector> drawPile;
	public Transform layoutAnchor;
	public CardProspector target;
	public List<CardProspector> tableau;
	public List<CardProspector> discardPile;
	void Awake(){
		S = this;
	}

	void Start()
	{
		deck = GetComponent<Deck>(); // Get the Deck
		deck.InitDeck(deckXML.text); // Pass DeckXML to it
		Deck.Shuffle(ref deck.cards); // This shuffles the deck

		layout = GetComponent<Layout>();
		layout.ReadLayout(layoutXML.text);
		drawPile = ConvertListCardsToListCardProspectors(deck.cards);
		LayoutGame();
	}

	List<CardProspector> ConvertListCardsToListCardProspectors(List
<Card> lCD)
	{
		List<CardProspector> lCP = new List<CardProspector>();
		CardProspector tCP;
		foreach (Card tCD in lCD)
		{
			tCP = tCD as CardProspector; // a
			lCP.Add(tCP);

		}
		return (lCP);
	}

	CardProspector Draw()
	{
		CardProspector cd = drawPile[0]; // Pull the 0th CardProspe
	drawPile.RemoveAt(0); // Then remove it from Lis
		
return (cd); // And return it
	}
	// LayoutGame() positions the initial tableau of cards, a.k.a.
void LayoutGame()
	{
		// Create an empty GameObject to serve as an anchor for the
	if (layoutAnchor == null)
		{GameObject tGO = new GameObject("_LayoutAnchor");
			// ^ Create an empty GameObject named _LayoutAnchor in
	layoutAnchor = tGO.transform; // Grab its
	layoutAnchor.transform.position = layoutCenter; // Po
}
		CardProspector cp;
		// Follow the layout
		foreach (SlotDef tSD in layout.slotDefs)
		{
			// ^ Iterate through all the SlotDefs in the layout.slotD
			
		cp = Draw(); // Pull a card from the top (beginning) of t
		
		cp.faceUp = tSD.faceUp; // Set its faceUp to the value i
		
		cp.transform.parent = layoutAnchor; // Make its parent la
			// This replaces the previous parent: deck.deckAnchor, wh
		
		// appears as _Deck in the Hierarchy when the scene is p
		
		cp.transform.localPosition = new Vector3(
		layout.multiplier.x * tSD.x,
		layout.multiplier.y * tSD.y,
		-tSD.layerID);
			// ^ Set the localPosition of the card based on slotDef
			cp.layoutID = tSD.id;
			cp.slotDef = tSD;
			// CardProspectors in the tableau have the state CardStat
		cp.state = eCardState.tableau;
		tableau.Add(cp); // Add this CardProspector to the List<>
		}
	}
}

