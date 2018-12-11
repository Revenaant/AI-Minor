using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Slider hunterSlider;
    public Slider preyoidSlider;

    public Hunteroid hunter;
    public Preyoid[] preyoids;

    // Update is called once per frame
    void Update()
    {

        if (hunterSlider.value > 0) hunter.gameObject.SetActive(true);
        else hunter.gameObject.SetActive(false);

        for (int i = 0; i < preyoids.Length; i++)
        {
            preyoids[i].gameObject.SetActive(true);
            if (i > preyoidSlider.value - 1) preyoids[i].gameObject.SetActive(false);
        }
    }

    public void ToggleHunter(bool value)
    {
        if (value) hunterSlider.value = 1;
        else hunterSlider.value = 0;

        hunter.gameObject.SetActive(value);
    }

    public void Restart()
    {
        Boid.AllBoids.Clear();
        Obstacle.AllObstacles.Clear();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}