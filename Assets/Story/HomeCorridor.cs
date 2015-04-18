
// The corridor room at the home of our hero.
public class HomeCorridor : StoryEntity {

	public void EnterFromKitchen() {
		Default();
	}

	public void EnterFromOutside() {
		Default();
	}

	void Default() {
		Narrate("You find yourself in a long corridor.");
		Choose(
			new Choice(Default, "Go left."),
			new Choice(Default, "Go right.")
		);
	}
}
