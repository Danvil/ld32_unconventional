﻿// The whole world and related functionality like time of day
public class World {

	public static World Singleton;

	public HomeBedroom homeBedroom = new HomeBedroom();
	public HomeKitchen homeKitchen = new HomeKitchen();
	public HomeCorridor homeCorridor = new HomeCorridor();
	public Street street = new Street();
	public Riverside riverside = new Riverside();

	public int day = 1;
	public int timeOfDay = 6;

	public bool IsFirstLight() {
		return timeOfDay == 6;
	}

	public bool IsTime(int d, int h) {
		return day == d && timeOfDay == h;
	}

	public bool raidHappend = false;
	public bool peaceKeeperBeatup = false;
}
