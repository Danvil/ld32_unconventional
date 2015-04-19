
// The corridor room at the home of our hero.
public class Riverside : StoryEntity {

	public void Enter() {
		Default();
	}

	void LeaveToStreet() {
		W.street.Enter();
	}

	void Default() {
		Narrate("You stand at the riverbank of a great river. The water looks murky and brown and emits an uncomfortable rotten smell. Debris and pieces of wood and plastic are swimming on the water. Fifty meters towards South the buildings of " + kCityName + " begin. The free space between them and the river is covered in tall, yellowish grass.");
		Choose(Opt(LeaveToStreet, "Go to street"));
	}

}
