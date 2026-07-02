using System;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class WinionFarAway : MonoBehaviour
{
	// Token: 0x06000695 RID: 1685 RVA: 0x0003BDD8 File Offset: 0x00039FD8
	private void Update()
	{
		if (DummyWinionAnimator.movePosition)
		{
			float num = Vector2.Distance(base.transform.position, DummyWinionAnimator.FarAwayTarget.transform.position);
			float num2 = Mathf.InverseLerp(0f, 2f, num);
			float num3 = Mathf.InverseLerp(0f, 8f, num);
			Vector2 vector = (base.transform.position - DummyWinionAnimator.FarAwayTarget.transform.position).normalized;
			float num4 = Random.Range(-15f, 15f);
			Vector2 vector2 = Quaternion.Euler(0f, 0f, num4) * vector;
			float num5;
			if (num <= 2f)
			{
				num5 = Mathf.Lerp(2f, 0.5f, num2);
			}
			else
			{
				num5 = Mathf.Lerp(0.5f, 0.1f, num3);
			}
			float num6 = Random.Range(0.8f, 1.2f);
			Vector2 vector3 = vector2 * (num5 * num6);
			base.transform.position += vector3 * (Time.deltaTime * DummyWinionAnimator.movePower);
		}
	}
}
