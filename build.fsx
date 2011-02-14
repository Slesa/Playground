// Learn more about F# at http://fsharp.net
#I @"tools\FAKE"
#r "FakeLib.dll" 
open Fake
//open System.IO

// Directories
let buildDir  = @".\build\"
let testDir   = @".\test\"
let deployDir = @".\deploy\"

// Tools
let fxCopRoot = @".\Tools\FxCop\FxCopCmd.exe"

// Filesets
let appReferences = 
  !+ @"sketches\Godot\**\*.csproj"
    ++ @"src\matrix\**\*.vcxproj"
      |> Scan
//let appReferences = 
//    !+ @"src\matrix\**\*.vcxproj"
//    !+ @"d:\sources\reloaded\src\matrix\src\SolTerminal\SolTerminal.Console\SolTerminal.Console.csproj"
//      |> Scan
 
let testReferences = 
    !+ @"src\**\*Test.csproj" 
        |> Scan 

 // version info
let version = "0.0.1";

// Targets
Target? Clean <-
    fun _ -> CleanDirs [buildDir; testDir; deployDir] 
 
Target? BuildApp <-
    fun _ ->  
        MSBuildDebug buildDir "Build" appReferences
          |> Log "AppBuild-Output: "     
 
Target? BuildTest <-
    fun _ -> 
        MSBuildDebug testDir "Build" testReferences
          |> Log "TestBuild-Output: "
 
Target? FxCop <-
    fun _ ->
        !+ (buildDir + @"\**\*.dll") 
         ++ (buildDir + @"\**\*.exe") 
           |> Scan  
           |> FxCop (fun p -> 
                {p with                     
                    ReportFileName = testDir + "FXCopResults.xml";
                    ToolPath = fxCopRoot})
  
Target? Deploy <-
    fun _ ->
        !+ (buildDir + "\**\*.*") 
          -- "*.zip" 
          |> Scan
          |> Zip buildDir (deployDir + "reloaded." + version + ".zip")
 
Target? Default <- DoNothing
Target? Test <- DoNothing 

// Dependencies
For? BuildApp <- Dependency? Clean    
For? BuildTest <- Dependency? Clean
//For? NUnitTest <- Dependency? BuildApp |> And? BuildTest |> And? FxCop      
//For? xUnitTest <- Dependency? BuildApp |> And? BuildTest |> And? FxCop      
For? Test <- Dependency? BuildApp |> And? BuildTest
//For? Test <- Dependency? xUnitTest |> And? NUnitTest      
For? Deploy <- Dependency? Test      
For? Default <- Dependency? Deploy
 
// start build
Run? Default

//let path = @"d:\sources\reloaded";
//Directory.SetCurrentDirectory( path );
//
//!+ @"src\sketches\Godot\**\*.vcxproj"
//  ++ @"src\matrix\**\*.vcxproj"
//  |> Scan
//  |> printfn "%A";
//
//  System.Console.ReadKey();

