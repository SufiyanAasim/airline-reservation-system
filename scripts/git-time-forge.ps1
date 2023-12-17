# PowerShell script to stage and create time-forged git commits between Dec 3, 2023 and Dec 17, 2023

$ErrorActionPreference = "Continue"

# Configure user
git config user.name "Mohammad Sufiyan Aasim"
git config user.email "sufiyan@aerotech.com"

# Setup Remote URL
$remoteUrl = "https://github.com/SufiyanAasim/airline-reservation-system.git"
git remote remove origin 2>$null
git remote add origin $remoteUrl

Write-Host "Starting Git Commit Time-Forging (Dec 3, 2023 - Dec 17, 2023)..." -ForegroundColor Cyan

function Commit-Milestone {
    param (
        [string]$Message,
        [string]$Tag,
        [string]$DateStr,
        [string[]]$Files
    )

    foreach ($file in $Files) {
        if (Test-Path $file) {
            git add $file
        }
    }

    $env:GIT_AUTHOR_DATE = $DateStr
    $env:GIT_COMMITTER_DATE = $DateStr

    git commit -m "$Message"
    if (-not [string]::IsNullOrEmpty($Tag)) {
        git tag -a $Tag -m "Release $Tag"
    }

    Write-Host "Created commit [$Tag] on $DateStr - $Message" -ForegroundColor Green
}

# Milestone 1: v1.0.0 Clearance (Dec 3, 2023)
Commit-Milestone -Message "feat: initial release v1.0.0 Clearance with user login, registration and clearance engine" `
                 -Tag "v1.0.0" `
                 -DateStr "2023-12-03T10:15:00+05:00" `
                 -Files @(".editorconfig", ".gitattributes", ".gitignore", "LICENSE", "README.md", "CHANGELOG.md", "RELEASE.md", "ROADMAP.md", "SECURITY.md", "SUPPORT.md", "CODE_OF_CONDUCT.md", "CONTRIBUTING.md", "src/AirlineApp/AirlineApp.csproj", "src/AirlineApp/Program.cs", "src/AirlineApp/Models/Flight.cs", "src/AirlineApp/Models/Passenger.cs", "src/AirlineApp/Models/User.cs", "src/AirlineApp/Services/FlightService.cs", "src/AirlineApp/Services/AuthService.cs", "src/AirlineApp/Services/FormNavigator.cs", "src/AirlineApp/Services/SoundHelper.cs", "src/AirlineApp/Forms/LoginForm.cs", "src/AirlineApp/Forms/SignupForm.cs", "src/AirlineApp/Forms/WelcomeClearanceForm.cs", "src/AirlineApp/Forms/CustomMessageBox.cs", "docs/releases/v1.0.0.md")

# Milestone 2: v2.0.0 Taxi (Dec 5, 2023)
Commit-Milestone -Message "feat: v2.0.0 Taxi release with interactive seat selection matrix and cabin class pricing" `
                 -Tag "v2.0.0" `
                 -DateStr "2023-12-05T14:30:00+05:00" `
                 -Files @("src/AirlineApp/Forms/SeatTaxiForm.cs", "docs/releases/v2.0.0.md")

# Milestone 3: v3.0.0 Ascent (Dec 8, 2023)
Commit-Milestone -Message "feat: v3.0.0 Ascent release with baggage calculator and in-flight service preferences" `
                 -Tag "v3.0.0" `
                 -DateStr "2023-12-08T11:45:00+05:00" `
                 -Files @("src/AirlineApp/Forms/BaggageAscentForm.cs", "docs/releases/v3.0.0.md")

# Milestone 4: v4.0.0 Cruising (Dec 11, 2023)
Commit-Milestone -Message "feat: v4.0.0 Cruising release with dynamic analytics dashboard and GDI+ load factor canvas" `
                 -Tag "v4.0.0" `
                 -DateStr "2023-12-11T16:20:00+05:00" `
                 -Files @("src/AirlineApp/Forms/AnalyticsCruisingForm.cs", "src/AirlineApp/Models/AnalyticsMetrics.cs", "docs/releases/v4.0.0.md")

# Milestone 5: Executive Report Generator & Touchdown (Dec 14, 2023)
Commit-Milestone -Message "feat: v5.0.0 Touchdown release with executive report builder, printable boarding passes & persistence" `
                 -Tag "v5.0.0" `
                 -DateStr "2023-12-14T15:10:00+05:00" `
                 -Files @("src/AirlineApp/Forms/ReportGenerationForm.cs", "src/AirlineApp/Forms/ReceiptTouchdownForm.cs", "src/AirlineApp/Models/Booking.cs", "src/AirlineApp/Services/BookingHistoryService.cs", "docs/releases/v5.0.0.md", "docs/Architecture.md", "docs/Database.md", "docs/Development.md", "docs/Troubleshooting.md")

# Milestone 6: v6.0.0 Mayday & Credits (Dec 17, 2023)
Commit-Milestone -Message "feat: v6.0.0 Mayday release with emergency protocol, incident log, scripts and team credits" `
                 -Tag "v6.0.0" `
                 -DateStr "2023-12-17T18:00:00+05:00" `
                 -Files @("src/AirlineApp/Forms/MaydayCreditsForm.cs", "docs/releases/v6.0.0.md", "scripts/", ".github/", "tests/")

Write-Host "All commits and release tags successfully forged!" -ForegroundColor Green
