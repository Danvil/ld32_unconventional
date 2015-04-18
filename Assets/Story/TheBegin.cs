// The beginning of the adventure.
public class TheBegin : StoryEntity {

	public TheBegin() {
		new Radio(() => World.S.homeRoomSleep.WakeUp());
	}
}
