using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorTool
{
    [System.Serializable]
    public class Floor : MonoBehaviour
    {
        public GameObject floor;

        public Transform trfTopWall;
        public Transform trfLeftWall;
        public Transform trfRightWall;

        public GameObject topWall;
        public GameObject leftWall;
        public GameObject rightWall;
        public GameObject shadeGameojectTop;
        public GameObject shadeGameojectLeft;
        public GameObject shadeGameojectRight;


        public void ShadeTopWall() => shadeGameojectTop.SetActive(true);
        public void ShadeLeftWall() => shadeGameojectLeft.SetActive(true);
        public void ShadeRightWall() => shadeGameojectRight.SetActive(true);
        public void UnShadeTopWall() => shadeGameojectTop.SetActive(false);
        public void UnShadeLeftWall() => shadeGameojectLeft.SetActive(false);
        public void UnShadeRightWall() => shadeGameojectRight.SetActive(false);
    }
}