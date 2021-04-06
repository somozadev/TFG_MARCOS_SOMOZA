using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorTool
{
    public class Selected : MonoBehaviour
    {
        [SerializeField] Material outlineMat;
        [SerializeField] float scale;
        [SerializeField] Color color;
        private Renderer rendererOutline;

        private void Start()
        {
            rendererOutline = CreateOutline(outlineMat, scale, color);
        }
        public void EnableOutline() => rendererOutline.enabled = true;
        public void DisableOutline() => rendererOutline.enabled = true;

        Renderer CreateOutline(Material outlineMat, float sc, Color col)
        {
            GameObject outl = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
            Renderer r = outl.GetComponent<Renderer>();

            r.material = outlineMat;
            r.material.SetColor("_Color", col);
            r.material.SetFloat("_Scale",sc);
            r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            outl.GetComponent<Selected>().enabled = false;
            outl.GetComponent<Collider>().enabled = false;
            
            r.enabled = false;
            return r;
        }

    }


}