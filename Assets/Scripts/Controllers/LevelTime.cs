using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTime : LevelCondition
{
    private float max_time;
    private float m_time;

    private GameManager m_mngr;

    public override void Setup(float value, Text txt, GameManager mngr)
    {
        base.Setup(value, txt, mngr);

        m_mngr = mngr;

        m_time = value;

        max_time = m_time;

        UpdateText();
    }
    
    public override void Reset(Text txt)
    {
        base.Setup(max_time, txt);
        m_time = max_time;
        UpdateText();
    }

    private void Update()
    {
        if (m_conditionCompleted) return;

        if (m_mngr.State != GameManager.eStateGame.GAME_STARTED) return;

        m_time -= Time.deltaTime;

        UpdateText();

        if (m_time <= -1f)
        {
            OnConditionComplete();
        }
    }

    protected override void UpdateText()
    {
        if (m_time < 0f) return;

        m_txt.text = string.Format("TIME:\n{0:00}", m_time);
    }
}
