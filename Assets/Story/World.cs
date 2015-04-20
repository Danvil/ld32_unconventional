using System.Collections.Generic;

// The whole world and related functionality like time of day
public class World {

	public static World Singleton;

	public HomeBedroom homeBedroom = new HomeBedroom();
	public HomeKitchen homeKitchen = new HomeKitchen();
	public HomeCorridor homeCorridor = new HomeCorridor();
	public Street street = new Street();
	public Riverside riverside = new Riverside();
	public Market market = new Market();
	public KesterManor kesterManor = new KesterManor();
	public DayOfMourning dayOfMourning = new DayOfMourning();

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
	public bool elaineMourning = false;
	public bool smelledTheBacon = false;
	public bool refusedElaine = false;
	public bool agreedToPhoto = false;

	public int bellaRumor = 0;
	public bool learnedAboutAdam = false;
	public bool discoveredHoleInWall = false;

	public bool sneakFail = false;
	public int numberOfPhotos = 3;

	public bool photofail = false;

	public bool hasCrateOpenPhoto = false;
	public bool hasCrateClosePhoto = false;
	public bool hasBarnSpotPhoto = false;
	public bool hasBarnPenPhoto = false;
	public bool hasMarnorPhoto = false;
	public bool hasSexPhoto = false;
	public List<int> manorWindowPhoto = new List<int>();

	public int NumerUsefulPhotos() {
		int n = 0;
		if(hasCrateOpenPhoto) n++;
		if(hasBarnPenPhoto) n++;
		if(hasSexPhoto) n++;
		return n;
	}
}
