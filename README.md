# Chess: A WPF Chess Game

## About the Project
This is a chess game developed using WPF, with XAML for the frontend and C# for the backend. It supports two-player mode and has plans for implementing a single-player mode with AI opponents.

## Getting Started
To play Chess, follow these steps:
1. Click on ChessApp.zip and download the zip file and extract the zip file by right clicking on the file and selecting 'extract all...'.
2. Once the zip file has been extracted, open the folder of the extracted file.
3. Open the WpfApp1 application file and the app will open on the chess board.
4. If a prompt pops up saying that this app could harm you pc, just click on 'more info' and then click on run anyway, the application is safe to run.

## Features
- Highlights the squares that a piece can move to.
- Local 2 player.

![ChessMaster Screenshot](https://github.com/reinert-droid/CSharpChessWPF/blob/main/ChessGameScreenshot.png)

## Technology Stack
- WPF v6.0.2
- C# v11.0
- .NET v7.0

## Project Structure
- ChessGameBackend| WpfApp1/ |-MainWindow.xaml.cs |-King.cs |-Queen.cs |-Bishop.cs |-Knight.cs |-Rook.cs |-Pawn.cs
- ChessGameFrontend| WpfApp1/ |MainWindow.xaml

## Known Issues and Roadmap
### Known Issues
- Sometimes possible blocks that the other player can move to gets highlighted when clicking on one of that player's pieces even though it is not that player's turn.

### Future Plans
- Implement AI opponents using MiniMax algorithm.
- Implement an online multiplayer function.
- Improve overall user interface to be more engaging and to look better.

## Acknowledgments
- reinert-droid

---
By Reinert
