
// The corridor room at the home of our hero.
public class Riverside : StoryEntity {

	Dialog mia;

	public Riverside() {
		mia = new Dialog();
		string miadesc = "Inside there is not much more than a single chair and a couple of blankets lying on the floor. On the chair sits an old woman in her sixties.";
		mia.AddLine("knock",
			"You knock against the door of the hut. There is a rustling noise. After a moment you hear the voice of an old woman. " + Quote("Come in!"),
			"knockenter"
		);
		mia.AddLine("knockenter",
			"You enter the hut. " + miadesc + " She has a book in her lab and looks at you curiously. " + Quote(" The old forgotten people don't get visits from you often."),
			"main"
		);
		mia.AddLine("enter",
			"You push the curtain at the door aside and enter the hut. " + miadesc + " She has a book in her lab and looks at you in disdain. ",
			"main"
		);
		mia.AddLine("main",
			Quote("How can I help you, child?"),
			new Answer(() => W.agreedToPhoto, Quote("Do you know how to enter Kester Manor?"), "kester"),
			new Answer(Quote("I want to know my future."), "future"),
			new Answer(Quote("Nevermind"), "leave")
		);
		mia.AddLine("kester",
			Quote("My son Adam works as a cook at Kester Manor. He is my good boy. Sometimes he brings food for me and the others. We would starve if not for him.") + " She pauses and turns the book in her lap around. " + Quote("Kester is a bad man. He does not allow my son to leave the Manor, but my boy found a way to visit his mother.") + " She gives the thought some weight with a long pause. " + Quote("Sometimes my son has to sleep here outside with us even though he is useful to the community and has a job!") + " A confused look is on her face. " + Quote("My good boy always takes the broken hut. It's really cold in there when the wind howls.") + " You are not completely sure that she still has all her senses together.", () => { W.learnedAboutAdam = true; }, "main");
		mia.AddLine("future",
			Quote("Come here child and give me your hand. I hope you have a small token of gratitude for an old woman?"),
			"future2");
		mia.AddLine("future2", "She takes your hand and feels the shape of your fingers and the inside of your palm. " + Quote("You seem to work hard, child. I see that you will find a loved one to hold dear. Don't loose hope into the future, child.") + " She releases your hand and smiles weakly.", "main");
		mia.AddLine("leave", ExamineHuts);
	}

	public void Enter() {
		Default();
	}

	void LeaveToStreet() {
		W.street.Enter();
	}

	void Default() {
		Narrate("You stand at the riverbank of a great river. The water looks murky and brown and emits an uncomfortable rotten smell. Debris and pieces of wood and plastic are swimming on the water. Fifty meters towards the South the buildings of " + kCityName + " begin. The free space between them and the river is covered in tall, yellowish grass.");
		Choose(
			Opt(LeaveToStreet, "Go to street"),
			Opt(Search, "Search the riverside for something interesting")
		);
	}

	void Search() {
		Narrate("You walk around the riverbank scanning your environment. You see several empty plastic bottles left behind by the river. The riverbank is limited to the East by a tall wall which spans from the nearby buildings into the river. Near the wall there are a couple of shabby wooden huts.");
		Choose(
			Opt(ExaminBottles, "Examine the bottles"),
			Opt(ExamineWall, "Walk to the wall"),
			Opt(ExamineHuts, "Walk to the huts")
		);
	}

	void ExaminBottles() {
		Narrate("You pick up one of the bottles and examine it more closely.");
		Narrate("It says 'Coca Cola' and a refreshing drink for the whole family.", Default);
	}

	void ExamineWall() {
		Narrate("The wall is about six meters tall and topped with barbwire. You know that behind the wall is Kester Manor. You should not linger too long.", Default);
	}

	void ExamineHuts() {
		Narrate("A couple of wooden huts no taller than two meters squeeze against the wall to Kester Manor. In front of one of them is a small fireplace with a couple of burnt pieces of wood. There are probably homes for the homeless people around here. In front of one of the huts stands a small stone statue in a meditating pose. The statue has lost one of his limbs and is partly covered with green moss.");
		Choose(
			Opt(HutCallOut, "Call out"),
			Opt(HutWalkMia, "Approach the hut with the stone statue"),
			Opt(HutWalk, "Approach one of the other huts"),
			Opt(W.learnedAboutAdam, HutDeserted, "Go to the broken hut."),
			Opt(Default, "Leave")
		);
	}

	void HutCallOut() {
		Narrate(Quote("Hello? Is there someone here?") + " Nothing is moving and you get no answer", ExamineHuts);
	}

	int huttext = 0;

	void HutWalk() {
		string[] texts = new string[] {
			"You can see an old woman lying on the floor. When she sees you the woman is shuffling around her blankets and creeps further back into the hut.",
			"An elderly man with a long beard is sitting in the hut. He gives you a hostile glance and snarls " + Quote("Back away rookie!"),
			"The interior of the hut is filled with empty plastic bottles. In the middle of the bottles sits a old woman covered in several layers of cloth. She scowls at you.",
			"The smell of feces billowing around this hut is almost unbearable. Suddenly a dark scheme is moving in the back of the hut and a loud coughing starts."
		};
		Narrate("You approach one of the huts. " + texts[huttext], ExamineHuts);
		huttext ++;
		if(huttext == texts.Length) {
			huttext = 0;
		}
	}

	void HutDeserted() {
		string adam = W.learnedAboutAdam && !W.discoveredHoleInWall ? " It does not look like Adam would sleep here." : "";
		Narrate("You stand in a ruined hut which looks even more shabby than the others. The floor is covered in debris. A couple of plants started to grow and stretch high to access the few rays of light falling through the cracks in the ceiling. There a couple of wooden crates piled against the wall." + adam);
		Choose(
			Opt(W.discoveredHoleInWall, Crawl, "Crawl through the hole in the wall."),
			Opt(W.learnedAboutAdam, HutDesertedInvestigate, "Investigate the hut more closely"),
			Opt(ExamineHuts, "Leave the hut")
		);
	}

	void HutDesertedInvestigate() {
		Narrate("The turn around every piece of debris in the hut. There seems to be nothing special about it. Just as you want to give up, you discover traces of a heavy object being moved near one of the crates. The crate is pretty heavy and you need all your force to move it away from the wall. Behind it you spot a small hole in the wall. If you would crouch on the floor you could crawl through it.");
		Choose(
			Opt(Crawl, "Crawl through the hole"),
			Opt(ExamineHuts, "Leave the hut")
		);
	}

	void Crawl() {
		Narrate("GREAT", () => new TheEnd());
	}

	void HutWalkMia() {
		Narrate("The entry to the hut is barred by an old tattered piece of cloth.");
		Choose(
			Opt(() => mia.Play("knock"), "Knock on the hut"),
			Opt(() => mia.Play("enter"), "Enter the hut"),
			Opt(ExamineHuts, "Leave the hut alone")
		);
	}
}
