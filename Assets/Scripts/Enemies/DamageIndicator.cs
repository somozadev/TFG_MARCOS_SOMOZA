using TMPro;
using System.Collections;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public TMP_Text text;
    public float lifeTime = 0.5f;
    public float minDist = 2f;
    public float maxDist = 3f;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;
    float fraction;

    private void Start()
    {
        CheckColor();
        fraction = lifeTime / 2f;
        transform.LookAt(2 * transform.position - GameManager.Instance.mainCamera.transform.position);
        float dir = Random.rotation.eulerAngles.z;
        iniPos = new Vector3(0, 2, 0);
        float dist = Random.Range(minDist, maxDist);
        targetPos = iniPos + (Quaternion.Euler(0, 0, dir)) * new Vector3(dist / 2, dist / 2, 0f);
        transform.localScale = Vector3.zero;
        StartCoroutine(IndicatorAnim());
    }

    private void CheckColor()
    {
        float curr = GetComponentInParent<Enemy>().stats.CurrentHp;
        float max = GetComponentInParent<Enemy>().stats.Hp;
        Debug.LogWarning("Curr:" + curr + "//" + "Max:" + max);
        Debug.LogWarning("(float)(2 / max):"+ (float)(2 / max));

        if (curr / max > (float)(2 / max))
            text.color = new Color32(30, 255, 30, 255);//green
        else if (curr / max >= (float)(1 / max))
            text.color = new Color32(255, 244, 30, 255);//yellow
        else
            text.color = new Color32(255, 30, 30, 255);//red

    }
    private IEnumerator IndicatorAnim()
    {
        while (timer < lifeTime)
        {
            timer += Time.deltaTime;
            CheckColor();
            if (timer > fraction)
            {
                text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifeTime - fraction));
            }

            transform.localPosition = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifeTime));
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifeTime));
            yield return new WaitForEndOfFrame();
        }
        timer = lifeTime;
        Destroy(gameObject);
    }

    public void SetDamageText(int damage) => text.text = damage.ToString();


}
