<?php
//khi goi thi goi nhu ben duoi chu y da ta base co it nhat 5 phan tu
//http://gamethuanviet.com/baucuatomca/SignUp.php?username=Hello
	$con = mysql_connect("localhost","gamethua_game","30xxxxx");
	if (!$con)
	{
		die('Could not connect: ' . mysql_error());
	}
	// some code

	mysql_select_db("gamethua_thuanviet", $con);
	//kiem tra du lieu da co hay chua
	$username = $_GET["username"];
	$CheckCommand = "Select count(`UserName`) as 'count' from `baucuatomca` WHERE `UserName` ='".$_GET["username"]. "'";
	//echo $CheckCommand;
	$result = mysql_query($CheckCommand);
	$count=mysql_fetch_assoc($result);
	$count=$count['count'];
	if($count >=1)
	{
		echo "FAIL";
	}
	else
	{
		
		$InserCommand = "INSERT INTO `baucuatomca` (`UserName`, `Score`,`Level`, `AddCoin`,`Played`, `Country`)" 
		."VALUES ('". $_GET["username"] 
		."',0,0,0,0,'UNKNOW')";
		
		//echo $InserCommand;
		$result = mysql_query($InserCommand);		
		//echo $count;
		if($result <1)
			echo "FAIL";
		else 
			echo "SUCCESS";
			
		
	}
	//end some code
	mysql_close($con);
?> 


