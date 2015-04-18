using UnityEngine;
using System.Collections;

public class Story : MonoBehaviour {

	public Stage stage;

	// Use this for initialization
	void Start () {
		//InfiniteTest(0);
		new TheBegin();
	}
	
	void InfiniteTest(int i) {
		stage.AddNarrative("Shoreditch banh mi tousled, four loko occupy +1 kogi. Cornhole cray meggings, cold-pressed Godard vinyl scenester hoodie four loko post-ironic fingerstache kogi authentic Thundercats flexitarian. Mustache pour-over before they sold out, viral actually cliche literally Odd Future farm-to-table. Drinking vinegar master cleanse Shoreditch ethical umami gastropub. Typewriter normcore four dollar toast cray, listicle fap Odd Future organic tousled sustainable McSweeney's actually meh. Fashion axe you probably haven't heard of them cred, hella Shoreditch gentrify distillery whatever banjo swag Brooklyn hoodie Thundercats roof party forage. Master cleanse retro lo-fi Thundercats Pitchfork actually.");
		stage.AddChoices(InfiniteTest, new string[] { "The fox jumps over the lazy cat.", "The cat jumps over the irrated squirrel.", "The squirrel eats the fox." });
	}

	// Update is called once per frame
	void Update () {
	
	}
}
