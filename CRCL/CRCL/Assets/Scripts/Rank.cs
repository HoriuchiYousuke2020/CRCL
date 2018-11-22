using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyGame/Rank", fileName = "Rank")]

public class Rank : ScriptableObject
{
    private int m_rank;
 private const int THELOWEST = 4;
    void Start()
    {
        m_rank = THELOWEST;
    }

 public void DetermineRANK (int rank)
    {
        m_rank = rank;
    }

    public int GetRank()
    {
        return m_rank;
    }

    public int GetLowest()
    {
        return THELOWEST;
    }

}
