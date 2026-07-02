using System;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public class ConsoleRoomName : MonoBehaviour
{
	// Token: 0x060005AF RID: 1455 RVA: 0x0003585C File Offset: 0x00033A5C
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Directory")
		{
			return;
		}
		if (this.lastRoom != null && other.gameObject == this.lastRoom)
		{
			return;
		}
		this.lastRoom = other.gameObject;
		string text = this.CreatePath(other.gameObject.transform);
		text = "cd /" + this.RemoveLastLetter(text);
		SingletoneBehaviour<CommandLineController>.Instance.ShowConsole(text);
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x000358DC File Offset: 0x00033ADC
	private string CreatePath(Transform target)
	{
		Transform transform = target;
		string text = "";
		while (transform.parent != null)
		{
			text = transform.GetComponent<RoomInfo>().roomName + "/" + text;
			transform = transform.parent;
		}
		return text;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00011903 File Offset: 0x0000FB03
	private string RemoveLastLetter(string dir)
	{
		if (dir.EndsWith("/"))
		{
			dir = dir.Substring(0, dir.Length - 1);
		}
		return dir;
	}

	// Token: 0x04000624 RID: 1572
	public GameObject lastRoom;
}
