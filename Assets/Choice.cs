using System;
using System.Linq;

public struct WA {
	public WA(Action a, float w) {
		action = a;
		weight = w;
	}

	public Action action;
	public float weight;
}

public struct Choice {
	public Choice(Action a, string t) {
		action = a;
		text = t;
	}

	public Action action;
	public string text;

	public static Action Random(params WA[] options) {
		System.Diagnostics.Debug.Assert(options.Length > 0, "Need at least one option!");
		float total = options.Sum(x => x.weight);
		float now = UnityEngine.Random.Range(0.0f, total);
		int i;
		float val = 0.0f;
		for(i=0; i<options.Length; i++) {
			val += options[i].weight;
			if(val >= now) {
				break;
			}
		}
		System.Diagnostics.Debug.Assert(i < options.Length, "Logic error");
		return options[i].action;
	}
}

