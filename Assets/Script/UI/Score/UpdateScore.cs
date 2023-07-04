using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        // get the score
        var _score = entityManager.CreateEntityQuery(typeof(Scoring)).GetSingleton<Scoring>();
        scoreText.text = _score.score.ToString();
    }
}
