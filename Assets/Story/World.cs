// The whole world and related functionality like time of day
public class World {

	public static World S = new World();

	public HomeBedroom homeBedroom = new HomeBedroom();
	public HomeKitchen homeKitchen = new HomeKitchen();
	public HomeCorridor homeCorridor = new HomeCorridor();

	public int day = 1;
	public int timeOfDay = 6;

	public bool IsFirstLight() {
		return timeOfDay == 6;
	}
}
