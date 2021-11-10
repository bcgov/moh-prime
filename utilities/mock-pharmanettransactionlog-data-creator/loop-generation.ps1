for ($num = 1 ; $num -le 30 ; $num++)
{   
	$command = "docker run -it --rm --name PRIME-1939 -v `"C:\Users\177092\Source\Repos\moh-prime\utilities\mock-pharmanettransactionlog-data-creator:/usr/src/myapp`" -w /usr/src/myapp python:3.9.7 /bin/bash -c `"pip install Faker; python create-mock-pharmanettransactionlog-data.py 1000000 pnet-logs$num.csv ${num}000000`""
	Write-Host $command
	Invoke-Expression $command
}