using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preyoid : Boid
{
    public Toggle[] toggles;

    protected override void Update()
    {
        if(toggles[4].isOn || toggles[5].isOn || toggles[3].isOn)
            FindObjectOfType<Spawner>().ToggleHunter(true);

        if(toggles[9].isOn || toggles[10].isOn || toggles[11].isOn)
            toggles[8].isOn = false;

        // Order is important for priorization
        if (toggles[7].isOn) _steering.WallAvoidance();
        if (toggles[6].isOn) _steering.CollisionAvoidance();
        if (toggles[4].isOn) _steering.Evade(FindObjectOfType<Hunteroid>());
        if (toggles[5].isOn) _steering.Hide(FindObjectOfType<Hunteroid>(), Obstacle.AllObstacles);
        if (toggles[1].isOn) _steering.Flee(MousePos, 15);
        if (toggles[3].isOn) _steering.Pursue(FindObjectOfType<Hunteroid>());
        if (toggles[0].isOn) _steering.Seek(MousePos, 5);
        if (toggles[8].isOn)
        {
            _steering.Flocking();
            toggles[9].isOn = false;
            toggles[10].isOn = false;
            toggles[11].isOn = false;
        }
        if (toggles[9].isOn) _steering.Separation(10);
        if (toggles[11].isOn) _steering.Cohesion(10);
        if (toggles[10].isOn) _steering.Alignment(10);
        if (toggles[2].isOn) _steering.Wander();


        transform.GetChild(0).localScale = Vector3.one;
        transform.GetChild(0).position = transform.position;
        base.Update();
    }
}
