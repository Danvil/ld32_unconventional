
// The corridor room at the home of our hero.
public class Street : StoryEntity {

	public void EnterFromCorridor() {
		Default();
	}

	void LeaveToCorridor() {
		World.S.homeCorridor.EnterFromStreet();
	}

	void Default() {
		Narrate("street");
		Choose(Opt(LeaveToCorridor, "Enter apartment block"));
	}

}
