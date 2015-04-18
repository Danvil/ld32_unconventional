using UnityEngine;
using System.Collections;

public class TheBegin {

	public TheBegin() {
		new Radio(() => new HomeRoomSleep(HomeRoomSleep.Entry.Awake));
	}
}
