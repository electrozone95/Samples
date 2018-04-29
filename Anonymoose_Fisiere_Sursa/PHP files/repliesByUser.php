<?php
//Comportament: returneaza numele si prenumele persoanelor care se gasesc la facultatea, seria si grupa precizate
//Server login variables
$user = 'root';
$pass = '';
$db = 'socialnet';

$db = new mysqli('localhost',$user,$pass,$db) or die("Unable to connect");

//program variables
$persoanaID = $_POST["persoanaidPost"];

$sql="SELECT raspunsuri.mesaj FROM raspunsuri
JOIN discussionthreads ON ( discussionthreads.ThreadID = raspunsuri.ThreadID )
JOIN persoane ON (persoane.PersoanaID = '".$persoanaID.".)
ORDER BY raspunsuri.ThreadID ASC";
$result = $db->query($sql);
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        echo "Mesaj:". $row["Mesaj"].";<br>";
    }
} else {
    echo "error;";
}
$db->close();

?>