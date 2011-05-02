// Include Fake Libs
#I @"tools\FAKE"
#r "FakeLib.dll" 

open Fake
open Fake.MSBuild

// Directories
let buildDir  = @".\build\"
let testDir   = @".\test\"
let deployDir = @".\deploy\"

// Tools
let mspecPath = @".\Tools\Machine.Specifications\mspec-x86-clr4.exe"
let fxCopRoot = @".\Tools\FxCop\FxCopCmd.exe"

// Filesets
let appReferences =
  !+ @"src\Lucifer\**\*.csproj"
    |> Scan

// Versioninfo
let version = "0.1" // or retrieve from CI server

// Targets
Target "Clean" (fun _ ->
  CleanDirs [buildDir; testDir; deployDir]
)

Target "BuildApp" (fun _ -> 
    AssemblyInfo 
        (fun p -> 
        {p with
            CodeLanguage = CSharp;
            AssemblyVersion = version;
            AssemblyTitle = "Lucifer Office";
            AssemblyDescription = "Bring the light into the world";
            Guid = "EE5621DB-B86B-44eb-987F-9C94BCC98441";
            OutputFileName = @".\src\Lucifer\AssemblyInfo.cs"})          
      
    appReferences 
        |> Seq.map (RemoveTestsFromProject AllNUnitReferences AllSpecAndTestDataFiles)
        |> MSBuildRelease buildDir "Build"
        |> Log "AppBuild-Output: "
)

Target "BuildTest" (fun _ -> 
  MSBuildDebug testDir "Build" appReferences
    |> Log "TestBuild-Output: "
)

Target "MSpecTest" (fun _ ->  
  !! (testDir @@ "*.Specs.dll") 
    |> MSpec (fun p -> 
      {p with 
        ToolPath = mspecPath; 
        HtmlOutputDir = testDir + @"_mspec_report.html"})
)

Target "FxCop" (fun _ ->
    !+ (testDir + @"\**\Lucifer.*.dll") 
      -- "*.Specs.dll"
        ++ (buildDir + @"\**\*.exe") 
        |> Scan  
        |> FxCop (fun p -> 
            {p with                     
                ReportFileName = testDir + "_fxcop_result.xml";
                ToolPath = fxCopRoot})
)

Target "Deploy" (fun _ ->
  !+ (buildDir + "\**\*.*") 
    -- "*.zip" 
      |> Scan
      |> Zip buildDir (deployDir + "Lucifer." + version + ".zip")
)

// Dependencies
// AllTargetsDependOn "Clean"
// "MSpecTest" <== ["BuildApp"; "BuildTest"]
// "Deploy" <== ["MSpecTest"]

"Clean"
  ==> "BuildApp" <=> "BuildTest"
  ==> "FxCop"
  ==> "MSpecTest"
  ==> "Deploy"

// start build
Run "Deploy"

