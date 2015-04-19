
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

	void LeaveToMarket() {
		W.market.Enter();
	}

	void Default() {
		Narrate("You stand on a dusty street surrounded by tall buildings. Some of them are partly collapsed and other, like your the house with your apartment, look like it will not take much longer until they suffer the same fate. In front of one of the buildings stands a PV from the peacekeepers. You see a few people walking on the street. Most of them seem to be on their way to or from the market at the western end of the street. Between the buildings small dark alleys are branching off towards the north and south. Through some of the alleys going North you can spot the great river.");
		Choose(
			Opt(LeaveToCorridor, "Enter apartment block"),
			Opt(LeaveToMarket, "Go to the market"),
			Opt(LeaveToRiver, "Go to the river"));
	}

}
