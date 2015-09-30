#pragma strict
var quit = false;



function OnMouseUp()
{
    if (quit)
    {
        Application.Quit();
    } else {
        Application.LoadLevel(1);
    }
}

function Update () {
    if (Input.GetKey(KeyCode.Escape))
    {
        Application.Quit();
    }
}

