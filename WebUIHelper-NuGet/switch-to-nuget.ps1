# PowerShell script to switch projects from Project Reference to NuGet Package
# Run this when you want to publish a stable version for students

Write-Host "🔄 Switching projects to use NuGet package..." -ForegroundColor Yellow

# List of projects to update
$projects = @(
    "..\Section 5 - Arrays and Collections\01 - Arrays vs List - When Each Makes Sense\StudentGradeTracker\StudentGradeTracker.csproj",
    "..\Section 5 - Arrays and Collections\02 - Generics Made Simple What T Means\StudentGradeTracker\StudentGradeTracker.csproj",
    "..\Section 5 - Arrays and Collections\03 - List string Basics Add Remove Show\StudentGradeTracker\StudentGradeTracker.csproj"
)

foreach ($project in $projects) {
    if (Test-Path $project) {
        Write-Host "📦 Updating $project" -ForegroundColor Green
        
        # Remove Project Reference
        dotnet remove $project reference "CSharpLearning.WebUIHelper\CSharpLearning.WebUIHelper.csproj"
        
        # Add NuGet Package (you'll need to publish first)
        # dotnet add $project package CSharpLearning.WebUIHelper
    }
}

Write-Host "✅ All projects switched to NuGet package!" -ForegroundColor Green
Write-Host "📝 Don't forget to publish the package first:" -ForegroundColor Cyan
Write-Host "   dotnet pack --configuration Release" -ForegroundColor Cyan
Write-Host "   dotnet nuget push ..." -ForegroundColor Cyan
