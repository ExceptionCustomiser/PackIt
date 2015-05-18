$xmlPath = "pack.xml";
$xmlData = [xml](Get-Content $xmlPath)
Function Project($xmlProjectNode) {
	Foreach($node In $xmlProjectNode.task) {
		Task($node)
	}
}
Function Task($xmlTask) {
	Foreach($node In $xmlTask.ChildNodes)
	{
		If($node.NodeType -Eq [Xml.XmlNodeType]::Element) {
			$function = (Get-Command $node.Name -CommandType Function).ScriptBlock
			Invoke-Command -scriptblock $function
		}
	}
}
Function copy($xmlAction) {
	Write-Host "copy"
}
Function delete($xmlAction) {
	Write-Host "delete"
}
Function zip($xmlAction) {
	Write-Host "zip"
}
Project($xmlData.pack);
Read-Host