using UnityEngine;
using System.Collections;

// The bedroom at the home of our hero.
public class HomeBedroom : StoryEntity {

	int hasSlept = 0;

	public void WakeUp() {
		Awake();
	}

	public void EnterDoor() {
		InRoom();
	}

	void Awake() {
		hasSlept = 2;
		string bacon = "";
		if(W.day == 1 && W.IsFirstLight()) {
			bacon += " There is a faint smell of burned meat in the air. Some of the neighbours seem to have an illegal breakfast. Hopefully the peacekeepers will not notice.";
		}
		Narrate("Slowly waking up, you try to focus on your surroundings. A single sunray is shining through the wooden boards which are nailed against the window. Small dust particles are dancing in the air." + bacon, InBed);
	}

	void InBed() {
		Narrate("You lie in the bed in a small dim room. Next to your bed stands a old, scrubby radio on the ground.");
		Choose(
			Opt(StandUp, "Stand up"),
			Opt(SwitchOnRadio, "Switch on the radio"),
			Opt(StayInBed, "Stay in bed"),
			Opt(W.refusedElaine || W.agreedToPhoto, W.dayOfMourning.Sleeper, "Sleep until the next day")
		);
	}

	void StandUp() {
		if(hasSlept == 0) {
			InRoom();
		}
		else if(hasSlept == 1) {
			Narrate("Wearily you turn around to stand up. Why can live not take place right here in that bed?", InRoom);
		} else {
			Narrate("You turn around to stand up. A sudden pain flashes through your back. You remember the small accident you had yesterday while climbing that wall. There was a promising fruit tree on the other side. You were sure to have a good grip, but one moment later you lay on the ground with a throbbing pain in your shoulder and elbow.", InRoom);
		}
	}

	void InRoom() {
		hasSlept = 0;
		Narrate("You stand in a small dim room. In one corner is your bed. There is a window which is secured and barred with a couple of wooden planks. Opposite to the window is a heavy wooden door.");
		Choose(
			Opt(Leave, "Open the door and walk out of the room."),
			Opt(InBed, "Lie down in bed."));
	}

	void Leave() {
		W.homeKitchen.EnterFromBedroom();
	}

	void SwitchOnRadio() {
		new Radio(InBed);
	}

	void StayInBed() {
		W.timeOfDay++;
		hasSlept = 1;
		if(!W.raidHappend) {
			W.elaineMourning = true;
			W.raidHappend = true;
		}
		Narrate("You turn around and cuddle in your blanket. You lie there pondering about the meaning of live and wish you could stay in bed the whole day. Your eye lids become heavy, and you fall asleep for an hour.", InBed);
	}
}
