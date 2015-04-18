using UnityEngine;
using System.Collections;

// The beginning of the adventure.
public class TheBegin {

	public TheBegin() {
		new Radio(() => new HomeRoomSleep(HomeRoomSleep.Entry.Awake));
	}
}
