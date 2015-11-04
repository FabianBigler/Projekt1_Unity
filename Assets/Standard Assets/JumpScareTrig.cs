using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class JumpScareTrig : MonoBehaviour
{
    public GameObject Player;
    public GameObject ScarePane;
    public GameObject AiController;
    public bool played = false;
    public bool trig = false;

    void Start()
    {
        trig = false;
        ScarePane.GetComponent<Renderer>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        trig = true;
        ScarePane.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - 10);
        ScarePane.GetComponent<Renderer>().enabled = true;
        GetComponent<AudioSource>().Play();
        var control = AiController.GetComponent<AICharacterControl>();
        control.characterState.Scare(control);
        //StartCoroutine(waitSomeTime());
        //Destroy(gameObject);
        //StartCoroutine(waitSomeTime());
        //ScarePane.GetComponent<Renderer>().enabled = false;
        //Destroy(gameObject);                        
        //makePlayerScream();
    }

    //void Update()
    //{
    //    if (trig == true)
    //    {
    //        StartCoroutine(waitSomeTime());
    //        Destroy(gameObject);
    //        //GetComponent<AudioSource>().Play();
    //    }
    //}

    //private IEnumerator waitSomeTime()
    //{
    //    yield return new WaitForSeconds(1f);        
    //    ScarePane.GetComponent<Renderer>().enabled = false;
    //    Destroy(gameObject);
    //    trig = false;     
    //}

    //private IEnumerator makePlayerScream()
    //{
    //    if (!played)
    //    {
    //        played = true;
    //        GetComponent<AudioSource>().Play();
    //    }
    //    yield return 0;
    //}

}