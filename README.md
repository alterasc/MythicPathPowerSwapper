# Template project for modding Pathfinder: Wrath of the Righteous

Includes 
- Publicize task
- On build event to copy your built mod into Mods folder.
- On release build event to zip your built mod and put it into PublishOutput folder

## Setup 

0. Install UMM if you don't have it.
1. Set `WrathPath` environment variable to point at game folder
2. Rename stuff    
    Do full text replacement (for example using Visual Studio Code or Visual Studio)
    - `MythicPathPowerSwapper` -> `Your project name`
    - `AlterAsc` -> `Your name`    
    
    Rename `MythicPathPowerSwapper.csproj` and `MythicPathPowerSwapper.sln` to match your mod name.
3. Open in Visual Studio.
4. Click on Solution -> Restore Nuget packages
5. Do Build -> Clean Solution.
6. Write your code
7. Don't forget to change this readme.
8. Set your homepage and repository in Info.json
