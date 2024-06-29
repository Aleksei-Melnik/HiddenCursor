# HiddenCursor Source Code

This repository contains the source code for the HiddenCursor application, a Windows utility that hides the mouse cursor in the bottom-right corner of the screen after a period of inactivity and shows it again when the mouse moves. This feature is activated only when SteamVR is running.

## Features
- Automatically hides the cursor in the bottom-right corner of the screen after 3 seconds of inactivity.
- Automatically shows the cursor when the mouse moves.
- Activates only when SteamVR is running.

## Requirements
- Windows 10 or newer.
- .NET Framework 4.7.2 or newer.
- SteamVR installed and configured.

## Installation
1. Clone the repository:
    ```sh
    git clone https://github.com/Aleksei-Melnik/HiddenCursor.git
    ```

2. Open the solution in Visual Studio.

3. Build the solution in `Release` mode.

4. The executable will be located in the `bin\Release` directory.

## Usage
1. Launch SteamVR.
2. Run the HiddenCursor executable.
3. The cursor will be hidden in the bottom-right corner of the screen after 3 seconds of inactivity and will reappear when the mouse moves.

## Code Overview
The main functionality is implemented in the `Program.cs` file, which includes:
- A timer that checks mouse activity every 3 seconds.
- Functions to get and set the cursor position using `user32.dll` functions.
- A function to check if SteamVR is running.

## Contributing
If you would like to contribute to this project, please fork the repository and create a pull request with your changes. Make sure to follow the coding style and include relevant tests.

## Support
If you encounter any issues or have questions, please create an issue on the GitHub repository or contact us directly:
- **GitHub**: [Aleksei-Melnik](https://github.com/Aleksei-Melnik)
- **Email**: melnik-alex@outlook.com
