using UnityEngine;
using System.Collections;
using UnityEditor;


[InitializeOnLoad]
public class Autorun {
    
    static Autorun() {
        generateBatteries();
    }

     static void generateBatteries() {
         //var objToSpawn = new GameObject("Cool GameObject made from Code");
         ////Add Components
         //objToSpawn.AddComponent<Rigidbody>();
         //objToSpawn.AddComponent<MeshFilter>();
         //objToSpawn.AddComponent<BoxCollider>();
         //objToSpawn.AddComponent<MeshRenderer>();

         //for (var i = 0; i < max; i++) {
         //    var theNewPos= new Vector3 (Random.Range(minPos,maxPos),0,Random.Range(minPos,maxPos));
         //    var go : GameObject = Instantiate(gameObject);
         //    go.transform.position = theNewPos;
         //}
     }
}
