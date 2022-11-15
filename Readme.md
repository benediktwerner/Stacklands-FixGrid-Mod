# Stacklands FixGrid Mod

Adjusts the card alignment grid (when pressing E to align card) so that it always perfectly aligns with the map
meaning you'll never have cards trying to align into the wall and jump around because they can't again.

Specifically, it increases the space between cards in the grid if necessary to make it align.

It also adds configuration options for the minimum size of the grid and by default slightly decreases the minimal horizontal distance between
cards. You can change the configuration via the Mod Manager or by editing the `BepInEx/config/de.benediktwerner.stacklands.FixGrid.cfg` file
which will be generated after starting the game with the mod installed for the first time.

## Manual Installation
This mod requires BepInEx to work. BepInEx is a modding framework which allows multiple mods to be loaded.

1. Download and install BepInEx from the [Thunderstore](https://stacklands.thunderstore.io/package/BepInEx/BepInExPack_Stacklands/).
4. Download this mod and extract it into `BepInEx/plugins/`
5. Launch the game

## Development
1. Install BepInEx
2. This mod uses publicized game DLLs to get private members without reflection
   - Use https://github.com/CabbageCrow/AssemblyPublicizer for example to publicize `Stacklands/Stacklands_Data/Managed/GameScripts.dll` (just drag the DLL onto the publicizer exe)
   - This outputs to `Stacklands_Data\Managed\publicized_assemblies\GameScripts_publicized.dll` (if you use another publicizer, place the result there)
3. Compile the project. This copies the resulting DLL into `<GAME_PATH>/BepInEx/plugins/`.
   - Your `GAME_PATH` should automatically be detected. If it isn't, you can manually set it in the `.csproj` file.
   - If you're using VSCode, the `.vscode/tasks.json` file should make it so that you can just do `Run Build`/`Ctrl+Shift+B` to build.

## Links
- Github: https://github.com/benediktwerner/Stacklands-FixGrid-Mod
- Thunderstore: https://stacklands.thunderstore.io/package/benediktwerner/FixGrid

## Changelog

- v1.0: Initial release
