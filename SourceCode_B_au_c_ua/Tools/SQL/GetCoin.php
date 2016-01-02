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

	// some code
	mysql_select_db("gamethua_thuanviet", $con);

	$username = $_GET["username"];	
	$selectCommand = "SELECT *   FROM  `baucuatomca` WHERE  `UserName` =  '".$username."'";		
	$result = mysql_query($selectCommand);
	//echo mysql_num_rows ($result)."|";
	while($row = mysql_fetch_array($result))
	{
		echo $row['AddCoin'];		
	}
	//end some code
	mysql_close($con);
?> 


