using System;
using System.Linq;

public class StoryEntity {

	public StoryEntity() {
		stage = Stage.Singleton;
	}

	Stage stage;

	public World W { get { return World.Singleton; } }

	static public T Rnd<T>(T[] u) {
		System.Diagnostics.Debug.Assert(u.Length > 0, "Need at least one element");
		return u[Rnd(u.Length)];
	}

	// Gives a random number in [0, n[
	static public int Rnd(int n) {
		return UnityEngine.Random.Range(0, n);
	}

	static public Choice Opt(Action action, string text) {
		return new Choice(action, text);
	}

	static public Choice Opt(bool cond, Action action, string text) {
		return cond ? Opt(action, text) : null;
	}

	// Displayes a narrative text block
	public void Narrate(string text) {
		stage.AddNarrative(text);
	}

	static public Choice Condition(bool cond, Choice choice) {
		return cond ? choice : null;
	}

	static public string Condition(bool cond, string text) {
		return cond ? text : "";
	}

	// Displayes a narrative text block and gives one choice to continue.
	// If the choice is selected the given action is executed.
	public void Narrate(string text, Action action) {
		Narrate(text);
		Choose(new Choice(action, "Continue"));
	}

	// Displayes a dialog text block
	public void Talk(string text) {
		stage.AddNarrative(Quote(text));
	}

	// Asks the player for a choice.
	public void Choose(params Choice[] choices) {
		choices = choices.Where(x => x != null && x.action != null).ToArray();
		for(int i=0; i<choices.Length; i++) {
			choices[i].text = Italics(choices[i].text);
		}
		stage.AddChoices(choices);
	}

	static public string Quote(string text) {
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
