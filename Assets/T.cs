using UnityEngine;
using System.Collections;

public static class T {

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

	static public string Observe(string text) {
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
