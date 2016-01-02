<?php
	$con = mysql_connect("localhost","gamethua_game","30xxxxx");
	if (!$con)
	{
		die('Could not connect: ' . mysql_error());
	}
	// some code
	mysql_select_db("gamethua_thuanviet", $con);
	//http://gamethuanviet.com/baucuatomca/SMS.php?moid=VIETEL1367522&service_num=7595&phone=84989538859&syntax=NMH&message=NMh&user=NiscoSTM
	//http://gamethuanviet.com/baucuatomca/SMS.php?moid=VIETEL1367522&service_num=7595&phone=84989538859&syntax=NMH&message=NMh BCTC toilaai&user=NiscoSTM
	//tin nhan nguoi goi se la " STM BDCL username
	//tin nhan nguoi goi se la " NMH BAUCUA username
		
	if(!$_REQUEST['moid']  || !$_REQUEST['service_num']  || !$_REQUEST['phone']  || !$_REQUEST['syntax']  || !$_REQUEST['message']  || !$_REQUEST['user'])
	{
		die("204|mot trong cac bien la null");
	}
	$MOID = $_GET["moid"];
	$SERVICE_NUM = $_GET["service_num"];
	$PHONE = $_GET["phone"];
	$SYNTAX = $_GET["syntax"];
	$MESSAGE = $_GET["message"];
	$USER = $_GET["user"];

	$pieces = explode(" ", $MESSAGE);//tach message thanh mang

	//if($pieces[2]=="")
	//die("204|tin nhan khong hop le!");

	$GAME_ID = $pieces[1];
	$U = $pieces[2].trim();
	$pieces[1] = strtoupper($pieces[1].trim());
	
	
	//echo "</br>".strlen($U)."</br>".strlen($pieces[1])."</br>";
	if((strlen($U) <1) || (strlen($pieces[1]) <1))
	{
		echo "204| Loi cu phap tin nhan";
	}
	else if($pieces[1] == "BCTC")
	{
		BauCuaTomCa($U,$T,$PHONE,$SERVICE_NUM);
	}else
	{
		echo "204| Loi khong ro nguyen nhan. Kiem tra lai cu phap tin nhan";
	}
	

mysql_close($con);

function BauCuaTomCa($U,$T,$PHONE,$SERVICE_NUM)
{
	if($SERVICE_NUM =="7595")
	{
		$T = 5000;
		$Coin = 100000;
	}
	else if($SERVICE_NUM =="7695")
	{
		$T = 10000;
		$Coin = 250000;
	}
	else //7795
	{
		$T = 15000;
		$Coin = 500000;
	}	
	
	$result = mysql_query("insert into baucuatomcaInfoSMS (USER, TIEN,PHONE,TONGDAI) values ('$U', '$T','$PHONE','$SERVICE_NUM')");	
	if (!empty($result))
	{		
			echo "200| [BAU CUA TOM CA] Ban vua nap $Coin Coin vao tai khoan '$U' ";
			$result  = mysql_query("select * from baucuatomca where UserName='$U'");		
			if($row = mysql_fetch_array($result))
			{
			
				$Coin += $row['AddCoin'];		
				mysql_query("update baucuatomca set AddCoin= '$Coin' where UserName='$U'");
			}
			else
			{		
				mysql_query("insert into baucuatomca (UserName, AddCoin) values ('$U', '$Coin')");
			}
	}
	else
	{
	
		echo "204| [BAU CUA TOM CA] Loi khong ro nguyen nhan";
	}
	
}
?>

