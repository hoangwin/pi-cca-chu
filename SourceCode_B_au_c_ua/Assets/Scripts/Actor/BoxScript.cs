using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour {

	// Use this for initialization
	public Sprite spriteBau;
	public Sprite spriteCua;
	public Sprite spriteTom;
	public Sprite spriteCa;
	public Sprite spriteGa;
	public Sprite spriteNai;
	public static Sprite[] spriteBox ={null,null,null,null,null,null};

	public int value;
	void Start () {
		if(spriteBox[0] == null)
		{
			spriteBox[0] = spriteBau;
			spriteBox[1] = spriteCua;
			spriteBox[2] = spriteTom;
			spriteBox[3] = spriteCa;
			spriteBox[4] = spriteGa;
			spriteBox[5] = spriteNai;
		}
		setValue(0);
	}
	public void setValue(int id)
	{
		value = id;
		SpriteRenderer a = this.GetComponent<SpriteRenderer>();
		a.sprite = spriteBox[id];

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
