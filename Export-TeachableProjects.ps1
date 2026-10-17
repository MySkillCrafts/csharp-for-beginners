#requires -Version 5.1
<#
.SYNOPSIS
Creates one ZIP per numbered lesson folder, ready for upload to Teachable.
.EXAMPLE
& .\Export-TeachableProjects.ps1
.EXAMPLE
& .\Export-TeachableProjects.ps1 -SectionNumber 3
#>
[CmdletBinding()]
param(
    [string]$SourceRoot = $PSScriptRoot,
    [string]$OutputDirectory,
    [int[]]$SectionNumber
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.IO.Compression
Add-Type -AssemblyName System.IO.Compression.FileSystem

$SourceRoot = (Resolve-Path -LiteralPath $SourceRoot).Path
if (-not $OutputDirectory) {
    $OutputDirectory = Join-Path $SourceRoot 'All Projects'
}
$OutputDirectory = [System.IO.Path]::GetFullPath($OutputDirectory)

# Prune these folders before traversing, including hidden IDE and Git data.
$excludedDirectories = @('bin', 'obj', '.git', '.vs', '.vscode', '.idea', '.history', '.tmp.driveupload')
function Get-ProjectFiles {
    param([string]$Directory)
    foreach ($item in Get-ChildItem -LiteralPath $Directory -Force) {
        if ($item.Attributes -band [System.IO.FileAttributes]::ReparsePoint) {
            throw "Symbolic link or junction needs manual review: $($item.FullName)"
        }
        if ($item.PSIsContainer) {
            if ($item.Name -notin $excludedDirectories) {
                Get-ProjectFiles -Directory $item.FullName
            }
        }
        elseif ($item.Name -notlike '*.user' -and $item.Name -notlike '*.suo' -and
                $item.Name -notin @('.DS_Store', 'Thumbs.db')) {
            $item
        }
    }
}

$sections = @(Get-ChildItem -LiteralPath $SourceRoot -Directory |
    Where-Object { $_.Name -match '^Section (\d+)\b' } | Sort-Object Name)
$jobs = @(
    foreach ($section in $sections) {
        $sectionId = [int][regex]::Match($section.Name, '^Section (\d+)').Groups[1].Value
        if ($SectionNumber -and $sectionId -notin $SectionNumber) { continue }
        foreach ($lesson in Get-ChildItem -LiteralPath $section.FullName -Directory | Sort-Object Name) {
            if ($lesson.Name -notmatch '^(\d+)\s*-\s*(.+)$') { continue }
            $lessonId = [int]$Matches[1]
            $title = $Matches[2]
            $zipName = 'S{0:D2}-{1:D2} - {2}.zip' -f $sectionId, $lessonId, $title
            # Keep output outside lesson folders so it can never archive itself.
            $lessonPrefix = $lesson.FullName.TrimEnd('\', '/') + [System.IO.Path]::DirectorySeparatorChar
            if ($OutputDirectory.Equals($lesson.FullName, [StringComparison]::OrdinalIgnoreCase) -or
                $OutputDirectory.StartsWith($lessonPrefix, [StringComparison]::OrdinalIgnoreCase)) {
                throw 'OutputDirectory must be outside all selected lesson folders.'
            }
            [pscustomobject]@{ Lesson = $lesson; ZipName = $zipName }
        }
    }
)
if ($jobs.Count -eq 0) { throw 'No numbered lesson folders found for the selected sections.' }
if (@($jobs | Group-Object ZipName | Where-Object Count -GT 1).Count -gt 0) {
    throw 'Duplicate archive names found. Check the section and lesson folder numbers.'
}

New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null
$createdCount = 0
foreach ($job in $jobs) {
    $files = @(Get-ProjectFiles -Directory $job.Lesson.FullName)
    if ($files.Count -eq 0) {
        Write-Warning "Skipped empty lesson folder: $($job.Lesson.FullName)"
        continue
    }
    if (@($files | Where-Object Extension -EQ '.csproj').Count -eq 0) {
        Write-Warning "No .csproj in $($job.Lesson.Name); archiving the available lesson materials."
    }
    $zipPath = Join-Path $OutputDirectory $job.ZipName
    # Build a temporary archive first; replace the previous ZIP only on success.
    $temporaryPath = Join-Path $OutputDirectory ([guid]::NewGuid().ToString() + '.tmp')
    try {
        $archive = [System.IO.Compression.ZipFile]::Open($temporaryPath, [System.IO.Compression.ZipArchiveMode]::Create)
        try {
            foreach ($file in $files) {
                $relativePath = $file.FullName.Substring($job.Lesson.FullName.Length + 1)
                $entryName = ($job.Lesson.Name + '/' + $relativePath).Replace('\', '/')
                [System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile(
                    $archive, $file.FullName, $entryName, [System.IO.Compression.CompressionLevel]::Optimal
                ) | Out-Null
            }
        }
        finally { $archive.Dispose() }
        Move-Item -LiteralPath $temporaryPath -Destination $zipPath -Force
    }
    finally {
        if (Test-Path -LiteralPath $temporaryPath) {
            Remove-Item -LiteralPath $temporaryPath
        }
    }
    Write-Host "Created: $($job.ZipName) ($($files.Count) files)"
    $createdCount++
}
Write-Host "Done! Created $createdCount ZIP archives in $OutputDirectory"
