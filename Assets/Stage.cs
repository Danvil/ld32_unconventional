using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System;

public class Stage : MonoBehaviour {

	public GameObject pfNarrative;
	public GameObject pfOption;
	public Scrollbar scrollbar;

	const float kNarrativeOffset = 6.0f;
	const float kOptionOffset = 4.0f;
	const float kOptionOffsetLast = 15.0f;

	float position = 0;
	RectTransform rectTransform;
	int scrollbarDirty = 0;

	// All text blocks on the stage (newest first)
	List<GameObject> textBlocks = new List<GameObject>();

	Button[] oldButtons = new Button[0];

	void Awake() {
		rectTransform = GetComponent<RectTransform>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(scrollbarDirty > 0) {
			scrollbarDirty--;
			if(scrollbarDirty == 0) {
				scrollbar.value = 0;
			}
		}
	}

	public void AddNarrative(string narrative) {
		GameObject go = (GameObject)Instantiate(pfNarrative);
		AddTextBlock(go, narrative, kNarrativeOffset);
	}
	
	public void AddChoices(Action<int> onChoice, string[] options) {
		// Delete old buttons from option text.
		for(int i=0; i<oldButtons.Length; i++) {
			Destroy(oldButtons[i]);
		}
		// Create new options and save created buttons.
		oldButtons = new Button[options.Length];
		for(int i=0; i<options.Length; i++) {
			GameObject go = (GameObject)Instantiate(pfOption);
			float len = (i + 1 == options.Length ? kOptionOffsetLast : kOptionOffset);
			AddTextBlock(go, options[i], len);
			Button button = go.GetComponent<Button>();
			int copyi = i; // Need to create a copy otherwise a kind of reference is used in the event.
			button.onClick.AddListener(() => {
				onChoice(copyi);
			});
			oldButtons[i] = button;
		}
	}

	void PurgeTextBlocks() {
		// Unity3D supports only 65k vertices per canvas.
		const int kMaxCharacters = 10000;
		// This function deletes old text blocks to guarantee that not too much text is displayed.
		// First find out which text blocks we can keep.
		int num = 0;
		int firstDeleted = textBlocks.Count;
		for(int i=0; i<textBlocks.Count; i++) {
			num += textBlocks[i].GetComponent<Text>().text.Length;
			if(num > kMaxCharacters) {
				firstDeleted = i;
				break;
			}
		}
		// Delete remaining text blocks.
		for(int i=firstDeleted; i<textBlocks.Count; i++) {
			Destroy(textBlocks[i]);
		}
		textBlocks.RemoveRange(firstDeleted, textBlocks.Count - firstDeleted);
	}

	void AddTextBlock(GameObject go, string text, float offset) {
		PurgeTextBlocks();
		textBlocks.Insert(0, go);
		go.GetComponent<Text>().text = text;
		go.GetComponent<ContentSizeFitter>().SetLayoutVertical();
		RectTransform rt = go.GetComponent<RectTransform>();
		rt.SetParent(this.transform);
		float currentHeight = rectTransform.sizeDelta.y;
		rt.anchoredPosition = new Vector2(0, -currentHeight);
		float newHeight = currentHeight + rt.sizeDelta.y + offset;
		rectTransform.sizeDelta = new Vector2(380, newHeight);
		scrollbar.size = 580.0f / newHeight;
		scrollbarDirty = 2;
	}
}
