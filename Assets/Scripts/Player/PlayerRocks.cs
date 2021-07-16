using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocks : MonoBehaviour
{

    public List<string> currentItems;


    public void ApplyForRun()
    {
        foreach (string item in currentItems)
            ApplyModifications(item, true);
    }
    public void ClearForLostRun()
    {currentItems.Clear();}

        public void ApplyHpRock()
        {
            GameManager.Instance.player.playerStats.AddMaxHp(50);
            GameManager.Instance.player.playerStats.SetCurrentHpToMax();
            GameManager.Instance.player.playerStats.HpRegen(0.05f);
            //GameManager.Instance.player.playerStats.SetHpRockVisuals();

        }
        public void ApplyDmgRock()
        {
            GameManager.Instance.player.playerStats.AddMaxHp(-50);
            GameManager.Instance.player.playerStats.AddDmg(0.7f);
            GameManager.Instance.player.playerStats.SetCurrentHpToMax();
            GameManager.Instance.player.playerStats.HpSteal(0.05f);
            //GameManager.Instance.player.playerStats.SetDmgRockVisuals();

        }
        public void ApplyDefRock()
        {
            GameManager.Instance.player.playerStats.AddDef(15);
            GameManager.Instance.player.playerStats.AddSpd(-1f);
            //GameManager.Instance.player.playerStats.SetDefRockVisuals();

        }
        public void ApplySpdRock()
        {

            GameManager.Instance.player.playerStats.AddDef(-7);
            GameManager.Instance.player.playerStats.AddSpd(1.2f);
            GameManager.Instance.player.playerStats.AddAttr(-0.6f);
            //GameManager.Instance.player.playerStats.SetSpdRockVisuals();
        }
        public void ApplyInvRock()
        {
            GameManager.Instance.player.playerStats.InvOnNewRoom(6f);
            //GameManager.Instance.player.playerStats.SetInvRockVisuals();

        }
        public void ApplyDropRock()
        {
            GameManager.Instance.player.extraStats.DropRate = 50;
            //GameManager.Instance.player.playerStats.SeDropRockVisuals();
        }
        public void ApplySalesRock()
        {
            GameManager.Instance.player.extraStats.Sales = true;
            //GameManager.Instance.player.playerStats.SeSalesRockVisuals();
        }
        public void ApplyGrwRock()
        {
            GameManager.Instance.player.extraStats.ShotsSize += 2f;
            //GameManager.Instance.player.playerStats.SeGrwvRockVisuals();
        }
        public void ApplyRndbRock()
        {
            List<string> randomList = new List<string>();
            if (PlayerPrefs.GetInt("HpRock_Equipped", 0) > 0)
                randomList.Add("HpRock");
            if (PlayerPrefs.GetInt("DmgRock_Equipped", 0) > 0)
                randomList.Add("DmgRock");
            if (PlayerPrefs.GetInt("DefRock_Equipped", 0) > 0)
                randomList.Add("DefRock");
            if (PlayerPrefs.GetInt("SpdRock_Equipped", 0) > 0)
                randomList.Add("SpdRock");
            if (PlayerPrefs.GetInt("InvRock_Equipped", 0) > 0)
                randomList.Add("InvRock");
            if (PlayerPrefs.GetInt("DropRock_Equipped", 0) > 0)
                randomList.Add("DropRock");
            if (PlayerPrefs.GetInt("GrwRock_Equipped", 0) > 0)
                randomList.Add("GrwRock");
            if (PlayerPrefs.GetInt("SaleRock_Equipped", 0) > 0)
                randomList.Add("SaleRock");

            for (int i = 0; i < randomList.Count; i++)
            {
                if (randomList.Contains(currentItems[i]))
                {
                    randomList.Remove(currentItems[i]);
                }

            }

            string chosen = randomList.PickOne();
            ApplyModifications(chosen, false);
        }

        private void ApplyModifications(string chosen, bool isRndSearched)
        {
            if (!isRndSearched)
            {
                switch (chosen)
                {
                    case "hp":
                        ApplyHpRock();
                        break;
                    case "dmg":
                        ApplyDmgRock();
                        break;
                    case "def":
                        ApplyDefRock();
                        break;
                    case "spd":
                        ApplySpdRock();
                        break;
                    case "inv":
                        ApplyInvRock();
                        break;
                    case "drop":
                        ApplyDropRock();
                        break;
                    case "grw":
                        ApplyGrwRock();
                        break;
                    case "sales":
                        ApplySalesRock();
                        break;
                }
            }
            else
            {
                switch (chosen)
                {
                    case "HpRock":
                        ApplyHpRock();
                        break;
                    case "DmgRock":
                        ApplyDmgRock();
                        break;
                    case "DefRock":
                        ApplyDefRock();
                        break;
                    case "SpdRock":
                        ApplySpdRock();
                        break;
                    case "InvRock":
                        ApplyInvRock();
                        break;
                    case "DropRock":
                        ApplyDropRock();
                        break;
                    case "GrwRock":
                        ApplyGrwRock();
                        break;
                    case "SaleRock":
                        ApplySalesRock();
                        break;
                    case "RndRock":
                        ApplyRndbRock();
                        break;
                }
            }
        }

    }

