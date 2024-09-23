using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EncounterCounter
{
    public partial class MainWindow : Window
    {
        private const string ApiBaseUrl = "https://pokeapi.co/api/v2/pokemon/";
        private const string SaveFilePath = "pokemon_data.json";
        private List<PokemonData> allPokemonList; // Store all Pokémon data here
        private PokemonData currentPokemonData;
        private Dictionary<string, PokemonData> savedData = new Dictionary<string, PokemonData>();
        private bool isShiny = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadAllPokemonData(); // Load all Pokémon from Gen 1 to 5
            LoadEncounterData();

            // Subscribe to the Closing event to save data before the application closes
            this.Closing += MainWindow_Closing;
        }

        // This method gets triggered when the application is about to close
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveEncounterData(); // Save data on close
        }

        // Load all Pokémon data for Gen 1 to 5 at once (no need to fetch from API multiple times)
        private async void LoadAllPokemonData()
        {
            allPokemonList = new List<PokemonData>();

            for (int id = 1; id <= 649; id++) // Load Gen 1 to Gen 5 Pokémon
            {
                var pokemon = await GetPokemonAsync(id);
                allPokemonList.Add(pokemon);
            }
        }

        // Search Pokémon by name when user types in the search bar
        private void PokemonSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(pokemonSearchBox.Text))
            {
                return;
            }

            string searchQuery = pokemonSearchBox.Text.ToLower();
            List<PokemonData> filteredList = allPokemonList.FindAll(p => p.Name.ToLower().StartsWith(searchQuery));

            // Clear the ListBox and populate with filtered Pokémon
            pokemonListBox.Items.Clear();
            foreach (var pokemon in filteredList)
            {
                pokemonListBox.Items.Add(pokemon);
            }
        }

        // Fetch Pokémon data from PokéAPI (this is used once per Pokémon)
        private async Task<PokemonData> GetPokemonAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(ApiBaseUrl + id);
                var pokemon = JsonConvert.DeserializeObject<PokemonData>(response);

                // Assign the icon URL for the Pokémon's sprite
                pokemon.Icon = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png";
                return pokemon;
            }
        }

        // Handle the selection change in the Pokémon list
        private void PokemonListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pokemonListBox.SelectedItem != null)
            {
                currentPokemonData = (PokemonData)pokemonListBox.SelectedItem;
                currentPokemonLabel.Content = $"Currently hunting: {currentPokemonData.Name}";

                var bitmap = new BitmapImage(new Uri(currentPokemonData.Icon));
                selectedPokemonIcon.Source = bitmap;

                // Load previous encounter and shiny data if it exists
                if (savedData.ContainsKey(currentPokemonData.Name))
                {
                    currentPokemonData = savedData[currentPokemonData.Name];
                    encounterCountLabel.Content = $"Encounters: {currentPokemonData.Encounters}";
                    shinyButton.Content = currentPokemonData.IsShiny ? "Unmark Shiny" : "Mark as Shiny";
                    selectedPokemonIcon.Opacity = currentPokemonData.IsShiny ? 0.5 : 1.0;
                }
                else
                {
                    // Reset if no previous data
                    encounterCountLabel.Content = "Encounters: 0";
                    shinyButton.Content = "Mark as Shiny";
                    selectedPokemonIcon.Opacity = 1.0;
                }
            }
        }

        // Button click event to add an encounter
        private void AddEncounterButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPokemonData == null) return;

            currentPokemonData.Encounters++;
            encounterCountLabel.Content = $"Encounters: {currentPokemonData.Encounters}";
            SaveEncounterData();
        }

        // Button click event to mark as shiny
        private void ShinyButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPokemonData == null) return;

            currentPokemonData.IsShiny = !currentPokemonData.IsShiny;
            shinyButton.Content = currentPokemonData.IsShiny ? "Unmark Shiny" : "Mark as Shiny";

            selectedPokemonIcon.Opacity = currentPokemonData.IsShiny ? 0.5 : 1.0;
            SaveEncounterData();
        }

        // Load encounter data from JSON
        private void LoadEncounterData()
        {
            if (File.Exists(SaveFilePath))
            {
                var jsonData = File.ReadAllText(SaveFilePath);
                savedData = JsonConvert.DeserializeObject<Dictionary<string, PokemonData>>(jsonData) ?? new Dictionary<string, PokemonData>();
            }
        }

        // Save encounter and shiny data to JSON
        private void SaveEncounterData()
        {
            if (currentPokemonData != null)
            {
                savedData[currentPokemonData.Name] = currentPokemonData;
            }

            var jsonData = JsonConvert.SerializeObject(savedData, Formatting.Indented);
            File.WriteAllText(SaveFilePath, jsonData);
        }
    }

    // Define the structure for Pokémon Data
    public class PokemonData
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Encounters { get; set; }
        public bool IsShiny { get; set; }
    }
}
