using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class MazeHoleLight : MonoBehaviour
{
	// Token: 0x06000300 RID: 768 RVA: 0x000292C4 File Offset: 0x000274C4
	public void SetLightMode()
	{
		this.SetMaterials(this.floors, SingletoneBehaviour<MazeController>.Instance.LightMode.floorMaterials);
		this.SetMaterials(this.walls, SingletoneBehaviour<MazeController>.Instance.LightMode.wallMaterials);
		this.SetMaterials(this.doors, SingletoneBehaviour<MazeController>.Instance.LightMode.doorMaterials);
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00029324 File Offset: 0x00027524
	public void SetMaterials(List<MeshRenderer> renderers, List<Material> materials)
	{
		foreach (MeshRenderer meshRenderer in renderers)
		{
			meshRenderer.SetMaterials(materials);
		}
	}

	// Token: 0x06000302 RID: 770 RVA: 0x00029370 File Offset: 0x00027570
	public void SetDarkMode()
	{
		SingletoneBehaviour<SkyBoxManager>.Instance.SetLightIntensity(0.3f);
		this.SetMaterials(this.floors, SingletoneBehaviour<MazeController>.Instance.DarkMode.floorMaterials);
		this.SetMaterials(this.walls, SingletoneBehaviour<MazeController>.Instance.DarkMode.wallMaterials);
		this.SetMaterials(this.doors, SingletoneBehaviour<MazeController>.Instance.DarkMode.doorMaterials);
	}

	// Token: 0x06000303 RID: 771 RVA: 0x000293E0 File Offset: 0x000275E0
	public void SetTransparentMode()
	{
		this.SetMaterials(this.floors, SingletoneBehaviour<MazeController>.Instance.TransparentMode.floorMaterials);
		this.SetMaterials(this.walls, SingletoneBehaviour<MazeController>.Instance.TransparentMode.wallMaterials);
		this.SetMaterials(this.doors, SingletoneBehaviour<MazeController>.Instance.TransparentMode.doorMaterials);
	}

	// Token: 0x06000304 RID: 772 RVA: 0x00029440 File Offset: 0x00027640
	public void SetWallPapers(int index = 0)
	{
		this.SetMaterials(this.floors, SingletoneBehaviour<MazeController>.Instance.WallPaper[index].floorMaterials);
		this.SetMaterials(this.walls, SingletoneBehaviour<MazeController>.Instance.WallPaper[index].wallMaterials);
		this.SetMaterials(this.doors, SingletoneBehaviour<MazeController>.Instance.WallPaper[index].doorMaterials);
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0000FEBA File Offset: 0x0000E0BA
	public void ActiveNeonSign(bool active)
	{
		this.turnOnNeonSign = active;
		this.neons[0].transform.parent.gameObject.SetActive(active);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x000294B0 File Offset: 0x000276B0
	public void TurnOnNeonSign()
	{
		this.colorToggle = !this.colorToggle;
		Material material = (this.colorToggle ? SingletoneBehaviour<MazeController>.Instance.EmissionRed : SingletoneBehaviour<MazeController>.Instance.EmissionWhite);
		Material material2 = ((!this.colorToggle) ? SingletoneBehaviour<MazeController>.Instance.EmissionRed : SingletoneBehaviour<MazeController>.Instance.EmissionWhite);
		int num = 24;
		for (int i = 0; i < num; i++)
		{
			this.neons[i].material = material;
		}
		int count = this.neons.Count;
		for (int j = 24; j < count; j++)
		{
			this.neons[j].material = material2;
		}
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0000FEE4 File Offset: 0x0000E0E4
	private void OnDisable()
	{
		this.SetLightMode();
		base.StopCoroutine("BlinkInfinity");
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0000FEF7 File Offset: 0x0000E0F7
	public void LightBlink()
	{
		base.StartCoroutine("DarkSetting");
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0000FF05 File Offset: 0x0000E105
	public void LightBlickInfinity()
	{
		base.StartCoroutine("BlinkInfinity");
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0000FF13 File Offset: 0x0000E113
	private IEnumerator BlinkInfinity()
	{
		for (;;)
		{
			yield return new WaitForSeconds(0.5f);
			foreach (Light light in this.lights)
			{
				light.gameObject.SetActive(true);
			}
			yield return new WaitForSeconds(0.5f);
			using (List<Light>.Enumerator enumerator = this.lights.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Light light2 = enumerator.Current;
					light2.gameObject.SetActive(false);
				}
				continue;
			}
			yield break;
		}
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0000FF22 File Offset: 0x0000E122
	private IEnumerator DarkSetting()
	{
		yield return new WaitForSeconds(1.5f);
		this.SetDarkMode();
		yield return new WaitForSeconds(0.3f);
		this.SetLightMode();
		yield return new WaitForSeconds(0.2f);
		this.SetDarkMode();
		yield break;
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0000FF31 File Offset: 0x0000E131
	private void Update()
	{
		if (this.turnOnNeonSign)
		{
			this.timer += Time.deltaTime;
			if (this.timer >= 0.2f)
			{
				this.timer = 0f;
				this.TurnOnNeonSign();
			}
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00029560 File Offset: 0x00027760
	public void SetLight(Color color, bool active = true)
	{
		foreach (Light light in this.lights)
		{
			light.gameObject.SetActive(active);
			light.color = color;
		}
	}

	// Token: 0x0400033E RID: 830
	[Header("바닥과 천장")]
	public List<MeshRenderer> floors = new List<MeshRenderer>();

	// Token: 0x0400033F RID: 831
	[Header("벽과 문틀")]
	public List<MeshRenderer> walls = new List<MeshRenderer>();

	// Token: 0x04000340 RID: 832
	[Header("문")]
	public List<MeshRenderer> doors = new List<MeshRenderer>();

	// Token: 0x04000341 RID: 833
	[Header("라이트")]
	public List<Light> lights = new List<Light>();

	// Token: 0x04000342 RID: 834
	[Header("네온사인")]
	public List<MeshRenderer> neons = new List<MeshRenderer>();

	// Token: 0x04000343 RID: 835
	private bool turnOnNeonSign;

	// Token: 0x04000344 RID: 836
	private bool colorToggle;

	// Token: 0x04000345 RID: 837
	private float timer;

	// Token: 0x04000346 RID: 838
	public Color lightColor;
}
