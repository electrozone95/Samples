<?php
//Comportament: informatiile discutiei in functie de ID-ul discutiei in 
//Variabile logare
$user = 'root';
$pass = '';
$db = 'socialnet';


$db = new mysqli('localhost',$user,$pass,$db) or die("Unable to connect");
//echo"great work!!!";

$threadID = $_POST["threadidPost"];

$sql="SELECT * FROM DiscussionThreads WHERE ThreadID=".$threadID.";";
$result = $db->query($sql);
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
		echo "<br>"."ThreadID:". $row["ThreadID"].";<br>". "Subiect:" .
		$row["Subiect"].";<br>"."OPID:". $row["OPID"].";<br>"."Mesaj:".
		$row["Mesaj"]. ";";
    }
} else {
    echo "0 results";
}
$db->close();

?>