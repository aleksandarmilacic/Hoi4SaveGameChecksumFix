# HOI4 Save File Integrity Tool

This utility updates the checksum and hash for Hearts of Iron IV (HOI4) save files. It's designed for developers or modders who need to ensure their modified save files maintain integrity, allowing them to load in Ironman mode without issues. Additionally, it provides a way to update the save file with a new Steam ID.

## Prerequisites

- .NET Runtime
- The tool is intended for use on Windows, but can be run on Linux and macOS using the .NET 8 runtime.

## Usage

To use this tool, you must provide two arguments: the path to the save file and the Steam ID as a long integer. The command format is as follows:

```
<program> <file_path> <steam_ID>
```

Example:

```
SaveFileIntegrityTool.exe "C:\HOI4\saves\savegame.hoi4" 76561198123456789
```

Ensure you replace `"C:\HOI4\saves\savegame.hoi4"` with the path to your HOI4 save file and `76561198123456789` with your actual Steam ID.

## Features

- **Checksum and Hash Update**: Recalculates and updates the save file's checksum and MD5 hash based on the content and provided Steam ID.
- **Format Detection**: Automatically detects whether the save file is in binary or text format.
- **Error Handling**: Provides user-friendly error messages for common issues, such as missing file or incorrect arguments.

## How It Works

The tool reads the save file, calculates a new checksum using the Fletcher's checksum algorithm, and then generates a new MD5 hash that combines this checksum, the file's length, and the provided Steam ID. It then updates the save file with this new hash, ensuring the game recognizes the file as valid and uncorrupted.

## Limitations

- This tool is designed specifically for HOI4 save files and might not work with other games or formats.
- Manipulating save files can lead to unexpected behavior in-game. Always back up your save files before using this tool.

## Contributing

Contributions to the tool are welcome! Please feel free to fork the repository, make your changes, and submit a pull request.

## License

This tool is released under the MIT License. 

## Disclaimer

This tool is not affiliated with or endorsed by the developers of Hearts of Iron IV or Paradox Interactive. Use it at your own risk.