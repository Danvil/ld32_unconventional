using System;

// The corridor room at the home of our hero.
public class KesterManor : StoryEntity {

	int alarm = 0;

	void IncreaseAlarm(Action followup) {
		alarm ++;
		if(alarm > 5) {
			Alarm();
		} else {
			followup();
		}
	}

	bool holeForce = false;
	bool holeOpen = false;
	bool kneel = false;

	bool createsOpen = false;

	Dialog planks;
	Dialog crates;
	Dialog barne;
	Dialog kester;

	public KesterManor() {
		// Opening the way through the hole in the wall
		planks = new Dialog();
		planks.AddLine("start", "A couple of wooden planks bar your way on the other side.",
			new Answer("Examine the planks", "examine"),
			new Answer("Push against the planks carefully", "pushcare"),
			new Answer("Push against the planks with full force", "pushfull"),
			new Answer("Turn back", LeaveToRiverside));
		planks.AddLine("examine", "The planks seem to be loosly leaning on the other side. Something is supporting them from the other side.", "start");
		planks.AddLine("pushcare", "You carfully push against the planks to get them loose moving them slowly centimeter by centimeter. After a while the space is big enough for you to squeeze through.", () => {
				holeOpen = true; GardnersHut();
			});
		planks.AddLine("pushfull", "You push against the planks as hard as you can. At first not much happens, then suddenly with a loud thumb something falls on the other side and the planks fall backwards.", () => {
				holeOpen = true; holeForce = true; alarm++; GardnersHut();
			});

		// Investigating the crates
		crates = new Dialog();
		crates.AddLine("start", "The crates seem to be in good shape. They are covered with wooden lids.",
			new Answer(() => !createsOpen, "Open the lid of one of the crates", "open"),
			new Answer(() => W.numberOfPhotos > 0 && !createsOpen, "Take a photo of the crates", "photoclosed"),
			new Answer(() => W.numberOfPhotos > 0 && createsOpen, "Take a photo of the crates and their content", "photoopen"),
			new Answer(() => W.numberOfPhotos == 0, "You used all films and can not take anymore photos", "start"),
			new Answer("Walk away", "leave")
			);
		crates.AddLine("open", "You push one of the lids of one of the crates aside and peek inside. At the look of the conents you have to gasp. The box is filled to the top with various food items. You see small sacks with wheat and beans, nuts, dried fruit and dried meat. The food in a single one of theses crates must be enough to feed everyone in " + kCityName + " for more more than a week!", () => { createsOpen = true; }, "start");
		crates.AddLine("photoclosed", "You take a photo of the crates at the pier. They look a bit lonely in front of the broad river.", () => {
				W.numberOfPhotos--;
				W.hasCrateClosePhoto = true;
			}, "start");
		crates.AddLine("photoopen", "You take a photo of the crates at the pier. You make sure that all the delicious food is visible on the photo. If you show this to the community at the Day of Mourning it will create an outcry.", () => {
				W.numberOfPhotos--;
				W.hasCrateOpenPhoto = true;
			}, "start");
		crates.AddLine("leave", RiverSpot);

		// Investigating the barn
		barne = new Dialog();
		barne.AddLine("start", "The barn smells of soil and you can hear noises from the inside.",
			new Answer("Enter the barn", "enter"),
			new Answer("Leave", "leave")
		);
		barne.AddLine("enter", "You sneak through the door in the barn. Inside a thick smell of soil and feces welcomes you.", "main");
		barne.AddLine("main", "The barn is divided into several small pens. In each pen you see an four legged animal. You have not seen large animals like that for a long time and they look rather alien to you. All the cattle " + kCityName + " had died several years ago during the a large drought period, or so they said.",
			new Answer(() => W.numberOfPhotos > 0, "Take a photo of the animals in their pens", "photo"),
			new Answer(() => W.numberOfPhotos == 0, "You used all films and can not take anymore photos", "main"),
			new Answer("Leave the barn", "leave")
		);
		crates.AddLine("photo", "You take a photo of the animals in the barn. If you show this to the community at the Day of Mourning it will create an outcry.", () => {
				W.numberOfPhotos--;
				W.hasBarnPenPhoto = true;
			}, "start");
		barne.AddLine("leave", BarnSpot);

		// Investigating Kester Manor
		// kester = new Dialog();
		// kester.AddLine("start", "Kester Manor is a large building with beautiful glass windows. It seems rather deserted on the outside. If you are careful you can sneak around and peek through the windows.",
		// 	new Answer("Peek through a window", "window"),
		// 	new Answer("Leave the Manor", "leave")
		// );
		// kester.AddLine("window", "start");
		// kester.AddLine("leave", HouseSpot);
	}

	public void EnterHoleInWall() {
		if(holeOpen) {
			GardnersHut();
		} else {
			planks.Play("start");
		}
	}

	void LeaveToRiverside() {
		W.riverside.EnderThroughHoleInWall();
	}

	void GardnersHut() {
		Narrate("You are standing in a large shack full of old tools and empty shelfs. The sun is shining through small holes in the walls. " + (holeForce ? "Near the wall a couple of planks lie on the floor. A metal barrel is overturned and lying on the side." : "A couple of planks lean against the wall covering a hole in the wall. A metal barrel stands near the planks supporting them from the other side."));
		Choose(
			Opt(GardnersHutLeave, "Leave the hut"),
			Opt(GardnersHutLeaveCarefully, "Slowly approach the door and peak out"),
			Opt(LeaveToRiverside, "Crawl through the hole to the other side")
		);
	}

	void GardnersHutLeaveCarefully() {
		Narrate("You approach the door and open it just enough to peek through. On the other side lies a wide garden with green grass and old trees. You make sure that nobody is there and leave the hut.", ShackSpot);
	}

	void GardnersHutLeave() {
		string extra = "";
		if(Rnd(2) == 0) {
			alarm++;
			extra = " Someone is walking through the garden about a hundred meter away. You are not sure if he has seen you.";
		}
		Narrate("You are stepping through the door of the shack into a wide garden with green grass and old trees." + extra, ShackSpot);
	}

	void Steaze(int v, int k, Action followup) {
		Choose(
			Opt(!kneel, () => { kneel = true; SteazeKneel(k, followup); }, "Quickly kneel behind a low hedge to avoid beeing seen"),
			Opt(kneel, () => SteazeKneel(k, followup), "Keep kneeling down"),
			Opt(!kneel, () => SteazeStand(v, followup), "Keep standing to get a better overview"),
			Opt(kneel, () => { kneel = false; SteazeStand(v, followup); }, "Stand up to get a better overview"),
			Opt(Shout, "Shout for someone to help you")
		);
	}

	void SteazeKneel(int v, Action followup) {
		Narrate("You crouch on the ground to avoid beeing seen.");
		followup();
		// string[] spots = new string[] { "near the river", "in the garden", "the barn", "in front of the house" };
		// if(Rnd(5) <= v) {
		// 	Narrate("Someone is walking around " + Rnd(spots) + ". You are not sure if he has seen you.");
		// 	IncreaseAlarm(followup);
		// } else {
		// 	Narrate("No one in sight -- you are safe for now.");
		// 	followup();
		// }
	}

	void SteazeStand(int v, Action followup) {
		string[] spots = new string[] { "near the river", "in the garden", "the barn", "in front of the house" };
		if(Rnd(6) < v) {
			Narrate("Someone is walking around " + Rnd(spots) + ". You are not sure if he has seen you.");
			IncreaseAlarm(followup);
		} else {
			Narrate("No one in sight -- you are safe for now.");
			followup();
		}
	}

	void ShackSpot() {
		Narrate("You are near a shack in a large garden area. The grass is unnatural green here compared to the rest of " + kCityName + " There are small hedges and old trees scattered around. On some of the trees you spot ripe fruite which looks rather delicious. A couple of hundred meter to the East you can see a complex of several one-story houses with a large two-story house in the middle. This must be Kester Manor. On the way there around fifty meter away from Kester Manor you make out a large barn. You can see the river in the North from here.");
		Steaze(1, 0, () => 
			Choose(
				Opt(GardnersHut, "Enter the shack"),
				Opt(RiverSpot, "Walk North to the river"),
				Opt(BarnSpot, "Walk East to the barn")
			));
	}

	void RiverSpot() {
		Narrate("The river is as dirty and muddy as everywhere else. Slowly the water flows towards the West and carries all kinds of debris with it downstream. A wooden pier leads out onto the water. Near the pier a couple of wooden crates are stapled. A small road leads South towards the barn near Kester Manor.");
		Steaze(2, 0, () => 
			Choose(
				Opt(() => crates.Play("start"), "Investigate the crates"),
				Opt(ShackSpot, "Walk back to the shack"),
				Opt(BarnSpot, "Walk south-east to the barn")
			));
	}

	void RoadSpot() {
		Narrate("You are near a dusty gravel road leads West from " + kCityName + " to a large barn in the East. Behind the barn you can see Kester Manor. The road is quite in the open so you should not linger to long here, or someone might see you.");
		Steaze(3, 0, () => 
			Choose(
				Opt(ShackSpot, "Walk north-west to the shack"),
				Opt(BarnSpot, "Walk north-east to the barn")
			));
	}

	void BarnSpot() {
		Narrate("You are near a barn about fifty meter away from Kester Manor. A smell of soil and rot hangs in the air. You can hear shuffeling of feet and grunting noises from inside the barn. A gravel road leads from the barn West towards " + kCityName + " and North to the river.");
		Steaze(3, 0, () => 
			Choose(
				Opt(() => barne.Play("start"), "Walk to the barn door"),
				Opt(W.numberOfPhotos > 0, BarnSpotPhoto, "Take a photo of the barn"),
				Opt(RiverSpot, "Walk North to the river"),
				Opt(ShackSpot, "Walk West to the shack"),
				Opt(RoadSpot, "Walk South to the road"),
				Opt(HouseSpot, "Walk to the house")
			));
	}

	void BarnSpotPhoto() {
		W.numberOfPhotos--;
		W.hasBarnSpotPhoto = true;
		Narrate("You take a quite inconclusive photo of the barn. It is not very exciting and will probably not interest the community.", BarnSpot);
	}

	void HouseSpot() {
		Narrate("You cower behind a small bush near Kester Manor. It is a large two story house in quite good shape. Its windows are not barred with planks as normal, but seem to have real glass. Two peacekeepers are sitting on the ground near the front door. They seem to be occupied with some kind of dice game. If you are careful you can sneak around and peek through the windows.");
		Steaze(4, 0, () => 
			Choose(
				Opt(W.numberOfPhotos > 0, HouseSpotPhoto, "Take a photo of the house"),
				Opt(HouseSneak, "Sneak around the house"),
				Opt(BarnSpot, "Walk to the barn")
			));
	}

	void HouseSpotPhoto() {
		W.numberOfPhotos--;
		W.hasMarnorPhoto = true;
		Narrate("You take a photo of the house with the two guards playing dice. It looks rather picturesque, perhaps it will please the community.", HouseSpot);
	}

	int manorWindow = -1;

	void HouseSneak() {
		string[] text = new string[] {
			"You see a huge room with a big table with chairs around. One wall is covered with a large mirror. There is a bowl with fresh fruit on the table.",
			"You see a room with several sinks and water taps. There are stories of richer communities where people do not have to wash in the river, but use theses rooms to clean themselves.",
			"Carefully you peek into the window, and to your surprise you see Kester himself. He is naked and surrounded by two equally naked young women. This is outrageous as woman have to be separated at all times during the weeks of Mourning!"
		};
		string[] nophoto = new string[] {
			" This might be a good motive, but unfortunately you are out of films.",
			" This does not look very interesting.",
			" This would be a very good motive. A shame that you are out of films."
		};
		manorWindow++;
		if(manorWindow >= text.Length) {
			manorWindow = 0;
		}
		Narrate("Carefully you sneak around the house peeking into the huge glass windows. " + text[manorWindow] + (W.numberOfPhotos == 0 ? nophoto[manorWindow] : ""));
		Choose(
			Opt(W.numberOfPhotos > 0, HouseSneakPhoto, "Take a photo"),
			Opt(HouseSneak, "Sneak to the next window"),
			Opt(BarnSpot, "Walk back to the barn")
		);
	}

	void HouseSneakPhoto() {
		W.manorWindowPhoto.Add(manorWindow);
		if(manorWindow == 2) {
			W.hasSexPhoto = true;
		}
		Narrate("This will hopefully convince the community that Kester needs to be brought down. Perhaps there is more.");
		Choose(
			Opt(HouseSneak, "Sneak to the next window"),
			Opt(BarnSpot, "Walk back to the barn"));
	}

	void Shout() {
		Narrate(Quote("Hello? Can someone help me?") + " You walk around a bit and try to get someones attention. After a minute you see someone approaching. When he sees you he pulls out a whistle.", Alarm);
	}

	void Alarm() {
		W.sneakFail = true;
		Narrate("You here a loud shrieking noise and someone is shouting. " + Quote("Intruders! On you posts!") + " You hear the sound of running feet and in a moment a couple of peacekeepers are running towards you.");
		Choose(
			Opt(Surrender, "Raise your arms to surrender"),
			Opt(Flee, "Run away")
		);
	}

	void Surrender() {
		Narrate("They quickly close in to your location knocking you down in the floor. Someone hits you on the head with his rifle and the world gets dark around you.", W.dayOfMourning.Captured);
	}

	void Flee() {
		Narrate("You quickly turn around and try to get away. Back towards the shank, back to the river. The peacekeepers are shouting loudly and open fire on you. Suddenly you are trust forward by the impact of a bullet. You feel a sharp intense pain in your side and fall over. While your struggle to get up again, the peacekeepers close in and fire a couple of more rounds. Your world gets dark.", W.dayOfMourning.Shot);
	}

	void Default() {
		Narrate("KesterManor");
		Choose(
			Opt(LeaveToRiverside, "Crawl back through the whole in the wall")
		);
	}

}
