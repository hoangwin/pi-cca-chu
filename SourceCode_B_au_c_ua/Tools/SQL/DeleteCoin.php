<?php
//khi goi thi goi nhu ben duoi chu y da ta base co it nhat 5 phan tu
//http://gamethuanviet.com/baucautomca/GetCoin.php?username=Hello
//http://gamethuanviet.com/baucuatomca/GetCoin.php?username=hello
	$con = mysql_connect("localhost","gamethua_game","30xxxxx");
	if (!$con)
	  {
		echo "Loi Connect";
		die('Could not connect: ' . mysql_error());
	  }

	mysql_select_db("gamethua_thuanviet", $con);
	
	$username = $_GET["username"];	
	$UpdateCommand = "UPDATE `baucuatomca` "
					."SET `AddCoin`=0 "
					. " WHERE `UserName` =  '".$username."'";
	//echo $UpdateCommand;
	$result = mysql_query($UpdateCommand);
	//echo "</br>". mysql_num_rows ($result)."</br>";
	
	mysql_close($con);
?> 


