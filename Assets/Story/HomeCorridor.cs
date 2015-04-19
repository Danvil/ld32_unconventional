
// The corridor room at the home of our hero.
public class HomeCorridor : StoryEntity {

	int level = 2;

	Dialog baconDialog;
	Dialog peacekeeperRaidStart;
	Dialog elaineMourn;
	Dialog elaineWakeup;
	Dialog elaineHelp;

	public HomeCorridor() {
		// Talk with the door smelling like bacon
		baconDialog = new Dialog();
		baconDialog.AddLine("standing", "You stand before an old worn-down wooden door.",
			new Answer("Knock on the door", "knock"),
			new Answer("Leave", Default));
		baconDialog.AddLine("knock",
			"You knock on the door. At first nothing happens. After a while you hear a muffled voice talking through the door." + Quote("What do you want?"),
			new Answer(Quote("Can I join your breakfast?"), "join"),
			new Answer(Quote("Sorry, wrong door!"), Default));
		baconDialog.AddLine("join", Quote("What breakfast? There is no breakfast here. Mind your own business."),
			new Answer(Quote("But I can clearly smell the meat!"), "meat", () => { W.smelledTheBacon = true; }),
			new Answer(Quote("Well, nevermind then."), Default));
		baconDialog.AddLine("meat", Quote("Be quiet idiot! You will alert someone if you keep shouting around. There is nothing here for you."),
			new Answer(Quote("Please let me in, I am really hungry!"), "noreply"),
			new Answer("Leave", Default));
		baconDialog.AddLine("noreply", "The voice behind the door is quite.",
			new Answer("Knock on the door", "knock"),
			new Answer("Leave", Default));
		// The Peacekeeper enters the buildings
		peacekeeperRaidStart = new Dialog();
		peacekeeperRaidStart.AddLine("hold", Quote("Attention citizens! This is an official Peacekeeping investigation. Return to your apartments and do not interfere.") + " In front of you stands a tall peacekeeper in heavy combat armor. He wields a semi-automatic combat rifle in one hand. In the other he holds a flashlight which he points directly in your face.",
		    new Answer("Quickly run to your apartment", () => { W.elaineMourning = true; LeaveToApartment(); }),
			new Answer(Quote("Hey don't point that flashlight in my face!"), "ask"),
			new Answer(Quote("What is going on here?"), "ask"));
		peacekeeperRaidStart.AddLine("ask", Quote("Stand back citizen! This investigation does not concern you."),
		    new Answer("Quickly run to your apartment", () => { W.elaineMourning = true; LeaveToApartment(); }),
			new Answer(Quote("You can not just walk in here like you own this place!"), PeacekeeperBeatUp)
			);
		// Elaine mourns over her dead brother
		elaineMourn = new Dialog();
		string elaine1 = "She wears an overall which seems to be slightly too large for her size. The overall must have had a powerful blue color once, but now it is worn-out and covered with oil stains and burnt patches. She is wearing a colorful scarf in tones of yellow and red which would have lightened the mood would it be for other circumstances.";
		string elaine2 = "Her head is covered with wild chestnut colored hair, and from the features in her face you estimate her to be in her mid twenties.";
		elaineMourn.AddLine("mourn", "You see a women kneeling over a body with several gunshot wounds. " + elaine1 + " When you approach she quickly lifts her head and gives you a cautious look. " + elaine2 + " Her eyes are in tears, but that gives her a certain frailness and natural beauty.",
		    new Answer(() => !W.refusedElaine, Quote("What happened here?"), "happened"),
			new Answer("Walk away", Default));
		elaineMourn.AddLine("happened", Quote("They killed my brother! My poor brother!") + " She is sobbing violently. " + Quote("What did he do to them? They just walked in here and shot him!"),
			new Answer(Quote("I am so sorry! The peacekeepers do not have the right to kill people"), "agree"),
			new Answer(Quote("This is not my business."), "unmoved"),
		    new Answer(() => { return W.smelledTheBacon; }, Quote("What was he thinking? I could smell the meat even in my bedroom. Eating meat is strictly forbidden during the Weeks of Mourning. The peacekeepers don't take an offense like that easily."), "bacon"));
		elaineMourn.AddLine("agree", Quote("No they don't. They are bullying us as they please, while we have enough to suffer already. My brother was a good man, he always helped other people."), "help");
		elaineMourn.AddLine("unmoved", Quote("How can that not be your business? Next time they kill <i>you</i> because you look the wrong way."), "help");
		elaineMourn.AddLine("bacon", Quote("Still they do not have the right to kill someone over a small piece of meat! There are so many rules which are easy to overstep and it ends in violence so quickly."), "help");
		elaineMourn.AddLine("help", () => elaineHelp.Play("start"));
		// Elaine founds you knocked out
		elaineWakeup = new Dialog();
		elaineWakeup.AddLine("wakeup", Quote("Hey you! Wake up!") + " The voice of a woman is ringing in your ears. You feel a dull pain in your head and body. Slowly you open your eyes and try to focus. A young woman is bowed over you. " + elaine1 + " " + elaine2 + " Her eyes are red as if she has recently cried.",
			new Answer(Quote("What happened?"), "explain"),
			new Answer("...", "explain"));
		elaineWakeup.AddLine("explain", Quote("The peacekeepers have beaten you up. You must have been in their way when they came to kill my brother.") + " She looks has a pained expression in here face in looks way. " + Quote("My poor brother..."),
			new Answer(Quote("They killed someone?"), "ask"),
			new Answer(Quote("This is not my business. I had enough for today!"), "enough"));
		elaineWakeup.AddLine("ask", Quote("Yes. The peacekeepers broke through the door of my brothers apartment. Looks like he found a small animal and ate some of its meat for breakfast. I don't know what happened exactly, but theses idiots shot him several times and now he is lying dead on the floor in a pool of blood."), "help");
		elaineWakeup.AddLine("enough", Quote("This affects everyone! Next time they kill <i>you</i> because you look the wrong way!"), "help");
		elaineWakeup.AddLine("help", () => elaineHelp.Play("start"));
		// Elaine asks you for your help
		elaineHelp = new Dialog();
		elaineHelp.AddLine("start", Quote("Please, you have to help me! It is forbidden to have bodies lying around for too long. You know how strict they are with enforcing their rules.") + " She seems to be very uneasy and looks around nervously. " + Quote("Please help me bring the body outside to the river. If they come back while the body is still lying around they will kill me too."),
			new Answer(Quote("Leave me out of this. I don't want to end like that body."), "refuse"),
			new Answer(Quote("Ok, but let's be quick about it."), "accept1"));
		elaineHelp.AddLine("refuse", Quote("Please you have to help me! I can not carry him alone."),
			new Answer(Quote("No, you are on your own here!"), "refuse2"),
			new Answer(Quote("If I really have to..."), "accept2"));
		elaineHelp.AddLine("refuse2", () => { W.refusedElaine = true; Default(); });
		elaineHelp.AddLine("accept1", Quote("Oh, thank you! I could not do that without your help."), "accept");
		elaineHelp.AddLine("accept2", Quote("Thank you! Thank you so much! It will not take long!"), "accept");
		elaineHelp.AddLine("accept", "", () => { W.elaineMourning = false; },
			new Answer(() => W.peaceKeeperBeatup, "walk"),
			new Answer(() => !W.peaceKeeperBeatup, "carry"));
		elaineHelp.AddLine("walk", "You walk upstairs to the second floor. Two doors away from your apartment lies a body on the ground. ", new Answer("carry"));
		elaineHelp.AddLine("carry", "Suddenly the woman starts to sob. She turns away and puts her hands in front of her face.",
			new Answer("Be silent and wait.", "carrysilent"),
			new Answer(Quote("Let's be quick before they come back."), "carryquick"));
		elaineHelp.AddLine("carrysilent", "The woman covers her face and silently sobs. After a while she seems to get calmer and turns around.", "carryout");
		elaineHelp.AddLine("carryquick", "She quickly turns towards you, with a dismayed expression on her face. She seems to bite on her lips to pull herself together.", "carryout");
		elaineHelp.AddLine("carryout", Quote("I take him under his shoulders, can you lift the feet?") + " You help her lift the body and carry it downstairs. It's an arduous task and you begin to sweat and feel the pain in your back again.", "river");
		elaineHelp.AddLine("river", "Together you carry the body outside the apartment building, over the street and through a small dark alley. A hundred meters further down, the buildings give way to a broad river with brownish water. " + Quote("Here, let's drop him into the water."),
			new Answer(Quote("Don't you want to give him a proper funeral?"), "funeral"),
			new Answer(Quote("Ok. I count until three and we throw him in together."), "throw"));
		elaineHelp.AddLine("funeral", Quote("This is not the time for jokes. You know very well that people like us don't get funerals. Everyone is throwing their dead into the river."), "throw");
		elaineHelp.AddLine("throw", Quote("I'll count till three: 1... 2...") + " Together you swing the body back and forth." + Quote("3!") + " You let loose and the body is flying for not even two meters before it falls into the river with a loud splashing noise.", "end");
		elaineHelp.AddLine("end", "The woman turns towards the water and stands there in complete silence. Slowly the current takes the body and carries him downstream.", "end2");
		elaineHelp.AddLine("end2", "After a while she turns towards you with a weak smile. " + Quote("My name is Elaine.") + " She looks you straight in the eyes and speaks with a silent voice. " + Quote("This injustice has to stop and I have an idea how it can be done. Come meet me at the market place in the " + kElaineShopName + ". I have to run back now to clean up the rest, before the peacekeepers come back.") + "Without waiting for a reply she turns around and runs back towards the alley.", () => { W.timeOfDay = 8; W.riverside.Enter(); });
	}

	public void PeacekeeperBeatUp() {
		W.peaceKeeperBeatup = true;
		Narrate("Before you can even finish talking, the peacekeeper swiftly strikes you with his rifle. You tumble and struggle to keep standing. The peacekeeper enters the corridor and strikes you again this time more heavily. The weight of the impact sweeps you off your feet and you fall to the floor. More peacekeepers enter the corridor and swarm around you. They kick you in your back and side, and someone hits your head. You feel a sharp numbing pain and the world around you gets dark.", ElaineWakeup);
	}

	public void ElaineWakeup() {
		elaineWakeup.Play("wakeup");
	}

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
		bool isBacon = level == 2 && W.IsTime(1, 6);
		Narrate(
			"You find yourself in a long corridor. The ceiling is quite low and on each side there is a perfectly regular sequence of identically looking doors. The atmosphere is rather depressing." + Condition(level == 1, " On one end of the corridor there is a staircase. At the other end there is a door leading outside.") + Condition(level > 1, " On one end of the corridor there is a staircase, the other direction is just a dead end.") + Condition(level == 2 && W.elaineMourning && W.IsTime(1, 6), " There is a body lying in the hallway and a woman is kneeling over it.") + Condition(isBacon, baconText));
		Choose(
			Opt(Staircase, "Go to the staircase"),
			Opt(Neighbor, "Knock on a random door"),
			Opt(level == 2, LeaveToApartment, "Enter your apartment"),
			Opt(level == 1, LeaveToStreet, "Leave the building"),
			Opt(level == 2 && W.elaineMourning && W.IsTime(1, 6), () => elaineMourn.Play("mourn"), "Approach the mourning woman"),
			Opt(isBacon, NeighborBacon, "Find the door which smells like bacon")
		);
	}

	void Neighbor() {
		Narrate("You knock on a random door in the corridor.",
			Choice.Random(NeighborResult1, NeighborResult2, NeighborResult3));
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

	void NeighborBacon() {
		Narrate("You nose leads you towards one of the many doors where the smell of Bacon is most intense.", () => baconDialog.Play("standing"));
	}

	void LeaveToApartment() {
		W.homeKitchen.EnterFromCorridor();
	}

	void LeaveToStreet() {
		if(level == 1 && !W.raidHappend) {
			W.raidHappend = true;
			Narrate("You walk to the door which yields to the street. Just in that moment where you reach for the door handle, someone from outside opens it violently. You can hardly manage to jump back before it slams in your face.",
				() => peacekeeperRaidStart.Play("hold"));
		} else {
			W.street.Enter();
		}
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
