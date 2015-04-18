using System;
using System.Linq;
using System.Collections.Generic;

public class Awnser {
	public string anwser;
	public string nextTag = "";
	public Action action = null;
	public Func<bool> condition = null;

	public Awnser(string anwser, string nextTag) {
		this.anwser = anwser;
		this.nextTag = nextTag;
	}

	public Awnser(string anwser, string nextTag, Action action) {
		this.anwser = anwser;
		this.nextTag = nextTag;
		this.action = action;
	}

	public Awnser(string anwser, Action action) {
		this.anwser = anwser;
		this.action = action;
	}

	public Awnser(string anwser, string nextTag, Action action, Func<bool> condition) {
		this.anwser = anwser;
		this.nextTag = nextTag;
		this.action = action;
		this.condition = condition;
	}

	public Awnser(string anwser, string nextTag, Func<bool> condition) {
		this.anwser = anwser;
		this.nextTag = nextTag;
		this.condition = condition;
	}
}

public class DialogLine {
	public string text;
	public Awnser[] awnsers;
}

public class Dialog : StoryEntity {

	Dictionary<string, DialogLine> lines = new Dictionary<string, DialogLine>();

	public void AddLine(string tag, string text, params Awnser[] awnsers) {
		lines[tag] = new DialogLine() { text = text, awnsers = awnsers };
	}

	public void Play(string tag) {
		DialogLine line = lines[tag];
		System.Diagnostics.Debug.Assert(line != null, "Dialog logic error. Tag '" + tag + "' does not exist.");
		Narrate(line.text);
		Choose(line.awnsers
			.Where(x => x != null && (x.condition == null || x.condition()))
			.Select(x => new Choice(() => {
				if(x.action != null) {
					x.action();
				}
				if(x.nextTag != "") {
					Play(x.nextTag);
				}
			}, x.anwser))
			.ToArray());
	}
	
}
