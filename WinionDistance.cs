using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014A RID: 330
public class WinionDistance : SingletoneBehaviour<WinionDistance>
{
	// Token: 0x060007E0 RID: 2016 RVA: 0x00040F3C File Offset: 0x0003F13C
	private void Start()
	{
		if (GameManager.instance.gameData.curChapter != GameManager.Chapter.DesktopMode)
		{
			Object.Destroy(this);
		}
		this.distances.Add(new DistanceData("아이온"));
		this.distances.Add(new DistanceData("보"));
		this.distances.Add(new DistanceData("그리드"));
		this.distances.Add(new DistanceData("픽스"));
		this.distances.Add(new DistanceData("디버그"));
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00040FCC File Offset: 0x0003F1CC
	private void ShakeHand(WinionHandler winionHandler, bool frontHand = true)
	{
		if (winionHandler.characterState == CharacterState.ShakeHand || winionHandler.characterState == CharacterState.PickUp)
		{
			return;
		}
		winionHandler.characterState = CharacterState.ShakeHand;
		winionHandler.winionMovement.SetActiveMovement(false, false, false);
		winionHandler.winionAnimator.PlayAnimation(frontHand ? "ShakeHand" : "ShakeBackHand", false);
		winionHandler.winionAnimator.SetLoop(false);
		winionHandler.winionAnimator.SetAction(delegate
		{
			if (!winionHandler.winionMouseEvent.MouseEnter)
			{
				winionHandler.winionMovement.SetActiveMovement(true, false, false);
				return;
			}
			winionHandler.SetIdleByWinionStatus();
		});
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00041074 File Offset: 0x0003F274
	private bool CanShakeHand(WinionHandler iHandler, WinionHandler jHandler)
	{
		return iHandler.CanInterruptState(2) && jHandler.CanInterruptState(2) && !iHandler.UIWinionEnabled && iHandler.worldWinionEnabled && !jHandler.UIWinionEnabled && jHandler.worldWinionEnabled && iHandler.winionBehaviour.CanInterrupt && jHandler.winionBehaviour.CanInterrupt;
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x000410D0 File Offset: 0x0003F2D0
	private void Update()
	{
		for (int i = 0; i < 5; i++)
		{
			for (int j = i; j < 5; j++)
			{
				if (i != j)
				{
					if (!this.distances[i].canShake)
					{
						return;
					}
					if (!this.distances[j].canShake)
					{
						return;
					}
					float num = Vector2.Distance(GameManager.instance.GetWinionHandlers()[i].transform.position, GameManager.instance.GetWinionHandlers()[j].transform.position);
					if (num < 0.5f)
					{
						if (!this.distances[i].NearBy[j])
						{
							WinionHandler winionHandler = GameManager.instance.GetWinionHandlers()[i];
							WinionHandler winionHandler2 = GameManager.instance.GetWinionHandlers()[j];
							if (this.CanShakeHand(winionHandler, winionHandler2))
							{
								float y = winionHandler.transform.position.y;
								float y2 = winionHandler2.transform.position.y;
								bool flag = y > y2;
								long num2 = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
								if (num2 - this.distances[i].lastShakeTime >= 10000L && num2 - this.distances[j].lastShakeTime >= 10000L)
								{
									this.distances[i].lastShakeTime = num2;
									this.distances[j].lastShakeTime = num2;
									this.ShakeHand(winionHandler, flag);
									this.ShakeHand(winionHandler2, !flag);
								}
							}
							this.distances[i].NearBy[j] = true;
						}
						if (!this.distances[j].NearBy[i])
						{
							this.distances[j].NearBy[i] = true;
						}
					}
					else
					{
						if (this.distances[i].NearBy[j])
						{
							this.distances[i].NearBy[j] = false;
						}
						if (this.distances[j].NearBy[i])
						{
							this.distances[j].NearBy[i] = false;
						}
					}
					this.distances[i].Distance[j] = num;
					this.distances[j].Distance[i] = num;
				}
			}
		}
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x0001328B File Offset: 0x0001148B
	public float GetDistance(Winion targetA, Winion targetB)
	{
		return this.distances[(int)targetA].Distance[(int)targetB];
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x000132A4 File Offset: 0x000114A4
	public void SetShakeHandState(Winion target, bool value)
	{
		this.distances[(int)target].canShake = value;
	}

	// Token: 0x040008C9 RID: 2249
	public List<DistanceData> distances = new List<DistanceData>(5);
}
