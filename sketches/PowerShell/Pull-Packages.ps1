# http://devcentral.f5.com/weblogs/Joe/archive/2009/01/13/powershell-abcs---p-is-for-parameters.aspx
# http://weblogs.asp.net/jgalloway/archive/2011/02/02/downloading-a-local-nuget-repository-with-powershell.aspx

# the parameters when converting to funclet
param(
[switch] $help,
[switch] $notlatest,
[switch] $overwrite,
[int] $top = 500, # use $top = 0 to grab all,
[string] $destination  
)

# --- settings ---
$feedUrlBase = "http://go.microsoft.com/fwlink/?LinkID=206669"

# --- locals ---
$webClient = New-Object System.Net.WebClient

# --- helpers ---
# ternary operator
function Invoke-Ternary ([bool]
$decider,[scriptblock]$iftrue,[scriptblock]$iffalse)
{
begin {}
process {
if ($decider) { &$iftrue} else { &$iffalse }
}
end {}
}
set-Alias ?: Invoke-Ternary

# download entries on a page, recursively called for page continuations
function DownloadEntries {
 param ([string]$feedUrl) 
 $feed = [xml]$webClient.DownloadString($feedUrl)
 $entries = $feed | select -ExpandProperty feed | select -ExpandProperty entry
 $progress = 0
             
 foreach ($entry in $entries) {
    $url = $entry.content.src
    $fileName = $entry.properties.id + "." + $entry.properties.version + ".nupkg"
    $saveFileName = join-path $destination $fileName
    $pagepercent = ((++$progress)/$entries.Length*100)
    if ((-not $overwrite) -and (Test-Path -path $saveFileName)) 
    {
        write-progress -activity "$fileName already downloaded" -status "$pagepercent% of current page complete" -percentcomplete $pagepercent
        continue
    }
    write-progress -activity "Downloading $fileName" -status "$pagepercent% of current page complete" -percentcomplete $pagepercent
    $webClient.DownloadFile($url, $saveFileName)
  }
  $link = $feed.feed.link | where { $_.rel.startsWith("next") } | select href
  if ($link -ne $null) {
    # if using a paged url with a $skiptoken like 
    # http:// ... /Packages?$skiptoken='EnyimMemcached-log4net','2.7'
    # remember that you need to escape the $ in powershell with `
    $feedUrl = $link.href
    DownloadEntries $feedUrl
  }
}  

# the NuGet feed uses a fwlink which redirects
# using this to follow the redirect
function GetPackageUrl {
 param ([string]$feedUrlBase) 
 $resp = [xml]$webClient.DownloadString($feedUrlBase)
 return $resp.service.GetAttribute("xml:base")
}

if( $help ) {
	""
	"-help               this help"
	"-notlatest          do not only pull latest version of package"
	"-overwrite          do overwrite if package already present"
	"-top [count]        Pull only <count> packages, pull all packages when count is 0"
	"-destination [path] Pull all packages to <path>. Default is ~/Documents/LocalNuGet"
	""
	exit
}

# -- output current settings ---
""
"Current settings:"
"---------------------------------------------------"
?: ($notlatest) { "Do not take the latest packages" } { "Get the latest packages" }
?: ($overwrite) { "Overwrite existing packages" } { "Do not overwrite existing packages" }
?: ($top) { "The number of packages to pull is $top" } { "Pull all packages" }
if( !$destination ) { $destination = join-path ([Environment]::GetFolderPath("MyDocuments")) "LocalNuGet" }
"Destination directy: $destination"
""

# --- do the actual work ---
# if dest dir doesn't exist, create it
if (!(Test-Path -path $destination)) { New-Item $destination -type directory }
# set up feed URL
$serviceBase = GetPackageUrl($feedUrlBase)
$feedUrl = $serviceBase + "Packages"
if(-not $notlatest) {
    $feedUrl = $feedUrl + "?`$filter=IsLatestVersion eq true"
    if($top) {
        $feedUrl = $feedUrl + "&`$orderby=DownloadCount desc&`$top=$top"
    }
}
DownloadEntries $feedUrl

