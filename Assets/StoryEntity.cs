using System;

public class StoryEntity {

	public StoryEntity() {
		stage = Stage.Singleton();
	}

	Stage stage;

	// Displayes a narrative text block
	public void Narrate(string text) {
		stage.AddNarrative(text);
	}

	// Displayes a narrative text block and gives one choice to continue.
	// If the choice is selected the given action is executed.
	public void Narrate(string text, Action action) {
		Narrate(text);
		Choose(new Choice(action, "Continue"));
	}

	// Displayes a dialog text block
	public void Talk(string text) {
		stage.AddNarrative(Quoted(text));
	}

	// Asks the player for a choice.
	public void Choose(params Choice[] choices) {
		for(int i=0; i<choices.Length; i++) {
			choices[i].text = Italics(choices[i].text);
		}
		stage.AddChoices(choices);
	}

	static public string Quoted(string text) {
		return "\u201C" + text + "\u201D";
	}

	static public string Multiline(string[] paragraphs) {
		string total = "";
		for(int i=0; i<paragraphs.Length; i++) {
			total += paragraphs[i];
			if(i + 1 != paragraphs.Length) {
				total += "\n";
			}
		}
		return total;
	}

	static public string Italics(string text) {
		return "<i>" + text + "</i>";
	}

	static public string NumberWord(int n) {
		if(n == 1) return "one";
		else if(n == 2) return "two";
		else if(n == 3) return "three";
		else if(n == 4) return "four";
		else return "TODO";
	}
}
