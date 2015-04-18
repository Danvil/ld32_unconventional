using UnityEngine;
using System.Collections;

// The whole world and related functionality like time of day
public class World {

	public static World S = new World();

	public HomeRoomSleep homeRoomSleep = new HomeRoomSleep();
	public HomeRoomKitchen homeRoomKitchen = new HomeRoomKitchen();

	public int day = 1;
	public int timeOfDay = 6;

	public bool IsFirstLight() {
		return timeOfDay == 6;
	}
}
