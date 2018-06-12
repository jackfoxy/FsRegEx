// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#r "paket: groupref FakeBuild //"

#load "./.fake/build.fsx/intellisense.fsx"
//#load "./docsrc/tools/generate.fsx"



open System.IO
open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.DotNet.Testing


// --------------------------------------------------------------------------------------
// START TODO: Provide project-specific details below
// --------------------------------------------------------------------------------------

// Information about the project are used
//  - for version and project name in generated AssemblyInfo file
//  - by the generated NuGet package
//  - to run tests and to publish documentation on GitHub gh-pages
//  - for documentation, you also need to edit info in "docsrc/tools/generate.fsx"

// The name of the project
// (used by attributes in AssemblyInfo, name of a NuGet package and directory in 'src')
let project = "FsRegEx"

// Short summary of the project
// (used as description in AssemblyInfo and as a short summary for NuGet package)
let summary = "Composable F# regular expressions."

// Longer description of the project
// (used as a description for NuGet package; line breaks are automatically cleaned up)
let description = "FsRegEx provides composable F# functionality for all the capabilities of the .NET Regex class including supporting pipe forward |> composability."

// List of author names (for NuGet package)
let authors = [ "Jack Fox" ]

// Tags for your project (for NuGet package)
let tags = "F# fsharp regularexpression"

// File system information
let solutionFile  = "FsRegEx.sln"

// Default target configuration
let configuration = "Release"

// Pattern specifying assemblies to be tested using Expecto
let testAssemblies = "tests/**/bin" </> configuration </> "net47" </> "FsRegEx.Tests.exe"

// Git configuration (used for publishing documentation in gh-pages branch)
// The profile where the project is posted
let gitOwner = "jackfoxy"
let gitHome = sprintf "%s/%s" "https://github.com" gitOwner

// The name of the project on GitHub
let gitName = "FsRegEx"

// The url for the raw files hosted
let gitRaw = Environment.environVarOrDefault "gitRaw" "https://raw.githubusercontent.com/jackfoxy"

// --------------------------------------------------------------------------------------
// END TODO: The rest of the file includes standard build steps
// --------------------------------------------------------------------------------------

// Read additional information from the release notes document
let release = ReleaseNotes.load "RELEASE_NOTES.md"

// Helper active pattern for project types
let (|Fsproj|Csproj|Vbproj|Shproj|) (projFileName:string) =
    match projFileName with
    | f when f.EndsWith("fsproj") -> Fsproj
    | f when f.EndsWith("csproj") -> Csproj
    | f when f.EndsWith("vbproj") -> Vbproj
    | f when f.EndsWith("shproj") -> Shproj
    | _                           -> failwith (sprintf "Project file %s not supported. Unknown project type." projFileName)

// Generate assembly info files with the right version & up-to-date information
Target.create "AssemblyInfo" (fun _ ->
    let getAssemblyInfoAttributes projectName =
        [ AssemblyInfo.Title (projectName)
          AssemblyInfo.Product project
          AssemblyInfo.Description summary
          AssemblyInfo.Version release.AssemblyVersion
          AssemblyInfo.FileVersion release.AssemblyVersion
          AssemblyInfo.Configuration configuration ]

    let getProjectDetails projectPath =
        let projectName = Path.GetFileNameWithoutExtension(projectPath)
        ( projectPath,
          projectName,
          Path.GetDirectoryName(projectPath),
          (getAssemblyInfoAttributes projectName)
        )

    !! "src/**/*.??proj"
    |> Seq.map getProjectDetails
    |> Seq.iter (fun (projFileName, _, folderName, attributes) ->
        match projFileName with
        | Fsproj -> AssemblyInfoFile.createFSharp (folderName </> "AssemblyInfo.fs") attributes
        | Csproj -> AssemblyInfoFile.createCSharp ((folderName </> "Properties") </> "AssemblyInfo.cs") attributes
        | Vbproj -> AssemblyInfoFile.createVisualBasic ((folderName </> "My Project") </> "AssemblyInfo.vb") attributes
        | Shproj -> ()
        )
)

// Copies binaries from default VS location to expected bin folder
// But keeps a subdirectory structure for each project in the
// src folder to support multiple project outputs
Target.create "CopyBinaries" (fun _ ->
    !! "src/**/*.??proj"
    -- "src/**/*.shproj"
    |>  Seq.map (fun f -> ((Path.getDirectory f) </> "bin" </> configuration, "bin" </> (Path.GetFileNameWithoutExtension f)))
    |>  Seq.iter (fun (fromDir, toDir) -> Shell.copyDir toDir fromDir (fun _ -> true))
)

// --------------------------------------------------------------------------------------
// Clean build results

let buildConfiguration = DotNet.Custom <| Environment.environVarOrDefault "configuration" configuration

Target.create "Clean" (fun _ ->
    Shell.cleanDirs ["bin"; "temp"; "docs"]
)

// --------------------------------------------------------------------------------------
// Build library & test project

Target.create "Build" (fun _ ->
    solutionFile 
    |> DotNet.build (fun p -> 
        { p with
            Configuration = buildConfiguration })
)

// --------------------------------------------------------------------------------------
// Run the unit tests using test runner

Target.create "RunTests" (fun _ ->
    !! testAssemblies
    |> Expecto.run id
)

// --------------------------------------------------------------------------------------
// Build a NuGet package

Target.create "NuGet" (fun _ ->
    Paket.pack(fun p ->
        { p with
            OutputPath = "bin"
            Version = release.NugetVersion
            ReleaseNotes = String.toLines release.Notes})
)

Target.create "PublishNuget" (fun _ ->
    Paket.push(fun p ->
        { p with
            WorkingDir = "bin" })
)

// --------------------------------------------------------------------------------------
// Generate the documentation

//let fakePath = "packages" </> "FAKE.Core" </> "tools" </> "FAKE.exe"
//let fakeStartInfo script workingDirectory args fsiargs environmentVars =
//    (fun (info: ProcStartInfo) ->
//        { info with 
//            FileName = Path.GetFullPath fakePath
//            Arguments = sprintf "%s --fsiargs -d:FAKE %s \"%s\"" args fsiargs script
//            WorkingDirectory = workingDirectory }
//        |> Process.withFramework        
//        |> Process.setEnvironmentVariable "MSBuild" MSBuild.msBuildExe
//        |> Process.setEnvironmentVariable "GIT" CommandHelper.gitPath)

///// Run the given buildscript with FAKE.exe
//let executeFAKEWithOutput workingDirectory script fsiargs envArgs =
//    let exitCode = 
//        Process.execSimple
//            (fakeStartInfo script workingDirectory "" fsiargs envArgs)
//            TimeSpan.MaxValue
//    System.Threading.Thread.Sleep 1000
//    exitCode

// Documentation
//let buildDocumentationTarget fsiargs target =
//    Trace.trace (sprintf "Building documentation (%s), this could take some time, please wait..." target)
//    let exit = executeFAKEWithOutput "docsrc/tools" "generate.fsx" fsiargs ["target", target]
//    if exit <> 0 then
//        failwith "generating reference documentation failed"
//    ()

// Paths with template/source/output locations
let bin        = __SOURCE_DIRECTORY__ @@ "../../bin"
let content    = __SOURCE_DIRECTORY__ @@ "docsrc/content"
let output     = __SOURCE_DIRECTORY__ @@ "../../docs"
let files      = __SOURCE_DIRECTORY__ @@ "../files"
let templates  = __SOURCE_DIRECTORY__ @@ "docsrc/tools/templates"
let formatting = __SOURCE_DIRECTORY__ @@ "packages\FSharp.Formatting\\"
let docTemplate = "docpage.cshtml"

//Target.create "GenerateReferenceDocs" (fun _ ->
//    buildDocumentationTarget "-d:RELEASE -d:REFERENCE" "Default"


//    FSFormatting.createDocs (fun s ->
//        s)

//)

//let generateHelp fail debug =
//    let args =
//        if debug then "--define:HELP"
//        else "--define:RELEASE --define:HELP"
//    try
//        buildDocumentationTarget args "Default"
//        Trace.traceImportant "Help generated"
//    with
//    | e when fail ->
//        Trace.traceImportant "generating help documentation failed"
//        Trace.traceException e
//    | _ ->
//        Trace.traceImportant "generating help documentation failed"
        
//Target.create "GenerateDocs" (fun _ ->
//    File.delete "docsrc/content/release-notes.md"
//    Shell.copyFile "docsrc/content/" "RELEASE_NOTES.md"
//    Shell.rename "docsrc/content/release-notes.md" "docsrc/content/RELEASE_NOTES.md"

//    File.delete "docsrc/content/license.md"
//    Shell.copyFile "docsrc/content/" "LICENSE.txt"
//    Shell.rename "docsrc/content/license.md" "docsrc/content/LICENSE.txt"

//    let layoutRootsAll = new System.Collections.Generic.Dictionary<string, string list>()
//    layoutRootsAll.Add("en",[ templates; formatting @@ "templates"
//                              formatting @@ "templates/reference" ])
//    DirectoryInfo.getSubDirectories (DirectoryInfo.ofPath templates)
//    |> Seq.iter (fun d ->
//                    let name = d.Name
//                    if name.Length = 2 || name.Length = 3 then
//                        layoutRootsAll.Add(
//                                name, [templates @@ name
//                                       formatting @@ "templates"
//                                       formatting @@ "templates/reference" ]))

//    let subdirs =
//        [ content; ]
//    for dir in subdirs do
//        let sub = "." // Everything goes into the same output directory here
//        let langSpecificPath(lang, path:string) =
//            path.Split([|'/'; '\\'|], System.StringSplitOptions.RemoveEmptyEntries)
//            |> Array.exists(fun i -> i = lang)
//        let layoutRoots =
//            let key = layoutRootsAll.Keys |> Seq.tryFind (fun i -> langSpecificPath(i, dir))
//            match key with
//            | Some lang -> layoutRootsAll.[lang]
//            | None -> layoutRootsAll.["en"] // "en" is the default language

//        FSFormatting.createDocs (fun args ->
//            { args with
//                Source = content
//                OutputDirectory = output @@ sub
//                ToolPath = formatting
//                LayoutRoots = layoutRoots
//                Template = docTemplate }
//    )
//   // generateHelp true false
//)

//Target.create "GenerateHelpDebug" (fun _ ->
//    File.delete "docsrc/content/release-notes.md"
//    Shell.CopyFile "docsrc/content/" "RELEASE_NOTES.md"
//    Shell.Rename "docsrc/content/release-notes.md" "docsrc/content/RELEASE_NOTES.md"

//    File.delete "docsrc/content/license.md"
//    Shell.CopyFile "docsrc/content/" "LICENSE.txt"
//    Shell.Rename "docsrc/content/license.md" "docsrc/content/LICENSE.txt"

//    generateHelp true true
//)

//Target.create "KeepRunning" (fun _ ->
//    //use watcher = 
        
//    //    Fake.FileSystem.(!!) "docsrc/content/**/*.*" 
//    //    |> Fake.ChangeWatcher.WatchChanges (fun changes ->
//    //     generateHelp' true true
//    //)

//    //Trace.traceImportant "Waiting for help edits. Press any key to stop."

//    //System.Console.ReadKey() |> ignore

//    //watcher.Dispose()
//    ()
//)

//let createIndexFsx lang =
//    let content = """(*** hide ***)
//// This block of code is omitted in the generated HTML documentation. Use
//// it to define helpers that you do not want to show in the documentation.
//#I "../../../bin"

//(**
//F# Project Scaffold ({0})
//=========================
//*)
//"""
//    let targetDir = "docsrc/content" </> lang
//    let targetFile = targetDir </> "index.fsx"
//    Directory.ensure targetDir
//    File.WriteAllText(targetFile, System.String.Format(content, lang))

//Target.create "AddLangDocs" (fun _ ->
//    let args = System.Environment.GetCommandLineArgs()
//    if args.Length < 4 then
//        failwith "Language not specified."

//    args.[3..]
//    |> Seq.iter (fun lang ->
//        if lang.Length <> 2 && lang.Length <> 3 then
//            failwithf "Language must be 2 or 3 characters (ex. 'de', 'fr', 'ja', 'gsw', etc.): %s" lang

//        let templateFileName = "template.cshtml"
//        let templateDir = "docsrc/tools/templates"
//        let langTemplateDir = templateDir </> lang
//        let langTemplateFileName = langTemplateDir </> templateFileName

//        if File.Exists(langTemplateFileName) then
//            failwithf "Documents for specified language '%s' have already been added." lang

//        Directory.ensure langTemplateDir
//        Shell.Copy langTemplateDir [ templateDir </> templateFileName ]

//        createIndexFsx lang)
//)

// --------------------------------------------------------------------------------------
// Release Scripts

//#load "paket-files/fsharp/FAKE/modules/Octokit/Octokit.fsx"

//open Octokit

//Target.create "Release" (fun _ ->
//    let user =
//        match environVarOrDefault "github-user" String.Empty with
//        | s when not (String.IsNullOrWhiteSpace s) -> s
//        | _ -> Fake.UserInputHelper.getUserInput "Username: "
//    let pw =
//        match environVarOrDefault "github-pw" String.Empty with
//        | s when not (String.IsNullOrWhiteSpace s) -> s
//        | _ -> Fake.UserInputHelper.getUserPassword "Password: "
//    let remote =
//        CommandHelper.getGitResult "" "remote -v"
//        |> Seq.filter (fun (s: string) -> s.EndsWith("(push)"))
//        |> Seq.tryFind (fun (s: string) -> s.Contains(gitOwner + "/" + gitName))
//        |> function None -> gitHome + "/" + gitName | Some (s: string) -> s.Split().[0]

//    Staging.stageAll ""
//    Commit.exec "" (sprintf "Bump version to %s" release.NugetVersion)
//    Branches.pushBranch "" remote (Information.getBranchName "")

//    Branches.tag "" release.NugetVersion
//    Branches.pushTag "" remote release.NugetVersion

//    // release on github
//    createClient user pw
//    |> createDraft gitOwner gitName release.NugetVersion (release.SemVer.PreRelease <> None) release.Notes
//    // TODO: |> uploadFile "PATH_TO_FILE"
//    |> releaseDraft
//    |> Async.RunSynchronously
//)

Target.create "BuildPackage" ignore 
// --------------------------------------------------------------------------------------
// Run all targets by default. Invoke 'build <Target>' to override


Target.create "GenerateReferenceDocs" ignore 

Target.create "All" ignore

"Clean"
  ==>"AssemblyInfo"
  ==> "Build"
  ==> "CopyBinaries"
  ==> "RunTests"
  //==> "GenerateReferenceDocs"
  //==> "GenerateDocs"
  ==> "NuGet"
  ==> "BuildPackage"
  ==> "All"

//"GenerateReferenceDocs"
//  ==> "GenerateDocs"

//"GenerateHelpDebug"
//  ==> "KeepRunning"

//"Clean"
//  ==> "Release"

"BuildPackage"
  ==> "PublishNuget"
  //==> "Release"

Target.runOrDefault "All"
