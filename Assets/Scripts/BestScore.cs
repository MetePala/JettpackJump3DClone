using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    [SerializeField] GameObject _bestScorePanel;
    [SerializeField] TextMesh _bestScoreText;
    [SerializeField] Transform _playerTransform;
    [SerializeField] Text _bestText;
    float _bestScore;
    private void Awake()
    {
        _bestScore= PlayerPrefs.GetInt("bestscore");
        _bestScoreText.text= _bestScore.ToString();
        _bestScorePanel.transform.position = new Vector3(0, 2.2f, _bestScore-10);

        _bestText.text = "Best: "+_bestScore.ToString()+"m";
    }

    private void FixedUpdate()
    {
        if((_bestScore-_playerTransform.position.z)<30)
        {
            _bestScorePanel.SetActive(true);
        }
    }
}
