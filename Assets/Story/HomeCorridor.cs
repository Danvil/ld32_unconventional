
// The corridor room at the home of our hero.
public class HomeCorridor : StoryEntity {

	int level = 2;

	public void EnterFromApartment() {
		level = 2;
		Default();
	}

	public void EnterFromStreet() {
		level = 1;
		Default();
	}

	public void EnterFromRoof() {
		level = 3;
		Staircase();
	}

	void Default() {
		string baconText = " A smell of burnt meat hangs in the air.";
		bool isBacon = level == 2 && World.S.day == 1 && World.S.IsFirstLight();
		Narrate(
			"You find yourself in a long corridor. The ceiling is quite low and on each side there is a perfectly regular sequence of identically looking doors. The atmosphere is rather depressing."
			+ Condition(level == 1, " On one end of the corridor there is a staircase. At the other end there is a door leading outside.")
			+ Condition(level > 1, " On one end of the corridor there is a staircase, the other direction is just a dead end.")
			+ Condition(isBacon, baconText));
		Choose(
			Opt(Staircase, "Go to the staircase"),
			Opt(Neighbor, "Knock on a random door"),
			Opt(level == 2, LeaveToApartment, "Enter your apartment"),
			Opt(level == 1, LeaveToStreet, "Leave the building"),
			Opt(isBacon, NeighborBacon, "Find the door which smells like bacon")
		);
	}

	void Neighbor() {
		Narrate("You knock on a random door in the corridor.",
			Choice.Random(NeighborResult1, NeighborResult2, NeighborResult3, NeighborResult4, NeighborResult5));
	}

	void NeighborResult1() {
		Narrate("NeighborResult1", Default);
	}

	void NeighborResult2() {
		Narrate("NeighborResult2", Default);
	}

	void NeighborResult3() {
		Narrate("NeighborResult3", Default);
	}

	void NeighborResult4() {
		Narrate("NeighborResult4", Default);
	}

	void NeighborResult5() {
		Narrate("NeighborResult5", Default);
	}

	void NeighborBacon() {
		Narrate("NeighborBacon", Default);
	}

	void LeaveToApartment() {
		World.S.homeKitchen.EnterFromCorridor();
	}

	void LeaveToStreet() {
		World.S.street.EnterFromCorridor();
	}

	void Staircase() {
		Narrate("You are standing in front of a set of stairs at the end of a long corridor. The look old and run-down. Dirt and small debris has accumulated in the corner of the steps. People have scribbled notes on the walls. ");
		Choose(
			Opt(Notes, "Look at the notes"),
			Opt(level == 3, LeaveToRoof, "Access the roof"),
			Opt(level < 3, () => { level++; Staircase(); }, "Walk up"),
			Opt(level > 1, () => { level--; Staircase(); }, "Walk down"),
			Opt(Default, "Turn towards the corridor")
		);
	}

	void Notes() {
		string[] notes = null;
		if(level == 1) {
			notes = new string[] {
				"Die!", "Die!", "Die!"
			};
		} else if(level == 2) {
			notes = new string[] {
				"Die!", "Die!", "Die!"
			};
		} else if(level == 3) {
			notes = new string[] {
				"Die!", "Die!", "Die!"
			};
		} else {
			System.Diagnostics.Debug.Assert(false, "Logic error");
		}
		Narrate(Rnd(notes), Staircase);
	}

	void LeaveToRoof() {
		Narrate("The access to the roof is barred with a metal bolt which is secured with a heavy iron lock. There is a sign on the door saying: " + Quote("Access prohibited! The roof poses the life threating danger of falling.") + " You can only turn around and walk down.", EnterFromRoof);
	}
}
