for ($num = 1 ; $num -le 5 ; $num++)
{   
	$command = "oc cp .\pnet-logs${num}.csv pr-1721-webapi-7-dzssg:/tmp/pnet-logs${num}.csv"
	Write-Host $command
	Invoke-Expression $command
}