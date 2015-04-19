
// The corridor room at the home of our hero.
public class Market : StoryEntity {

	Dialog elaine;
	Dialog fishmonger;
	Dialog clother;
	Dialog bella;

	public Market() {
		elaine = new Dialog();
		elaine.AddLine("hihelp",
			Quote("Hey, you again! Thank you again for your help back there.") + " Elaine looks a bit better than before. Sitting in the shop seems to give her some confidence. She even manages to pull off a smile, even though you can still see the distress in her eyes.",
			new Answer(Quote("I am sorry for your loss. You must feel terrible."), "pity"),
			new Answer(Quote("You mentioned something about an idea earlier at the river?"), "help"),
			new Answer(Quote("I am just looking around."), "looking")
		);
		elaine.AddLine("pity",
			Quote("I do not want to talk about it now."),
			"default"
		);
		elaine.AddLine("hirefuse",
			Quote("Hello, can I help you? Do you want to buy something?") + " Elaine glances at you wearily.",
			new Answer(Quote("About your brother earlier. I am sorry for refusing to help you. Is there anything I can do?"), "sorry"),
			new Answer(Quote("I am just looking around."), "looking")
		);
		elaine.AddLine("sorry",
			Quote("Luckily one of the neighbors was so decent and helped me with the body. I can not really blame you, you probably were as shocked as myself."),
			"help"
		);
		elaine.AddLine("hi", Quote("Hello there! Any progress with the photos?"), "default");
		elaine.AddLine("default",
			"Elaine is sitting on a chair in a corner of the shop. She seems to be occupied with a small box full of wires and other electronic parts.",
			new Answer(Quote("I am sorry for your loss. You must feel terrible."), "pity"),
			new Answer(Quote("Just looking around."), "looking"),
			new Answer(() => !W.agreedToPhoto, Quote("Can I help you with anything?"), "help"),
			new Answer(() => W.agreedToPhoto, Quote("What should I do again?"), "ihaveforgotten"),
			new Answer("Leave the shop", "leave")
		);
		elaine.AddLine("ihaveforgotten", Quote("Sneak into Kester Manor and try to find out what he is doing there with our food. Take a few unmasking photos and come back to me alive."), "default");
		elaine.AddLine("looking",
			Quote("If you need my counsel or look for something in particular let me know."),
			"default"
		);
		elaine.AddLine("help",
			Quote("The peacekeepers have bullied us for long enough. Since Kester is in Power in " + kCityName + " it has become even worse. Everyone has to contribute almost all food they have to the great celebration for the Day of Mourning. We are starving and everyone who can not pay is beaten up by the peacekeeprs and accused of hiding something. There is talk on the street that Kester is diverting most of what is collected into his own pockets, selling it to the other cities for personal favors and luxury items.") + " A sudden fervor is showing in her voice. " + Quote("We need to stop this and I have just what it needs!"), "fetchphoto"
		);
		elaine.AddLine("fetchphoto", "She walks to the back of her store and scrabbles around in on of the many shelfs covering the walls. A moment later she returns with a box. " + Quote("Look, someone traded this to me yesterday. It is an old instant camera, and the best thing: there are a few films in good condition. These are very rare, but they are pretty useless these days. You can not eat photos after all."),
			new Answer(Quote("How does that help us with Kester?"), "listen"),
			new Answer(Quote("I hoped for something with a bit more power."), "moarpower")
		);
		elaine.AddLine("moarpower", Quote("Don't be stupid. You can not just attack the peacekeepers with raw force. They are too many and too well equipped. You just get killed and this would help no one."), "listen");
		elaine.AddLine("listen", Quote("Listen, it's easy you just have to find a way to sneak into Kester Manor and take a few shots of what they do with our food there. You bring the photos to me and we will show them on the Day of Mourning to unmask him in front of everyone."), "think?"
		);
		elaine.AddLine("think?", Quote("What do you think?"),
			new Answer(Quote("This is madness they will discover me!"), "madness"),
			new Answer(Quote("Why don't you go yourself?"), "yourself"),
			new Answer(Quote("Ok, I will try it, but no promises."), "ok"),
			new Answer(Quote("Kester will pay for what he has done!"), "ok"),
			new Answer(Quote("It is too dangerous. I can not do it."), "nook")
		);
		elaine.AddLine("madness", Quote("Not if you are careful. The Day of Mourning is the only day of the year on which  Kester has to leave his manor and show himself in public. An opportunity like this does not come again for a long time."), "think?");
		elaine.AddLine("yourself", Quote("I do not have the courage to do it. I would just blunder as always and they would detect me."), "think?");
		elaine.AddLine("nook", Quote("Well, I can not force you. If you would please excuse me then."), "default");
		elaine.AddLine("ok", Quote("Splendid!") + " She hands you the camera and a pack of films. " + Quote("Return to me once you have the photos.") + " Elaine seems relieved as if some of the burden on her shoulders has become a bit lighter. " + Quote("Perhaps Bella at the market can help you. She seems to know a few secrets about people."), () => { W.agreedToPhoto = true; }, "default");
		elaine.AddLine("leave", InspectStalls);
		
		// The fish monger tries to sell fish he does not have
		fishmonger = new Dialog();
		fishmonger.AddLine("hi", Quote("Hungry?"),
			new Answer(Quote("Yes, can I have one of your fish?"), "yes"),
			new Answer(Quote("No thanks! Your fish stinks."), "no")
		);
		fishmonger.AddLine("yes", Quote("It's a shame, but I am out of fish."), "leave");
		fishmonger.AddLine("no", Quote("It's fresh like the morning dew!"), "leave");
		fishmonger.AddLine("leave", InspectStalls);
		
		// The clother tries to sell you breeches
		clother = new Dialog();
		clother.AddLine("hi", Quote("Need new breeches?"),
			new Answer(Quote("What? No one is wearing breeches anymore."), "wut")
		);
		clother.AddLine("wut", Quote("They are on discount and they are everything we have today."), "leave");
		clother.AddLine("leave", InspectStalls);

		// Bella!
		bella = new Dialog();
		bella.AddLine("hi", Quote("Hi sweetheart, the guys around here call me Bella. How can I make you happy?") + " She smiles at you warmly.", "dialog");
		bella.AddLine("dialog", "",
			new Answer("Why is the Swordfish closed?", "swordfish"),
			new Answer(() => W.agreedToPhoto, Quote("What is Kester doing with our food?"), "kester_rumors"),
			new Answer(() => W.bellaRumor == 0, Quote("Heard any rumors recently?"), "rumor1", () => { W.bellaRumor = 1; }),
			new Answer(() => W.bellaRumor == 1, Quote("Heard any rumors recently?"), "rumor2", () => { W.bellaRumor = 2; }),
			new Answer(() => W.bellaRumor == 2, Quote("Heard any rumors recently?"), "rumor3", () => { W.bellaRumor = 3; }),
			new Answer(() => W.bellaRumor == 3, Quote("Heard any rumors recently?"), "rumor4", () => { W.bellaRumor = 0; }),
			new Answer(Quote("I will better leave now."), "leave")
		);
		bella.AddLine("swordfish", Quote("Weeks of Mourning, darling. Have you forgotten?") + " She gives a soft sigh. " + Quote("No time for pleasure in theses days. I hope you are not too disappointed about that?"), "dialog");
		bella.AddLine("kester_rumors", Quote("Not so loud, sweetheart. You don't want to alert the peacekeepers. Poor guys already need to worry about so much.") + " She scans the nearby area to make sure no one is listing. " + Quote("Go to the river, you will find your answers there."), "dialog");
		bella.AddLine("rumor1", Quote("Noah seems to cause some trouble in the alleys as usual. Heard word he beat one of my girls. Tis' not nice to beat a girl."), "dialog");
		bella.AddLine("rumor2", Quote("They say an old woman lives near the river who can tell you the future. The 'riverlady' they call her."), "dialog");
		bella.AddLine("rumor3", Quote("At the junkyard there a strange flashes of light at night. Victor must have found another contraption. If you see him, tell him to be more careful, I don't want to loose my best customer."), "dialog");
		bella.AddLine("rumor4", Quote("You are worse than a fishwife, sweety."), "dialog");
		bella.AddLine("leave", InspectStalls);
	}

	public void Enter() {
		Default();
	}

	void LeaveToStreet() {
		W.street.Enter();
	}

	void Default() {
		Narrate("The market square of " + kCityName + " is a relatively busy place. In the middle of the square several workers are erecting a wooden platform for the Day of Mourning. A couple of merchants stand at provisional wooden stalls and try to trade their goods. Others are more lucky and own one of the shops in the adjecent buildings.");
		Choose(
			Opt(LeaveToStreet, "Go to the street"),
			Opt(InspectStalls, "Inspect the market stalls and shops.")
		);
	}

	void InspectStalls() {
		Narrate("You walk over the market looking at the various stalls and shops. Most of them offer used goods like cloth or household items. You barely see anyone which can offer any kind of food. Several of them catch your attention.");
		Choose(
			Opt(LeaveToStreet, "Go to the street"),
			Opt(Fishmonger, "Walk to the fish monger"),
			Opt(Clother, "Walk to the textile trader"),
			Opt(Bella, "Go to 'The Swordfish'"),
			Opt(ElektroSale, "Enter the " + kElaineShopName)
		);
	}

	void Fishmonger() {
		Narrate("A tall man is standing behind a series of empty tables. " + Quote("FRESH FISH! ONLY THE FRESHEST FISH!"));
		Choose(
			Opt(() => fishmonger.Play("hi"), "Speak to the fish monger"),
			Opt(InspectStalls, "Go somewhere else")
		);
	}

	void Clother() {
		Narrate("A small woman stand near several boxes full of old cloth. Most of her ware looks worn several times and has holes in it. She looks at you suspiciously.");
		Choose(
			Opt(() => clother.Play("hi"), "Speak to the cloth trader"),
			Opt(InspectStalls, "Go somewhere else")
		);
	}

	void Bella() {
		Narrate("The door to the 'Swordfish' is barred. A slender, good looking woman stands at a corner nearby. She has soft eyes and most be in her early thirties. Her cloth look less used than expected and even show some of their original red color. She is giving you a cheeky smile.");
		Choose(
			Opt(() => bella.Play("hi"), "Speak to the woman"),
			Opt(InspectStalls, "Go somewhere else")
		);
	}

	void ElektroSale() {
		Narrate("The " + kElaineShopName + " is located in one of the decaying building surrounding the market place. Over the door is a huge neon sign spelling the name of the shop. When you enter a bell is ringing deeper in the shop. The shop is stuffed with shelfs full of old electric tools, equipment and spare parts.");
		Choose(
			Opt(Default, "Leave the shop"),
			Opt(() => {
				if(W.agreedToPhoto) {
					elaine.Play("hi");
				} else {
					elaine.Play(W.refusedElaine ? "hirefuse" : "hihelp");
				}
			}, "Speak to Elaine")
		);
	}

}
