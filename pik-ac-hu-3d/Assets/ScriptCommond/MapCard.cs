using UnityEngine;
using System.Collections;

public class MapCard : MonoBehaviour {

	// Use this for initialization
    public const int COL = 10;
    public const int ROW = 6;
    public static float CELL_WIDTH = 73;
    public static float CELL_HEIGHT = 80;
    public static int BEGIN_X = 100;
    public static int BEGIN_Y = -20;
    public const int CardNo = 15;
    public Transform templateGround;
    public Transform templateGround1;
    static Card[][] CardMatrix; // mảng 2 chiều (8x12) lưu giá trị các thẻ hình
    public Transform[] Character;

    static bool CardSelected; // Thẻ hình thứ nhất đã được chọn
    //bien de tim pair
    static int CardX1; // Vị trí của thẻ hình thứ nhất
    static int CardY1;
    static int CardX2; // Vị trí của thẻ hình thứ nhất
    static int CardY2;
    static int rCount; // Chiều dài đường đi từ thẻ hình thứ 2 đến thẻ hình thứ
    static int[] rX; // Đường đi từ thẻ hình thứ 2 đến thẻ hình thứ nhất
    // (rX[0],rY[0]) -> ... ->(rX[rCount-1],rX[rCount-1])
    static int[] rY;

    int tCount; // Chiều dài đường đi tạm trong quá trình đệ quy
    int[] tX, tY;
    static ArrayList arrayListPair = new ArrayList();// luu vao 4
    Direction[] d; // Hướng đi của đường đi tạm
    // Ví dụ: Up -> Left -> Left -> ...
    public static MapCard instance;
    public enum Direction
    {
        Left, Right, Up, Down, None
    }
    void Start()
    {
        instance = this;
        CELL_WIDTH = ((Collider)(templateGround.GetComponent<Collider>())).bounds.size.x + 0.1f;
        CELL_HEIGHT = ((Collider)(templateGround.GetComponent<Collider>())).bounds.size.z + 0.1f;
        templateGround.gameObject.SetActive(false);
        int MAX = (ROW + 2) * (COL + 2);
        rX = new int[MAX];
        rY = new int[MAX];
        tX = new int[MAX];
        tY = new int[MAX];
        d = new Direction[MAX];

        CardMatrix = new Card[ROW + 2][]; // tạo ma trận các thẻ hình

        // Mảng này cho biết mỗi hình xuất hiện mấy lần
        
        int k = 0;
        
        for (int i = 0; i < ROW + 2; i++)
        {
            CardMatrix[i] = new Card[COL + 2];
        
            for (int j = 0; j < COL + 2; j++)
            {
        
                if (i == 0 || i == ROW + 1 || j == 0 || j == COL + 1)
                {
                    Object obj;
                    obj = (Instantiate(templateGround, templateGround.transform.position, Quaternion.identity));
                    Transform tran = (Transform)obj;
                    tran.gameObject.SetActive(false);
                    tran.Translate((-j + 1) * CELL_HEIGHT, 0, (i - 1) * CELL_WIDTH);
                    CardMatrix[i][j] = tran.gameObject.GetComponent<Card>();
                    CardMatrix[i][j].fixPosition = tran.position;
        
                    CardMatrix[i][j].Value = -1;
                    CardMatrix[i][j].X = j;
                    CardMatrix[i][j].Y = i;
        
                }
                else
                {
                    Object obj;
                    if ((i + j) % 2 == 0)
                        obj = (Instantiate(templateGround, templateGround.transform.position, Quaternion.identity));
                    else
                        obj = (Instantiate(templateGround1, templateGround.transform.position, Quaternion.identity));
                    Transform tran = (Transform)obj;
                    tran.gameObject.SetActive(true);
                    tran.Translate((-j + 1) * CELL_HEIGHT, 0, (i - 1) * CELL_WIDTH);
                    //  tran.Rotate(180, 0, 0);
                    int _index = k / 4;
                    Debug.Log(_index);
                    Transform objChar = (Transform)(Instantiate(Character[_index], templateGround.transform.position, Quaternion.identity));
                    objChar.gameObject.SetActive(true);
                    objChar.Translate((-j + 1) * CELL_HEIGHT, 0, (i - 1) * CELL_WIDTH);
                    objChar.Rotate(-70, 0, 0);
                    objChar.parent = tran;

                    CardMatrix[i][j] = tran.gameObject.GetComponent<Card>();
                    CardMatrix[i][j].renderrerObject = objChar.gameObject;
                    CardMatrix[i][j].fixPosition = tran.position;
                    CardMatrix[i][j].Value = _index;
                    CardMatrix[i][j].X = j;
                    CardMatrix[i][j].Y = i;

                    k++;
                }
            }
        }
        autoSortMap();
    }
    private int CountTurn(Direction[] d, int tCount)
    {
        int count = 0;
        for (int i = 2; i < tCount; i++) // duyệt qua các điểm trong đường đi
        {
            if (d[i - 1] != d[i])
                count++; // nếu hướng khác nhau nghĩa là có rẽ
        }
        return count;
    }

    // đệ quy quay lui để tìm đường đi rẽ tối đa 2 lần và ngắn nhất
    // [y,x] là ô hiện tại đang xét
    // direct là hướng đi đến ô [y,x]
    private void FindRoute(int x, int y, Direction direct)
    {
        if (x < 0 || x > COL + 1)
            return;
        if (y < 0 || y > ROW + 1)
            return; // nếu ra khỏi ma trận, thoát
       // Debug.Log(x + "," + y);
        if (CardMatrix[y][x].Value != -1)
            return; // không phải ô trống, thoát (1)

        tX[tCount] = x; // đưa ô [y,x] vào đường đi
        tY[tCount] = y;
        d[tCount] = direct; // ghi nhận là hướng đi đến ô [y,x]

        // nếu rẽ nhiều hơn 2 lần, thoát
        if (CountTurn(d, tCount + 1) > 2)
            return;

        tCount++;
        CardMatrix[y][x].Value = -2; // đánh dấu ô [y,x] đã đi qua
        // lệnh (1) đảm bảo ô đã đi qua sẽ không đi lại nữa
        if (x == CardX1 && y == CardY1) // nếu đã tìm đến vị trí hình thứ nhất
        {
            // kiểm tra xem đường đi mới tìm có ngắn hơn đường đi trong các lần
            // trước?
            if (tCount < rCount)
            {
                // nếu ngắn hơn, ghi nhớ lại đường đi này
                rCount = tCount;
                for (int i = 0; i < tCount; i++)
                {
                    rX[i] = tX[i];
                    rY[i] = tY[i];
                }
            }
        }
        else // nếu chưa đi đến được ô hình thứ nhất -> đệ quy để đi đến 4 ô
        // xung quanh
        {
            FindRoute(x - 1, y, Direction.Left);
            FindRoute(x + 1, y, Direction.Right);
            FindRoute(x, y - 1, Direction.Up);
            FindRoute(x, y + 1, Direction.Down);
        }

        // ô [y,x] đã xét xong, quay lui nên loại ô [y,x] ra khỏi đường đi
        tCount--;
        // đánh dấu lại là ô [y,x] trống, có thể đi qua lại
        CardMatrix[y][x].Value = -1;
    }

    

    public void searchPair(int _CardX1, int _CardY1, int _CardX2, int _CardY2)
    {

        CardX1 = _CardX1;
        CardY1 = _CardY1;

        int x = _CardX2;
        int y = _CardY2;
        if (x < 0 || x > COL || y < 0 || y > ROW)
            return;

        if (CardMatrix[y][x].Value != -1) // Nếu click vào một thẻ hình
        {

            if (x != CardX1 || y != CardY1)
            {
                // nếu thẻ hình thứ 2 giống thẻ hình thứ nhất
                if (CardMatrix[y][x].Value == CardMatrix[CardY1][CardX1].Value)
                {
                    int temp = CardMatrix[y][x].Value;
                    // để dễ dàng trong việc tìm đường, ta đánh dấu 2 ô này là
                    // trống
                    CardMatrix[y][x].Value = CardMatrix[CardY1][CardX1].Value = -1;

                    rCount = 10000; // ban đầu giả sử không có đường
                    // đi
                    tCount = 0;
                    FindRoute(x, y, Direction.None); // đệ quy tìm đường

                    // Sau khi tìm đường khôi phục lại hình ở ô thứ nhất và thứ
                    // 2
                    CardMatrix[y][x].Value = CardMatrix[CardY1][CardX1].Value = temp;

                    if (rCount != 10000) // tìm thấy đường đi rẽ
                    // íthơn 2 lần
                    {
                        Debug.Log("aaaaaaaaa");
                        temp = Random.Range(0, 3) + 1;
                        //	effect1.sprite.setAnim(effect1, temp, false, false);
                        //	effect2.sprite.setAnim(effect2, temp, false, false);
                        CardX2 = x;
                        CardY2 = y;
                        arrayListPair.Add(new Vector4(CardX1, CardY1, CardX2, CardY2));


                        CardSelected = false;

                    }
                }
            }
        }

    }
    public void CardClick(int x, int y) // Nhấn chuột tại ô [y,x]
    // trong ma trận
    {
      
        if (x < 0 || x > COL || y < 0 || y > ROW)
            return;

        if (CardMatrix[y][x].Value != -1) // Nếu click vào một thẻ hình
        {
            if (!CardSelected) // Nếu thẻ hình thứ nhất chưa chọn
            {
                //	SoundManager.playSound(SoundManager.SOUND_CLICK_CARD, 1);
                CardSelected = true; // Đánh dấu thẻ hình thứ nhất đã chọn
                CardX1 = x; // Lưu lại vị trí thẻ hình thứ nhất
                CardY1 = y;
                //	StateGameplay.spriteTileBoard.setAnim(0, CardX1 * CELL_WIDTH + BEGIN_X, CardY1 * CELL_HEIGHT + BEGIN_Y, true, false);
                //here
                // DrawGame();//here
            }
            else // nếu thẻ hình thứ nhất đã chọn
                if (x != CardX1 || y != CardY1)
                {
                    Debug.Log("Okie 0 :" + CardMatrix[y][x].Value + "," + CardMatrix[CardY1][CardX1].Value);
                    // nếu thẻ hình thứ 2 giống thẻ hình thứ nhất
                    if (CardMatrix[y][x].Value == CardMatrix[CardY1][CardX1].Value)
                    {
                        Debug.Log("Okie 1");
                        int temp = CardMatrix[y][x].Value;
                        // để dễ dàng trong việc tìm đường, ta đánh dấu 2 ô này là
                        // trống
                        CardMatrix[y][x].Value = CardMatrix[CardY1][CardX1].Value = -1;

                        rCount = 10000; // ban đầu giả sử không có đường
                        // đi
                        tCount = 0;
                        FindRoute(x, y, Direction.None); // đệ quy tìm đường

                        // Sau khi tìm đường khôi phục lại hình ở ô thứ nhất và thứ
                        // 2
                        CardMatrix[y][x].Value = CardMatrix[CardY1][CardX1].Value = temp;

                        if (rCount != 10000) // tìm thấy đường đi rẽ
                        // íthơn 2 lần
                        {
                            Debug.Log("rCount :" + rCount);
                        //    countFrameDrawPath = 4;
                         //   countFrameDrawAddScrore = 4;
                            Random a = new Random();
                            temp = Random.Range(0, 3) + 1;                            

                            CardMatrix[y][x].Value = -1;
                        //    CardMatrix[y][x].gameObject.SetActive(false);

                            CardMatrix[CardY1][CardX1].Value = -1; // hai thẻ hình này đã được 'ăn'
                            //CardMatrix[CardY1][CardX1].gameObject.SetActive(false);
                            
                            CardX2 = x;
                            CardY2 = y;
                            DrawPath();
                        //    RemainingCount -= 2;
                            CardSelected = false;
                          //  mScoreAdd = 10;


                            // Nếu không còn thẻ hình nào, chúc mừng và khởi tạo

                         //   if (RemainingCount == 0)
                            {

                                //StateWinLose.isWin = true;
                                //FruitLink.changeState(IConstant.STATE_WINLOSE);
                                //SoundManager.pausesoundLoop(1);
                                //SoundManager.playSound(SoundManager.SOUND_WIN, 1);

                            }
                           // else
                            {
                                //SoundManager.playSound(SoundManager.SOUND_COMBOL_1, 1);
                            }

                        }
                        else // nếu không tìm thấy đường đi
                        {
                            //SoundManager.playSound(SoundManager.SOUND_CLICK_CARD, 1);
                            CardSelected = true; // hủy chọn thẻ hình thứ nhất
                            CardX1 = x; // Lưu lại vị trí thẻ hình thứ nhất
                            CardY1 = y;
                            //StateGameplay.spriteTileBoard.setAnim(0, CardX1 * CELL_WIDTH + BEGIN_X, CardY1 * CELL_HEIGHT + BEGIN_Y, true, false);
                        }
                    }
                    else // nếu thẻ hình thứ 2 không giống thẻ hình thứ nhất
                    {
                        //SoundManager.playSound(SoundManager.SOUND_CLICK_CARD, 1);
                        CardSelected = true; // hủy chọn thẻ hình thứ nhất
                        CardX1 = x; // Lưu lại vị trí thẻ hình thứ nhất
                        CardY1 = y;
                        //StateGameplay.spriteTileBoard.setAnim(0, CardX1 * CELL_WIDTH + BEGIN_X, CardY1 * CELL_HEIGHT + BEGIN_Y, true, false);
                    }
                }
        }
    }

    public static void DrawPath()
    {
        Vector3[] waypoints = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0) };

        waypoints = new Vector3[rCount];

        for (int i = 0; i < rCount ; i++)
        {
            Debug.Log(rCount - 1 - i +"," + rX[i] + "," + rY[i]);
         //   Debug.Log(CardMatrix[rY[i]][rX[i]]);
            waypoints[rCount-1- i] = CardMatrix[rY[i]][rX[i]].transform.position;
         //   Debug.Log(i);
        }

        CardMatrix[CardY1][CardX1].willPaird = CardMatrix[CardY2][CardX2].gameObject;
        iTween.MoveTo(CardMatrix[CardY1][CardX1].transform.gameObject, iTween.Hash("path", waypoints, "time",rCount*0.10,"EaseType","linear","oncomplete","Movecompleted"));
       
       
        /*
		if (!effect1.sprite.hasAnimationFinished(effect1._currentAnimation, effect1._currentFrame, effect1._waitDelay)) {

			effect1.sprite.drawAnim(g, effect1, CardX1 * CELL_WIDTH + BEGIN_X, CardY1 * CELL_HEIGHT + BEGIN_Y);
			effect2.sprite.drawAnim(g, effect2, CardX2 * CELL_WIDTH + BEGIN_X, CardY2 * CELL_HEIGHT + BEGIN_Y);
		}
		int i, x1 = 0, y1 = 0, x2, y2;
		if (countFrameDrawPath-- >= 0) {

			FruitLink.mainPaint.setStyle(Style.STROKE);
			FruitLink.mainPaint.setStrokeWidth(5);
			FruitLink.mainPaint.setColor(Color.BLUE);

			x1 = rX[0] * CELL_WIDTH + CELL_WIDTH / 2 + BEGIN_X;
			y1 = rY[0] * CELL_HEIGHT + CELL_HEIGHT / 2 + BEGIN_Y; // (y1, x1) là tâm của thẻ hình thứ 2

			//align path
			if (rX[0] == 0)
				x1 = x1 + 2 * CELL_WIDTH / 5;
			else if (rX[0] == COL + 1)
				x1 = x1 - 2 * CELL_WIDTH / 5;
			if (rY[0] == 0)
				y1 = y1 + 2 * CELL_HEIGHT / 5;
			else if (rY[0] == ROW + 1)
				y1 = y1 - 2 * CELL_HEIGHT / 5;
			//align path 

			for (i = 1; i < rCount; i++) {
				x2 = rX[i] * CELL_WIDTH + CELL_WIDTH / 2 + BEGIN_X;
				y2 = rY[i] * CELL_HEIGHT + CELL_HEIGHT / 2 + BEGIN_Y; // (y1, x1) là tâm của các ô đi qua

				//align path		
				if (rX[i] == 0)
					x2 = x2 + 2 * CELL_WIDTH / 5;
				else if (rX[i] == COL + 1)
					x2 = x2 - 2 * CELL_WIDTH / 5;
				if (rY[i] == 0)
					y2 = y2 + 2 * CELL_HEIGHT / 5;
				else if (rY[i] == ROW + 1)
					y2 = y2 - 2 * CELL_HEIGHT / 5;
				//align path	

				g.drawLine(x1, y1, x2, y2, FruitLink.mainPaint); // Vẽ đường thẳng nối 2 ô
				x1 = x2;
				y1 = y2;

			}
		}
		if (countFrameDrawAddScrore-- >= 0) {

			x1 = rX[rCount / 2] * CELL_WIDTH + CELL_WIDTH / 2 + BEGIN_X;
			y1 = rY[rCount / 2] * CELL_HEIGHT + CELL_HEIGHT / 2 + BEGIN_Y;

			Log.d("rcount : ", " " + rCount);
			Log.d("x,y : ", " " + x1 + "," + y1);
			//if(countFrameDrawAddScrore%2 !=0)
			//PikachuActivity.fontbig_Yellow.drawString(g, " + " + mScoreAdd, x1, y1 - 20 - countFrameDrawAddScrore*2, BitmapFont.ALIGN_CENTER);
			//else
			FruitLink.fontbig_White.drawString(g, " + " + mScoreAdd, x1, y1 - (20 - countFrameDrawAddScrore * 2), BitmapFont.ALIGN_CENTER);
		}
         * */
    }

    public void searchPair()
    {
        arrayListPair.Clear();
        for (int i = 0; i <= ROW + 1; i++)
        {
            for (int j = 0; j <= COL + 1; j++)
            {
                for (int x = 0; x <= ROW + 1; x++)
                {
                    for (int y = j; y <= COL + 1; y++)
                    {
                        searchPair(j, i, y, x);
                    }
                }
            }
        }
        
		if (arrayListPair.Count > 0) {
			int temp = Random.Range(0,arrayListPair.Count);
			CardX1 = (int)(((Vector4) arrayListPair[temp]).x); // Vị trí của thẻ hình
															// thứ nhất
            CardY1 = (int)(((Vector4)arrayListPair[temp]).y);
            CardX2 = (int)(((Vector4)arrayListPair[temp]).z); // Vị trí của thẻ hình
															// thứ nhất
            CardY2 = (int)(((Vector4)arrayListPair[temp]).w);

            Debug.Log(CardX1 + "," + CardY1 + " ," + CardX2 + "," + CardY2);
            GamePlay.instance.isHint = true;
            GamePlay.instance.effectHint1.gameObject.SetActive(true);
            GamePlay.instance.effectHint2.gameObject.SetActive(true);
            GamePlay.instance.effectHint1.position = CardMatrix[CardY1][CardX1].transform.position;
            GamePlay.instance.effectHint2.position = CardMatrix[CardY2][CardX2].transform.position;
            GamePlay.instance.effectHint1.Translate(0, 2, 0,Space.World);
            GamePlay.instance.effectHint2.Translate(0, 2, 0, Space.World);

            GamePlay.instance.effectObject1 = CardMatrix[CardY1][CardX1].gameObject.transform;
            GamePlay.instance.effectObject2 = CardMatrix[CardY2][CardX2].gameObject.transform;
            CardMatrix[CardY1][CardX1].gameObject.GetComponent<Card>().renderrer.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderHightLight;
            CardMatrix[CardY2][CardX2].gameObject.GetComponent<Card>().renderrer.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderHightLight;
		}
    }

    public void autoSortMap()
    {
        ArrayList maptemp = new ArrayList();       
        int count = 0;
        for (int i = 0; i <= ROW + 1; i++)
        {
            for (int j = 0; j <= COL + 1; j++)
            {
                if (CardMatrix[i][j].Value > -1)
                {
                    maptemp.Add(new Vector2(i, j));
                    count++;
                }
              
            }
        }
        Card caardTemp;
        Vector3 pos1;
        Vector3 pos2;
        for (int i = 0; i < count; i++)
        {
            int x1 = (int)(((Vector2)(maptemp[i])).x);
            int y1 = (int)(((Vector2)(maptemp[i])).y);
            int tempindex = Random.Range(0, count);
            int x2 = (int)(((Vector2)(maptemp[tempindex])).x);
            int y2 = (int)(((Vector2)(maptemp[tempindex])).y);
            pos1 = CardMatrix[x1][y1].transform.position;
            pos2 = CardMatrix[x2][y2].transform.position;
            caardTemp = CardMatrix[x1][y1];
            CardMatrix[x1][y1] = CardMatrix[x2][y2];            
            CardMatrix[x2][y2] = caardTemp;

            CardMatrix[x1][y1].transform.position = pos1;
			CardMatrix[x1][y1].fixPosition = pos1;
            CardMatrix[x2][y2].transform.position = pos2;
			CardMatrix[x2][y2].fixPosition = pos2;
            CardMatrix[x1][y1].X = y1;
            CardMatrix[x1][y1].Y = x1;
            CardMatrix[x2][y2].X = y2;
            CardMatrix[x2][y2].Y = x2;

        }

    }
  
	// Update is called once per frame
	void Update () {
	
	}
}
