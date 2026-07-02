using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

// Token: 0x0200044F RID: 1103
public class mParent : MonoBehaviour
{
	// Token: 0x06001F1D RID: 7965 RVA: 0x000DE1AC File Offset: 0x000DC3AC
	public void Update()
	{
		if (this.m_Mode != mParent.Mode.Idle)
		{
			MultiParentConstraint component = this.mParentCon.GetComponent<MultiParentConstraint>();
			WeightedTransformArray sourceObjects = component.data.sourceObjects;
			sourceObjects.SetWeight(0, (this.m_Mode == mParent.Mode.Ground) ? 1f : 0f);
			sourceObjects.SetWeight(1, (this.m_Mode == mParent.Mode.Hand) ? 1f : 0f);
			sourceObjects.SetWeight(2, (this.m_Mode == mParent.Mode.Back) ? 1f : 0f);
			component.data.sourceObjects = sourceObjects;
			this.m_Mode = mParent.Mode.Idle;
		}
	}

	// Token: 0x06001F1E RID: 7966 RVA: 0x0001C149 File Offset: 0x0001A349
	public void Start()
	{
		this.m_Mode = mParent.Mode.Ground;
		Debug.Log("ground");
	}

	// Token: 0x06001F1F RID: 7967 RVA: 0x0001C15C File Offset: 0x0001A35C
	public void hand()
	{
		this.m_Mode = mParent.Mode.Hand;
		Debug.Log("hand");
	}

	// Token: 0x06001F20 RID: 7968 RVA: 0x0001C16F File Offset: 0x0001A36F
	public void back()
	{
		this.m_Mode = mParent.Mode.Back;
		Debug.Log("back");
	}

	// Token: 0x04001D7E RID: 7550
	public GameObject mParentCon;

	// Token: 0x04001D7F RID: 7551
	private mParent.Mode m_Mode;

	// Token: 0x02000450 RID: 1104
	private enum Mode
	{
		// Token: 0x04001D81 RID: 7553
		Idle,
		// Token: 0x04001D82 RID: 7554
		Ground,
		// Token: 0x04001D83 RID: 7555
		Hand,
		// Token: 0x04001D84 RID: 7556
		Back
	}
}
