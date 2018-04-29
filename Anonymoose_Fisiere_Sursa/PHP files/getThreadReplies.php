<?php
//Comportament: preia raspunsurile utilizatorilor in functie de ID-ul threadului in care au fost postate
//Server login variables
$user = 'root';
$pass = '';
$db = 'socialnet';

$db = new mysqli('localhost',$user,$pass,$db) or die("Unable to connect");

//program variables
$threadID = $_POST["threadidPost"];

//echo"great work!!!";

$sql1="SELECT * FROM Raspunsuri WHERE ThreadID=".$threadID.";";
$result = $db->query($sql1);
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
		echo "<br>"."ThreadID:". $row["ThreadID"].";<br>". "ReplyID:" .
		$row["ReplyID"].";<br>"."OPID:". $row["PosterID"].";<br>"."Mesaj:".
		$row["Mesaj"]. ";";
    }
} else {
    echo "error;";
}
$db->close();

?>