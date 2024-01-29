# Weighted Probability Table (for Unity)

## What is it?
The easiest way to explain what a weighted probability table is is to reference a term that you're probably already familiar with if you're into video games, which is "loot table". A weighted probability table is essentially just a generic loot table that can be used for all sorts of purposes besides loot.

Some examples of systems that you could implement using weighted probability tables are:
- Randomized character upgrades, such as in a Vampire Survivors-like game
- Rolling shops, such as in an auto-battle game
- Card packs in a collectible card game

## Features
### Weights
Each entry in the table has a weight which determines how likely it is to be selected. The chance that a given entry is selected is calculated by dividing its weight by the total weight of all entries in the table.

### Weight Modifiers
The weight of each entry in the table can be modified dynamically using weight modifiers. Weight modifiers can leverage any data in your game that is available to them, so they're extremely flexible. One example of how you might use them is to create a weight modifier that increases the chance that a rare item (or upgrade, card, whatever) is selected based on how long it's been since the player recieved an item of that rarity. This helps soften the downside of randomization and minimizes the chance that a player has a significantly worse experience than average due to unfavorable RNG.

### Sampling Modes
There are three different sampling modes that are supported:
- **Sample With Replacement** - Select an entry from the table and keep it in the table after selection (i.e. "re-place" it). This is useful if you always want items in your table to have the same chance of being selected.
- **Sample Without Replacement** - Select an entry from the table and remove it from the table after selection. This is useful if you want to make sure that all items in the table are selected before repeating.
- **Sample With Weight Decay** - Select an entry from the table and decay its weight after selection so it has a lower chance of being selected in the future. This is useful if you want some or all items in the table to be selected a certain number of times before repeating while minimizing the chance that the same item is selected consecutively.

### Selection Limits
Selection limits can be defined for some or all entries in the table when using the **Sample With Weight Decay** sampling method. There are two types of selection limits:
- **Selections Per Cycle** - The number of times an entry can be selected before the table is exhausted (i.e. all items in the table have been selected the maximum number of times).
- **Selections Total** - The number of times an entry can be selected from the table for the lifetime of the table.

In both cases, selecting an entry with a selection limit specified will decay the weight of that entry proprtionally. For example, if an entry can be selected 4 times per cycle then selecting it will reduce its weight to 75% of its original weight. Subsequent selections will further reduce it to 50%, 25% and then 0% of the original weight. Once the table completes a cycle, the weight is reset back to its original value.

### Hierarchical Tables
You can nest tables within tables to create a hierarchical table structure which can be used for all sorts of purposes such as a loot box that contains multiple reward types.

For example, in a third-person shooter you might have unlockable characters, weapons, weapon skins, character skins, as well as a currency for purchasing rewards from a shop, and after each match the player recieves one (or more) of these rewards. You could implement this using hierarchical tables by creating separate tables for each reward type and nesting them within a higher level "Rewards" table that's used to select the player's reward(s) from.

The advantage to doing this over creating a single table that supports all reward types is that you can set the weights, weight modifiers, and selection limits of the tables themselves rather than the individual items within the tables. This enables you to do things like guarantee that the player gets at least one new character for every N boxes they open.

## Installation
1. Download or clone the code in this repo to your Unity project
2. If you'd like to use the included unit tests you'll need to install the Moq package from the Unity Package Manager. If not, just delete the Test directory and skip to step X
3. In Unity, select Window > Package Manager from the menu bar
4. Press the + icon at the top left of the Package Manager window and select 'Add package by name...'
5. Enter 'nuget.moq' in the Name field and press Add

## Examples
There are 4 examples included in the Examples directory which illustrate how to use weighted probability tables in various ways. I think it should give you a clear idea about how you can use the code for your own purposes, but if you have questions feel free to contact me (see contact info below).

If you don't need these examples, feel free to delete the Examples directory.

## Testing
The Test directory contains a source file with a whole bunch of unit tests. You can run these unit tests using the Test Runner tool in Unity. To do this, just follow these steps:
1. In Unity, select Window > General > Test Runner from the menu bar
2. In the Test Runner window, you should see a list of tests under GLHFStudios.Utility.WeightedProbabilityTable.Test
3. Select the tests you want to run and press Run Selected, or just simply press Run All

Note that there are a couple of tests which check that the probability of selecting a given item based on its weights is correct. These tests use what's called a Chi-Squared test, which fails some percentage of the time due to the probabilistic nature of what's being tested. If you see a test ending in "ProbabilityIsCorrect" fail, try running the again to see if it passes. If it's a one-off failure then it's nothing to worry about, but if it fails consistently then that indicates an issue.

## Author & Contact
[Alex Passeroni](https://alexpasseroni.com) (apasseroni1992@gmail.com)

## Questions
If you have any questions about how to use this code please feel free to reach out to me and I will get back to you as soon as I can!