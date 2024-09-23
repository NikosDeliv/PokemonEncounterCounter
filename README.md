# Pokémon Encounter Counter
The reason it has Pokemon only from Gen 1 to 5 is because I made this mainly for PokeMMO. This is a simple Pokémon encounter counter application built using C#, which allows users to track encounters while hunting for shiny Pokémon. The app fetches Pokémon data (Gen 1 to 5) from the PokéAPI, displays their names and icons, and lets users add encounters and mark Pokémon as shiny. The encounter data is saved automatically when the application is closed. For the exe just unzip the .rar file and you're good to go.

## Features

1. **Pokémon Search**: Search for Pokémon by name (Gen 1 to 5).
2. **Encounter Tracking**: Add encounters and view the total count for the selected Pokémon.
3. **Shiny Indicator**: Mark a Pokémon as shiny, which is visually indicated.
4. **Persistence**: Encounter counts and shiny status are saved automatically and loaded when the application restarts.
5. **Pokémon Data Fetching**: Pokémon names and icons are fetched from the PokéAPI.

## Installation

Navigate to the project directory and build the solution using Visual Studio or Visual Studio Code with the required C# extensions.

If you prefer to use the precompiled executable, unzip the provided archive and run the `.exe` file. Make sure all the contents in the zip are extracted into the same directory.

## Usage

1. Launch the application.
2. Use the search bar to find a Pokémon by name (for example, "Pikachu").
3. Select the Pokémon from the search results.
4. Use the **Add Encounter** button to increase the encounter count.
5. If you find a shiny, press **Mark as Shiny** to mark the Pokémon.
6. Your progress (encounters and shiny status) is automatically saved when you close the application.

## Requirements

- .NET 6.0 or higher
- Internet connection for fetching Pokémon data from the PokéAPI

## How Encounter Data is Saved

Encounter data and shiny status are saved locally in a `pokemon_data.json` file, which is automatically created and updated in the same directory as the executable.

## Known Issues

- Icons sometimes may not load immediately due to network latency. Reload the Pokémon if this occurs.

## Screenshots


 ![Στιγμιότυπο οθόνης (696)](https://github.com/user-attachments/assets/617c087d-f62c-4ff2-a804-047b0572dbec)
 
 ![Στιγμιότυπο οθόνης (697)](https://github.com/user-attachments/assets/536bb924-cc43-43cb-b409-5d178ae30bcf)
 
 ![Στιγμιότυπο οθόνης (698)](https://github.com/user-attachments/assets/9a9695f4-a24c-4dd2-8eaf-6925ef5b8e3a)
 
 ![Στιγμιότυπο οθόνης (699)](https://github.com/user-attachments/assets/1f4fd195-24a0-4f62-881e-1483f1be3751)
