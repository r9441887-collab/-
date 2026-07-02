using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200037B RID: 891
public class WinionCube : MonoBehaviour
{
	// Token: 0x06001AA6 RID: 6822 RVA: 0x0000E32C File Offset: 0x0000C52C
	private void Start()
	{
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x000C4FCC File Offset: 0x000C31CC
	private void Update()
	{
		if (this.birthSoon && !this.shaking)
		{
			if (this.duration == 0f)
			{
				this.duration = Random.Range(1.5f, 3f);
			}
			this.time += Time.deltaTime;
			if (this.time > this.duration)
			{
				this.shaking = true;
				this.originPos = this.outLine.gameObject.GetComponent<RectTransform>().anchoredPosition;
				this.outLine.gameObject.GetComponent<RectTransform>().DOShakeAnchorPos(0.15f, 30f, 30, 60f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
				{
					this.outLine.gameObject.GetComponent<RectTransform>().anchoredPosition = this.originPos;
					this.time = 0f;
					this.duration = 0f;
					this.shaking = false;
				});
			}
		}
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x000192E1 File Offset: 0x000174E1
	public void CubePointEnter()
	{
		if (!this.outLine.enabled && !this.disabled_OutLine)
		{
			this.outLine.enabled = true;
		}
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x00019304 File Offset: 0x00017504
	public void CubePointExit()
	{
		if (this.outLine.enabled && !this.disabled_OutLine)
		{
			this.outLine.enabled = false;
		}
	}

	// Token: 0x06001AAA RID: 6826 RVA: 0x000C5090 File Offset: 0x000C3290
	public void CubePointClick()
	{
		bool flag = false;
		if (this.birthSoon && this.canBorn)
		{
			this.count++;
			SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_1, false, 1f, 1f);
			SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.WinionCube, false, 1f, 0.6f);
			int num = this.count;
			if (num <= 28)
			{
				if (num != 10)
				{
					if (num != 20)
					{
						if (num == 28)
						{
							SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_2, false, 1f, 1f);
							this.cubeImage.sprite = this.cubeImg_List[3];
						}
					}
					else
					{
						SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_2, false, 1f, 1f);
						this.cubeImage.sprite = this.cubeImg_List[2];
					}
				}
				else
				{
					SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_2, false, 1f, 1f);
					this.cubeImage.sprite = this.cubeImg_List[1];
				}
			}
			else if (num != 35)
			{
				if (num != 40)
				{
					if (num == 46)
					{
						SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_3, false, 1f, 1f);
						flag = true;
					}
				}
				else
				{
					SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_2, false, 1f, 1f);
					this.cubeImage.sprite = this.cubeImg_List[5];
				}
			}
			else
			{
				SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_2, false, 1f, 1f);
				this.cubeImage.sprite = this.cubeImg_List[4];
			}
		}
		if (flag)
		{
			this.outLine.gameObject.SetActive(false);
			return;
		}
		if (!this.shaking)
		{
			this.shaking = true;
			this.originPos = this.outLine.gameObject.GetComponent<RectTransform>().anchoredPosition;
			this.outLine.gameObject.GetComponent<RectTransform>().DOShakeAnchorPos(0.2f, 30f, 30, 60f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
			{
				this.outLine.gameObject.GetComponent<RectTransform>().anchoredPosition = this.originPos;
				this.shaking = false;
			});
		}
	}

	// Token: 0x06001AAB RID: 6827 RVA: 0x00019327 File Offset: 0x00017527
	public void AutoBorn()
	{
		base.StartCoroutine(this.Auto_co());
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x00019336 File Offset: 0x00017536
	private IEnumerator Auto_co()
	{
		bool isDestroy = false;
		int _count = 0;
		bool waitTime = false;
		for (;;)
		{
			_count++;
			if (_count % 2 == 0)
			{
				SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_1, false, 1f, 1f);
				SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.WinionCube, false, 1f, 0.6f);
			}
			int num = _count;
			if (num != 6)
			{
				if (num != 10)
				{
					if (num == 15)
					{
						SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_3, false, 1f, 1f);
						this.cubeImage.sprite = this.cubeImg_List[5];
						waitTime = true;
						isDestroy = true;
					}
				}
				else
				{
					SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_2, false, 1f, 1f);
					this.cubeImage.sprite = this.cubeImg_List[4];
					waitTime = true;
				}
			}
			else
			{
				SoundManager.instance.Play_SfxSound_2(SoundManager.SfxSound_2.EggBreakSound_2, false, 1f, 1f);
				this.cubeImage.sprite = this.cubeImg_List[3];
				waitTime = true;
			}
			if (isDestroy)
			{
				break;
			}
			if (!this.shaking)
			{
				this.shaking = true;
				this.originPos = this.outLine.gameObject.GetComponent<RectTransform>().anchoredPosition;
				this.outLine.gameObject.GetComponent<RectTransform>().DOShakeAnchorPos(0.2f, 30f, 30, 60f, false, true, ShakeRandomnessMode.Full).OnComplete(delegate
				{
					this.outLine.gameObject.GetComponent<RectTransform>().anchoredPosition = this.originPos;
					this.shaking = false;
				});
			}
			yield return new WaitUntil(() => !this.shaking);
			if (waitTime)
			{
				waitTime = false;
				yield return new WaitForSeconds(1.5f);
			}
		}
		this.outLine.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0400172D RID: 5933
	public Image outLine;

	// Token: 0x0400172E RID: 5934
	public Image cubeImage;

	// Token: 0x0400172F RID: 5935
	public List<Sprite> cubeImg_List;

	// Token: 0x04001730 RID: 5936
	public bool birthSoon;

	// Token: 0x04001731 RID: 5937
	public bool disabled_OutLine;

	// Token: 0x04001732 RID: 5938
	public bool canBorn = true;

	// Token: 0x04001733 RID: 5939
	private Vector2 originPos;

	// Token: 0x04001734 RID: 5940
	private bool shaking;

	// Token: 0x04001735 RID: 5941
	private float duration;

	// Token: 0x04001736 RID: 5942
	private int count;

	// Token: 0x04001737 RID: 5943
	private float time;
}
