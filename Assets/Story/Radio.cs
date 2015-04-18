using System;

// A radio spreading the word.
public class Radio {

	int state = 0;

	Action radioOffAction;

	public Radio(Action radioOffAction) {
		this.radioOffAction = radioOffAction;
		state = World.S.IsFirstLight() ? 0 : 1;
		Choice(0);
	}

	void Choice(int i) {
		int daysLeft = 5 - World.S.day;
		string message = "";
		if(daysLeft == 0) {
			message = T.Quoted("Today is the Day of Mourning. TODO");
		} else {
			message = T.Quoted(string.Format("Dear brother, dear sister, there are {0} days left until the Day of Mourning. We shall all work feverishly to fullfill our assignments. By contributing your gifts to the community you demonstrate the generosity and kindness of your soul. Your fellow citizens are counting on you.", T.NumberWord(daysLeft)));
		}
		string[] text = new string[] {
			T.Quoted("GOOD MORNING, CITIZEN! Rise and shine for a new day under the light of our beloved father Kester McIver. It is 6:17 and the sun is rising. Remember, today the sun will set at 19:23. Use daylight as efficient as possible to avoid unnecessary energy consumption!"),
			T.Multiline(new string[] { T.Quoted("Please listen to an official announcement from our beloved father."), message}),
			T.Quoted("And now ... the weather forecast! The sky is partly clouded, but most of the day will be sunny. Temperatures are around 15-25 degrees. There might be light rain towards the early evening, but most likely it will stay dry. Enjoy the formidable spring weather!"),
			T.Quoted("More radio"),
			T.Quoted("Event more radio")
		};
		string[] choice = new string[] {
			"Keep listening",
			"Turn the radio off"
		};
		if(i == 0) {
			Stage.S.AddNarrative(text[state]);
			state ++;
			if(state >= text.Length) {
				state = 1; // Skip first message
			}
			Stage.S.AddChoices(x => Choice(x), choice);
		} else {
			radioOffAction();
		}
	}
}
