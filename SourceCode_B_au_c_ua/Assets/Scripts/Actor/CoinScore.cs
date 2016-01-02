using UnityEngine;
using System.Collections;

public class CoinScore : MonoBehaviour {
	//cach thuc la luu vao bien coin1 va coin 2.
	// coin chinh thuc se la coin1 + 3
	//coin 2 se = coin 1 dich di 4 so cuoi
	// Use this for initialization
//	public static uint COIN1_MATCH = 0xAAAAAAAA;//101010
//	public static uint COIN2_MATCH = 0x55555555;//010101

	public static int COIN_OFFSET = 254;//010101
	public static int COIN_DEFAULT = 200;//010101
	public static int coin = 0;

	public static int getRealCoin()
	{
		return coin - COIN_OFFSET;
	}

	public static int getOffsetCoin()
	{
		return coin;
	}

	public static void setOffsetCoin(int realvalue)
	{
		coin = realvalue;
	}

	public static void addScore(int addScore)
	{
		coin += addScore;
	}

	public static void setDefaultValue()
	{
		coin = COIN_DEFAULT + COIN_OFFSET;
	}

}
