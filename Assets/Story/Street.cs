
// The corridor room at the home of our hero.
public class Street : StoryEntity {

	public void Enter() {
		Default();
	}

	void LeaveToCorridor() {
		W.homeCorridor.EnterFromStreet();
	}

	void LeaveToRiver() {
		W.riverside.Enter();
	}

	void Default() {
		Narrate("street");
		Choose(
			Opt(LeaveToCorridor, "Enter apartment block"),
			Opt(LeaveToRiver, "Go to the river"));
	}

}
