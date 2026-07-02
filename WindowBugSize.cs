using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

// Token: 0x020000A5 RID: 165
public class WindowBugSize : MonoBehaviour
{
	// Token: 0x06000428 RID: 1064 RVA: 0x0002DA2C File Offset: 0x0002BC2C
	private void Start()
	{
		for (int i = 0; i < this.size + 1; i++)
		{
			this.boneRenderer.transforms.Append(null);
		}
		this.boneRenderer.transforms[0] = this.rootTransform;
		for (int j = 0; j < this.size; j++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.pivot, this.rootTransform);
			this.pivots.Add(gameObject);
			this.boneRenderer.transforms[j + 1] = gameObject.transform;
			gameObject.name = "Pivot " + j.ToString();
			gameObject.transform.localPosition = new Vector3(0.8f, 0f, 0f);
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.damp, this.dampTransform);
			gameObject2.name = "Damp " + j.ToString();
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.GetComponent<DampedTransform>().data.constrainedObject = gameObject.transform;
			gameObject2.GetComponent<DampedTransform>().data.sourceObject = this.rootTransform;
			this.rootTransform = gameObject.transform;
		}
		for (int k = 0; k < this.size; k++)
		{
			if (k % 2 == 0)
			{
				GameObject gameObject3 = Object.Instantiate<GameObject>(this.horrorBody, this.pivots[k].transform);
				gameObject3.transform.localPosition = Vector3.zero;
				gameObject3.transform.localRotation = Quaternion.identity;
				if (k < this.size / 2)
				{
					gameObject3.transform.localScale = new Vector3(0.5f, 1f + 0.05f * (float)k, 1f + 0.05f * (float)k);
				}
				else
				{
					gameObject3.transform.localScale = new Vector3(0.5f, 1f + 0.05f * (float)(this.size - k), 1f + 0.05f * (float)(this.size - k));
				}
			}
			else
			{
				GameObject gameObject4 = Object.Instantiate<GameObject>(this.cubeBody, this.pivots[k].transform);
				gameObject4.transform.localPosition = Vector3.zero;
				gameObject4.transform.localRotation = Quaternion.identity;
			}
		}
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Update()
	{
	}

	// Token: 0x04000466 RID: 1126
	public int size = 10;

	// Token: 0x04000467 RID: 1127
	public BoneRenderer boneRenderer;

	// Token: 0x04000468 RID: 1128
	public GameObject cubeBody;

	// Token: 0x04000469 RID: 1129
	public GameObject horrorBody;

	// Token: 0x0400046A RID: 1130
	public GameObject pivot;

	// Token: 0x0400046B RID: 1131
	public GameObject damp;

	// Token: 0x0400046C RID: 1132
	public Transform rootTransform;

	// Token: 0x0400046D RID: 1133
	public Transform dampTransform;

	// Token: 0x0400046E RID: 1134
	public List<GameObject> pivots = new List<GameObject>();
}
