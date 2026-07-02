using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class Effect : MonoBehaviour
{
	// Token: 0x06001AF7 RID: 6903 RVA: 0x000CB6E4 File Offset: 0x000C98E4
	public void ShowEffect()
	{
		this.isStop = false;
		this.mEffect = base.GetComponent<ParticleSystem>();
		if (this.mEffect == null)
		{
			this.particleNull = true;
			Transform[] componentsInChildren = base.GetComponentsInChildren<Transform>(true);
			this.mEffect_ParticleList = new List<ParticleSystem>();
			foreach (Transform transform in componentsInChildren)
			{
				ParticleSystem component = transform.gameObject.GetComponent<ParticleSystem>();
				if (component != null && transform.gameObject.activeSelf)
				{
					this.mEffect_ParticleList.Add(component);
				}
			}
		}
		else
		{
			this.particleNull = false;
		}
		if (!this.particleNull)
		{
			this.mEffect.gameObject.SetActive(true);
			this.mEffect.Play();
			return;
		}
		if (this.mEffect_ParticleList.Count > 0)
		{
			base.gameObject.SetActive(true);
			foreach (ParticleSystem particleSystem in this.mEffect_ParticleList)
			{
				particleSystem.Play();
			}
		}
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x000CB7F8 File Offset: 0x000C99F8
	public void StopEffect()
	{
		if (!this.particleNull)
		{
			this.mEffect.Stop();
			return;
		}
		if (this.mEffect_ParticleList.Count > 0)
		{
			foreach (ParticleSystem particleSystem in this.mEffect_ParticleList)
			{
				particleSystem.Stop();
			}
		}
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x000CB86C File Offset: 0x000C9A6C
	private void Update()
	{
		if (this.particleNull && !this.isStop)
		{
			foreach (ParticleSystem particleSystem in this.mEffect_ParticleList)
			{
				if (!particleSystem.IsAlive())
				{
					this.isAlive = false;
				}
				if (particleSystem.IsAlive())
				{
					this.isAlive = true;
					break;
				}
			}
			if (!this.isAlive)
			{
				Action action = this.finishAction;
				if (action != null)
				{
					action();
				}
				this.finishAction = null;
				Action action2 = this.callBack;
				if (action2 != null)
				{
					action2();
				}
				this.isStop = true;
				base.gameObject.SetActive(false);
				this.mEffect_ParticleList.Clear();
				return;
			}
		}
		else if (!this.particleNull && !this.isStop && !this.mEffect.IsAlive())
		{
			Action action3 = this.finishAction;
			if (action3 != null)
			{
				action3();
			}
			this.finishAction = null;
			Action action4 = this.callBack;
			if (action4 != null)
			{
				action4();
			}
			this.isStop = true;
			this.mEffect.gameObject.SetActive(false);
		}
	}

	// Token: 0x0400180A RID: 6154
	private ParticleSystem mEffect;

	// Token: 0x0400180B RID: 6155
	private List<ParticleSystem> mEffect_ParticleList;

	// Token: 0x0400180C RID: 6156
	public Action callBack;

	// Token: 0x0400180D RID: 6157
	public Action finishAction;

	// Token: 0x0400180E RID: 6158
	private bool particleNull;

	// Token: 0x0400180F RID: 6159
	private bool isAlive;

	// Token: 0x04001810 RID: 6160
	private bool isStop;
}
