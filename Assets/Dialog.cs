using System;
using System.Linq;
using System.Collections.Generic;

public class Answer {
	public Func<bool> condition = null;
	public string answer = "";
	public string nextTag = "";
	public Action action = null;

	public Answer(string nextTag) {
		this.nextTag = nextTag;
	}

	public Answer(Action action) {
		this.action = action;
	}

	public Answer(string answer, string nextTag) {
		this.answer = answer;
		this.nextTag = nextTag;
	}

	public Answer(string answer, string nextTag, Action action) {
		this.answer = answer;
		this.nextTag = nextTag;
		this.action = action;
	}

	public Answer(string answer, Action action) {
		this.answer = answer;
		this.action = action;
	}

	public Answer(Func<bool> condition, string nextTag) {
		this.condition = condition;
		this.nextTag = nextTag;
	}

	public Answer(Func<bool> condition, string answer, string nextTag, Action action) {
		this.condition = condition;
		this.answer = answer;
		this.nextTag = nextTag;
		this.action = action;
	}

	public Answer(Func<bool> condition, string answer, string nextTag) {
		this.condition = condition;
		this.answer = answer;
		this.nextTag = nextTag;
	}
}

public class DialogLine {
	public string text;
	public Action action;
	public Answer[] answers;
}

public class Dialog : StoryEntity {

	Dictionary<string, DialogLine> lines;

	public Dialog() {
		this.lines = new Dictionary<string, DialogLine>();
	}

	public Dialog(Dictionary<string, DialogLine> lines) {
		this.lines = lines;
	}

	public void AddLine(string tag, string text, Action action, params Answer[] answers) {
		lines[tag] = new DialogLine() { text = text, answers = answers };
	}

	public void AddLine(string tag, string text, Action action, string nextTag) {
		lines[tag] = new DialogLine() { text = text, answers = new Answer[] { new Answer("", nextTag, action) } };
	}

	public void AddLine(string tag, string text, params Answer[] answers) {
		lines[tag] = new DialogLine() { text = text, answers = answers };
	}

	public void AddLine(string tag, string text, string nextTag) {
		lines[tag] = new DialogLine() { text = text, answers = new Answer[] { new Answer(nextTag) } };
	}

	public void AddLine(string tag, string text, Action action) {
		lines[tag] = new DialogLine() { text = text, answers = new Answer[] { new Answer(action) } };
	}

	public void AddLine(string tag, Action action) {
		lines[tag] = new DialogLine() { text = "", answers = new Answer[] { new Answer(action) } };
	}

	public void Play(string tag) {
		DialogLine line = lines[tag];
		System.Diagnostics.Debug.Assert(line != null, "Dialog logic error. Tag '" + tag + "' does not exist.");
		if(line.action != null) {
			line.action();
		}
		if(line.text != "") {
			Narrate(line.text);
		}
		var answers = line.answers.Where(x => x != null && (x.condition == null || x.condition())).ToArray();
		System.Diagnostics.Debug.Assert(answers.Length > 0, "Dialog logic error. Tag '" + tag + "' does not have any non-null answers.");
		if(line.text == "" && answers.Length == 1 && answers[0].answer == "") {
			var x = answers[0];
			if(x.action != null) {
				x.action();
			}
			if(x.nextTag != "") {
				Play(x.nextTag);
			}
		} else {
			Choose(answers
				.Select(x => new Choice(() => {
					if(x.action != null) {
						x.action();
					}
					if(x.nextTag != "") {
						Play(x.nextTag);
					}
				}, x.answer == "" ? "Continue" : x.answer))
				.ToArray());
		}
	}
	
}
