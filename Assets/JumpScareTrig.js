var hero : GameObject;
var scare : GameObject;
var played = false;
var trig = false;

function Start () {
	trig = false;
	scare.GetComponent.<Renderer>().enabled = false;
}

function OnTriggerEnter (other : Collider) {
	trig = true;
}

function Update () {
    if (trig == true) {
        scare.transform.position = new Vector3(hero.transform.position.x, hero.transform.position.y, hero.transform.position.z -10);
		scare.GetComponent.<Renderer>().enabled = true;
		removeovertime ();
		makehimscream ();
	}
}

function removeovertime () {
	yield WaitForSeconds (0.8);
	scare.GetComponent.<Renderer>().enabled = false;
	Destroy(this.gameObject);
	
}

function makehimscream () {
	if (!played) {
		played = true;
		GetComponent.<AudioSource>().Play();
	}
}

