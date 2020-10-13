using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public string Field;
	public string Field2;

	public void OpenLink()
	{
		Application.OpenURL(Field2);
	}

	public void OpenLinkJS()
	{
		if(Application.platform==RuntimePlatform.WebGLPlayer){
			Application.ExternalEval("window.open('//"+Field+"');");
		}
		if(Application.platform==RuntimePlatform.WindowsEditor || Application.platform==RuntimePlatform.Android){
			OpenLink();
		}
		
	}

	public void OpenLinkJSPlugin()
	{
		#if !UNITY_EDITOR
		openWindow(Field);
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}