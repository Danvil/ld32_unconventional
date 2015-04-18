using UnityEngine;
using System.Collections;

// The kitchen at the home of our hero.
public class HomeKitchen : StoryEntity {

	bool lightSwitchedOn = false;

	public void EnterFromBedroom() {
		InRoom();
	}

	public void EnterFromCorridor() {
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
		Narrate("You stand in a window-less dark room. The lack of proper lighting makes it hard to see or do anything useful.");
		Choose(
			new Choice(FumbleForDoor, "Fumble your way towards the door."),
			new Choice(() => SwitchLight(true), "Try to find the light switch.")
		);
	}

	public void InRoomLight() {
		Narrate("You stand in your small kitchen.");
		Choose(
			new Choice(() => SwitchLight(false), "Switch off the light."),
			new Choice(LeaveToBedroom, "Leave for your bedroom."),
			new Choice(LeaveToCorridor, "Leave for the corridor."));
	}

	public void SwitchLight(bool value) {
		lightSwitchedOn = value;
		if(lightSwitchedOn) {
			Narrate("After some unsuccessful attempts you finally find the light switch. There is a sudden cracking sound, and the light goes on with a hum.", InRoom);
		} else {
			Narrate("You switch of the light and suddenly the roome goes dark. An eeri quietness surrounds you after the humming of the light machine has stopped. After a couple of moments your eyes have adjusted to the darkness.", InRoom);
		}
	}

	public void FumbleForDoor() {
		Narrate("You are a bit desorientated in the darkness and fumble for a door.",
			() => {
				if(UnityEngine.Random.Range(0, 2) == 0) {
					LeaveToBedroom();
				} else {
					LeaveToCorridor();
				}
			});
	}

	public void LeaveToBedroom() {
		World.S.homeBedroom.EnterDoor();
	}

	public void LeaveToCorridor() {
		new TheEnd();
	}
}
