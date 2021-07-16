using UnityEngine;

namespace DEBUGGING
{
    public class LevelPlusPlus : MonoBehaviour
    {
        public void AddOne()
        {
            PlayerPrefs.SetInt("level", (PlayerPrefs.GetInt("level") + 1));
        }
    }
}