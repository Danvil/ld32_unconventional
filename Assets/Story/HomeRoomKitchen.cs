using UnityEngine;
using System.Collections;

// The kitchen at the home of our hero.
public class HomeRoomKitchen {

	bool lightSwitchedOn = false;

	public void EnterFromHomeRoomSleep() {
		InRoom();
	}

	public void InRoom() {
		if(lightSwitchedOn) {
			InRoomLight();
		} else {
			InRoomDark();
		}
	}

	public void InRoomDark() {
		Stage.S.AddNarrative(T.Observe("You stand in a window-less dark room. The lack of proper lighting makes it hard to see or do anything useful."));
		Stage.S.AddChoices(new Choice[] {
			new Choice(FumbleForDoor, "Fumble your way towards the door."),
			new Choice(() => SwitchLight(true), "Try to find the light switch.")
		});
	}

	public void InRoomLight() {
		Stage.S.AddNarrative(T.Observe("You stand in your small kitchen."));
		Stage.S.AddChoices(new Choice[] {
			new Choice(() => SwitchLight(false), "Switch off the light."),
			new Choice(LeaveHomeSleepRoom, "Leave for your sleeping room."),
			new Choice(LeaveCorridor, "Leave for the corridor.")
		});
	}

	public void SwitchLight(bool value) {
		lightSwitchedOn = value;
		if(lightSwitchedOn) {
			Stage.S.AddNarrative(T.Observe("After some unsuccessful attempts you finally find the light switch. There is a sudden cracking sound, and the light goes on with a hum."), InRoom);
		} else {
			Stage.S.AddNarrative(T.Observe("You switch of the light and suddenly the roome goes dark. An eeri quietness surrounds you after the humming of the light machine has stopped. After a couple of moments your eyes have adjusted to the darkness."), InRoom);
		}
	}

	public void FumbleForDoor() {
		Stage.S.AddNarrative(T.Observe("You are a bit desorientated in the darkness and fumble for a door."),
			() => {
				if(UnityEngine.Random.Range(0, 2) == 0) {
					LeaveHomeSleepRoom();
				} else {
					LeaveCorridor();
				}
			});
	}

	public void LeaveHomeSleepRoom() {
		World.S.homeRoomSleep.EnterDoor();
	}

	public void LeaveCorridor() {
		new TheEnd();
	}
}
