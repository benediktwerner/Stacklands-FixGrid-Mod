# Stacklands FixGrid Mod

Adjusts the card alignment grid (when pressing E to align card) so that it always perfectly aligns with the map
meaning you'll never have cards trying to align into the wall and jump around because they can't again.

Specifically, it increases the space between cards in the grid if necessary to make it align.

It also adds configuration options for the minimum size of the grid and by default slightly decreases the minimal horizontal distance between
cards.

On the island, the visible grid can't easily be made to align properly so it will be hidden. The cards will still align to the
correct position. If you prefer, you can instead turn the alignment fix off for the island in the config. In that case, the grid
size will still be adjusted according to the configuration but the grid won't be perfectly aligned to the edge of the board.

## Development

- Build using `dotnet build`
- For release builds, add `-c Release`
- If you're using VSCode, the `.vscode/tasks.json` file allows building via `Run Build`/`Ctrl+Shift+B`

## Links

- Github: https://github.com/benediktwerner/Stacklands-FixGrid-Mod
- Steam Workshop: https://steamcommunity.com/sharedfiles/filedetails/?id=3012092960

## Changelog

- v1.1.1: Don't unpatch when exiting the game
- v1.1.0: Steam Workshop Support
- v1.0.2:
  - Compatibility with OffGrid and ToggleGrid mods
- v1.0.1:
  - Only snap cards on current board to grid
  - Fix island grid alignment (requires hiding the grid there, can be turned off in the config)
- v1.0: Initial release
