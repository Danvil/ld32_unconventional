﻿using UnityEngine;
using System.Collections;

public class World {

	public static World S = new World();

	public int day = 1;
	public int timeOfDay = 6;

	public bool IsFirstLight() {
		return timeOfDay == 6;
	}
}
