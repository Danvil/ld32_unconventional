using System;

// A radio spreading the word.
public class Radio : StoryEntity {

	int state = 0;

	Action radioOffAction;

	public Radio(Action radioOffAction) {
		this.radioOffAction = radioOffAction;
		state = 0;
		Play();
	}

	void Play() {
		int daysLeft = 5 - W.day;
		string message = "";
		if(daysLeft == 0) {
			message = "Today is the Day of Mourning. TODO";
		} else {
			message = string.Format("Dear brother, dear sister, there are {0} days left until the Day of Mourning. We shall all work feverishly to fullfill our assignments. By contributing your gifts to the community you demonstrate the generosity and kindness of your soul. Your fellow citizens are counting on you.", NumberWord(daysLeft));
		}
		string[] text = new string[] {
			"GOOD MORNING, CITIZEN! Rise and shine for a new day under the light of our beloved father Kester McIver. It is 6:17 and the sun is rising. Remember, today the sun will set at 19:23. Use daylight as efficient as possible to avoid unnecessary energy consumption!",
			Multiline(new string[] { "Please listen to an official announcement from our beloved father.", message}),
			"And now ... the weather forecast! The sky is partly clouded, but most of the day will be sunny. Temperatures are around 15-25 degrees. There might be light rain towards the early evening, but most likely it will stay dry. Enjoy the formidable spring weather!",
			"More radio",
			"Event more radio"
		};
		if(state == 0 && !W.IsFirstLight()) {
			state = 1;
		}
		Talk(text[state]);
		state = (state + 1) % text.Length;
		Choose(
			Opt(Play, "Keep listening"),
			Opt(radioOffAction, "Turn the radio off")
		);
	}
}
