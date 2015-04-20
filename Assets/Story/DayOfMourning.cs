// The final word.
public class DayOfMourning : StoryEntity {

	public DayOfMourning() {
	}

	public void GameOver() {
		Narrate("THE END");
	}

	public void HasPhotos() {
		Narrate("You go to bed, setting up the radio so that it will wake you tomorrow morning. You have vivid dreams of a huge house with many rooms. There seems to be no exit and you walk from room to the next looking for something lost. The morning message from Kester awakes you from your sleep. When you walk down the street everyone is already pretty exited about the celebration. You push your way forward to stand near the tribune. A few meters away you see Elaine. She waves at you excited.", Tribune);
	}

	public void Sleeper() {
		Narrate("You go to bed, setting up the radio so that it will wake you tomorrow morning. You fall into a deep dreamless slumber and awake to the everyday morning message from Kester. When you walk down the street everyone is already pretty exited about the celebration. You join the celebration.", Sleeper2);
	}

	public void Sleeper2() {
		if(!W.agreedToPhoto) {
			Narrate("Elaine is nowhere to be seen. You feel a bit guilty for letting her down, but after all you just want to stay save. Angering the peacekeepers is not a good idea if you want to keep on living for another year.", TribuneNoPhotos);
		} else {
			Narrate("You see Elaine in the crowd and she comes over to you. " + Quote("Why did you not visit me yesterday? Do you have the photos?"));
			Choose(
				Opt(W.numberOfPhotos < 3, Tribune, "Hand her the photos"),
				Opt(ElaineDisappointed, Quote("Sorry, I was not able to get them."))
			);
		}
	}

	public void ElainePhotoCheck() {
		if(W.NumerUsefulPhotos() > 0) {
			Narrate("She looks at the photos. " + Quote("Splendid! I knew, I could count on you! Give them to me and we shall show everyone tomorrow at the celebration that Kester must be overturned!"), Tribune);
		} else {
			Narrate("She looks at the photos. " + Quote("What have you photographed? This was not a sightseeing trip. You should photograph something unmasking about Kester!"), ElaineDisappointed);
		}
	}

	public void ElaineDisappointed() {
		W.photofail = true;
		Narrate("Sadness and disappointment fill Elaines eyes. " + Quote("But, ... you promised to get them!") + " She turns around and runs away. ", TribuneNoPhotos);
	}

	public void Tribune() {
		Narrate("The ceremony seems to go on endlessly. At some point Kester himself appears closely guarded by a couple of peacekeepers. The community seems to be only mildly pleased by his appearance. He preaches of the gods and there rules. When he mentions how suffering on earth will give you a happy live thereafter, Elaine steps forward. She speaks with a loud strong voice: " + Quote("And why do these rules do not apply to you?") + " Everyone in the community turns there head towards her. " + Quote("Thanks to this helpful soul ") + ", she points towards you, " + Quote("We are able to see the sins happening behind the walls of Kester Manor.") + " She holds up the photographs. A murmur goes through the assembled community. People are closing in around you and Elaine and lean forward to look on the photographs.", Photos);
	}

	bool sawCratePhoto = false;
	bool sawBarnPenPhoto = false;
	bool sawSexPhoto = false;

	int shounter = 0;

	public void Photos() {
		string[] shouts = new string[] {
			" People are starting to shout and more are pressing forward to catch a look.",
			" Everyone is alert now. People are shouting " + Quote("Kester liar!") + " and " + Quote("Food! Food!"),
			" The citizen are storming the tribune and trying to get to Kester. You only hear one word in the air " + Quote("LIAR!")
		};
		if(W.hasCrateOpenPhoto && !sawCratePhoto) {
			Narrate("A skinny woman with a child on her side looks at the photo with the opened crates full of food. She gives a loud shreak " + Quote("So much food! Why does he not share the food with us?") + " Her voice is getting louder and she cries " + Quote("My children are starving! Give us the food!") + shouts[shounter], Photos);
			shounter++;
			sawCratePhoto = true;
		}
		else if(W.hasBarnPenPhoto && !sawBarnPenPhoto) {
			Narrate("A stout man with a gaunt face looks at the photo of the animals in the barn. " + Quote("Cattle! Kester has cattle!") + " His voice is loud and carries over the community! " + Quote("Kester is a lier! Our animals are not dead, he was hiding them from us!") + shouts[shounter], Photos);
			shounter++;
			sawBarnPenPhoto = true;
		}
		else if(W.hasSexPhoto && !sawSexPhoto) {
			Narrate("Someone looks at the revealing photo with Kester and the naked women. " + Quote("Look at that pig! He forbids us everything, and what is he doing?") + " He shouts loud so that everyone can here him, " + Quote("Kester is a liar!") + shouts[shounter], Photos);
			shounter++;
			sawSexPhoto = true;
		}
		else {
			PhotoEnd();
		}
	}

	public void PhotoEnd() {
		Narrate("The community is in turmoil. Kester has been unmasked. The captain of the peacekeepers commands a few guards to follow him and pushes through the crowed. They approach you and try to get the photos. One of the peacekeepers snaps a photo out of the hand of a citizen. He looks at it and stops. " + Quote("I did not knew...") + "He takes of his helmet and hands the photo to the guard next to him.", GameOver);
	}

	public void TribuneNoPhotos() {
		Narrate("The ceremony seems to go on endlessly. At some point Kester himself appears closely guarded by a couple of peacekeepers. He preaches of the gods above and how suffering on earth will give you a happy live thereafter in heaven. The community seems to be mildly pleased by his word. At some point the peacekeepers bring a couple of small baskets with small food items. They order everyone to form a queue and receive a token of devotedness from father Kester himself. In the evening you walk back home, exhausted and hungry. You go to bed and hope for a better day tomorrow.", GameOver);
	}

	public void Shot() {
		Narrate("Slowly your consciousness fades a way. You have a sense of endless falling which fades into nothingness.", GameOver);
	}

	public void Captured() {
		Narrate("You are woken up with a splash of cold water in your face. Your head hurts even more than usual and slowly you remember what happened. You are tied in a sitting position in a completely dark room smelling of dampness and urine. After an almost endless time, someone opens the door abruptly and blinds you with a bright flash of light. Before you realize what is happening someone pulls a piece of cloth over your head and drags you outside. They pushing you hard onto a metal surface and a motor is started. After a short ride the vehicle comes to a stop. You can hear shouting and cheering outside. Suddenly a door is opened and the cheering becomes much louder. Someone comes, drags you out and pushes you on your knees. A powerful preaching voice intones: " + Quote("Here the sinner kneels before his god! He is a sign of our impurity and shall be cleansed from all evil. Execute him!") + " You are in complete shock and before you can react, a gunshot and sharp pain in your head.", Shot);
	}
}
