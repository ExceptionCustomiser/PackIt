$xmlPath = "pack.xml";
$xmlData = [xml](Get-Content $xmlPath)
Function Project($xmlProjectNode) {
	Foreach($node In $xmlProjectNode.task) {
		Task($node)
	}
}
Function Task($xmlTask) {
	Write-Host $xmlTask.name
	Foreach($node In $xmlTask.ChildNodes)
	{
		If($node.NodeType -Eq [Xml.XmlNodeType]::Element) {
			$function = (Get-Command $node.Name -CommandType Function).ScriptBlock
			Invoke-Command -scriptblock $function -ArgumentList $node -ea "stop"
		}
	}
}
Function copy($xmlAction) {
	Copy-Item $xmlAction.from $xmlAction.to -ea "stop"
}
Function delete($xmlAction) {
	Remove-Item $xmlAction.path -ea "stop"
}
Function zip($xmlAction) {
	Add-Type -Assembly System.IO.Compression.FileSystem
   $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
   [System.IO.Compression.ZipFile]::CreateFromDirectory($xmlAction.from,
        $xmlAction.to, $compressionLevel, $false)
}
Function rename($xmlAction) {
	Rename-Item $xmlAction.path $xmlAction.newName -ea "stop"
}
Function move($xmlAction) {
	Move-Item $xmlAction.from $xmlAction.to -ea "stop"
}
Function createFolder($xmlAction) {
	Try
	{
		New-Item $xmlAction.folder -type directory -ea "stop"
	}
	Catch
	{
		Write-Host "`tFehler beim Erstellen eines Ordners: "$_.Exception
	}
}
Try
{
	Project($xmlData.pack);
}
Catch
{
	Write-Error $_.Exception
}
Finally
{
	Read-Host
}