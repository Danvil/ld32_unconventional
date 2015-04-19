
// The corridor room at the home of our hero.
public class Riverside : StoryEntity {

	public void Enter() {
		Default();
	}

	void LeaveToStreet() {
		W.street.Enter();
	}

	void Default() {
		Narrate("riverside");
		Choose(Opt(LeaveToStreet, "Go to street"));
	}

}
