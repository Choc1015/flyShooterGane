using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Texts : MonoBehaviour, IUpdatable
{
    [SerializeField] TextMeshProUGUI PlayerHpTmp;
    [SerializeField] TextMeshProUGUI StageTmp;

    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
    }

    private void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this);
    }

    public void OnUpdate()
    {
        PlayerHpTmp.text = $"Player HP : {GameManager.Instance.playerHp}";
        StageTmp.text = $"Stage : {GameManager.Instance.Stage}";
    }
}
