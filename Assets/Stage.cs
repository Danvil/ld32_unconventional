﻿using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System;

// The stage controls displayed text blocks like narrative text and choices.
public class Stage : MonoBehaviour {

	static Stage S;

	public static Stage Singleton { get { return S; } }

	public GameObject pfNarrative;
	public GameObject pfOption;
	public Scrollbar scrollbar;

	const float kNarrativeOffset = 6.0f;
	const float kOptionOffset = 4.0f;
	const float kOptionOffsetLast = 15.0f;

	const float kWidth = 410;
	const float kHeight = 590;

	RectTransform rectTransform;
	int scrollbarDirty = 0;

	// All text blocks on the stage (newest first)
	List<GameObject> textBlocks = new List<GameObject>();

	Button[] oldButtons = new Button[0];

	void Awake() {
		S = this;
		rectTransform = GetComponent<RectTransform>();
	}

	void Start() {
	}
	
	void Update() {
		// For some reasong we can not set the scrollbar value immediately.
		// So we wait for two updates until we do it.
		// This is ugly and should be improved.
		if(scrollbarDirty > 0) {
			scrollbarDirty--;
			if(scrollbarDirty == 0) {
				scrollbar.value = 0;
			}
		}
	}

	// Displayes a narrative text block
	public void AddNarrative(string text) {
		DeactivateOldChoices();
		GameObject go = (GameObject)Instantiate(pfNarrative);
		AddTextBlock(go, text, kNarrativeOffset);
	}

	// Displayes a bunch of choices which can be selected.
	// If a choice is selected the corresponding action will be executed.
	public void AddChoices(Choice[] choices) {
		DeactivateOldChoices();
		// Create new options and save created buttons.
		oldButtons = new Button[choices.Length];
		for(int i=0; i<choices.Length; i++) {
			GameObject go = (GameObject)Instantiate(pfOption);
			float len = (i + 1 == choices.Length ? kOptionOffsetLast : kOptionOffset);
			string prefix = string.Format("<b>{0}: </b>", i + 1);
			AddTextBlock(go, prefix + choices[i].text, len);
			Button button = go.GetComponent<Button>();
			Action act = choices[i].action;
			button.onClick.AddListener(() => act());
			oldButtons[i] = button;
		}
	}

	void DeactivateOldChoices() {
		// Delete old buttons from option text.
		for(int i=0; i<oldButtons.Length; i++) {
			Destroy(oldButtons[i]);
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
		rectTransform.sizeDelta = new Vector2(kWidth, newHeight);
		scrollbar.size = kHeight / newHeight;
		scrollbarDirty = 2;
	}
}
