# Stacklands FixGrid Mod

Adjusts the card alignment grid (when pressing E to align card) so that it always perfectly aligns with the map
meaning you'll never have cards trying to align into the wall and jump around because they can't again.

Specifically, it increases the space between cards in the grid if necessary to make it align.

It also adds configuration options for the minimum size of the grid and by default slightly decreases the minimal horizontal distance between
cards. You can change the configuration via the Mod Manager or by editing the `BepInEx/config/de.benediktwerner.stacklands.FixGrid.cfg` file
which will be generated after starting the game with the mod installed for the first time.

On the island, the visible grid can't easily be made to align properly so it will be hidden. The cards will still align to the
correct position. If you prefer, you can instead turn the alignment fix off for the island in the config. In that case, the grid
size will still be adjusted according to the configuration but the grid won't be perfectly aligned to the edge of the board.

## Manual Installation

This mod requires BepInEx to work. BepInEx is a modding framework which allows multiple mods to be loaded.

1. Download and install BepInEx from the [Thunderstore](https://stacklands.thunderstore.io/package/BepInEx/BepInExPack_Stacklands/).
2. Download this mod and extract it into `BepInEx/plugins/`
3. Launch the game

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

- v1.0.2:
  - Compatibility with OffGrid and ToggleGrid mods
- v1.0.1:
  - Only snap cards on current board to grid
  - Fix island grid alignment (requires hiding the grid there, can be turned off in the config)
- v1.0: Initial release
