

<img src="Hangman_Graphics/InGame Hangman Animation/phase5.png" alt="" width="220"><img src="Hangman_Graphics/Main Menu Video Project/Hangman_text.png" alt="" width="528">

<br>

# General Presentation
This repository represents the work I've done for one of the 3 assessments at Visual Programming Environments, in 2019, at Transylvania University of Brașov, Romania.

## Purpose
The purpose of this assessment was to implement a windows Hangman videogame, using WPF and C#. The game needed to have additional features besides the game itself, like: user profiles management, data persistence, and statistics.

## Technologies and dependencies
For the implementation of this project I've used the following technologies and dependencies:
- **Visual Studio**: IDE
- **C#**: Programming language
- **[.NET Framework WPF](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/?view=netframeworkdesktop-4.8)**: A free and open-source graphical subsystem originally developed by Microsoft for rendering user interfaces in Windows-based applications

<br>

# Project Presentation

## User Interface
### Menus
|<img src="showcase/main_menu.png" alt=""><br>Main Menu|<img src="showcase/statistics.png" alt=""><br>Statistics|
|:-:|:-:|
|<img src="showcase/profile_creation.png" alt=""><br>**Profile Creation**|<img src="showcase/profile_selection.png" alt=""><br>**Profile Selection**|
|<img src="showcase/categories_selection.png" alt=""><br>**Categories Selection**|<img src="showcase/game.png" alt=""><br>**Game**|

<br>

### Dialog Boxes
|<img src="showcase/game_save.png" alt="" width="1200px"><br>Save Game|<img src="showcase/game_continue.png" alt="" width="1200px"><br>Continue Game|
|:-:|:-:|
|<img src="showcase/game_won.png" alt=""><br>**Won Game**|<img src="showcase/game_lost.png" alt=""><br>**Lost Gmae**|

## Features
Besides the main hangman game functionality, the user has the benefit of additional features, like:
- **Personalizing his own profile:** When creating a new profile, the user has the possibility to choose a name and a profile icon
- **Saving & resuming game sessions:** When exiting the game, the user can save his current session, just in order to resume it later on
- **Consulting statistics:** The user can consult statistics, comparing his profile with other profiles on the same computer

## Assets
The videogame has numerous audio-visual elements, like:
- **Animated main menu**
![](showcase/main_menu.gif)
-  **Profile icons**

|![](Hangman/Resources/ProfilePictures/1.png)|![](Hangman/Resources/ProfilePictures/2.png)|![](Hangman/Resources/ProfilePictures/3.png)|![](Hangman/Resources/ProfilePictures/4.png)|![](Hangman/Resources/ProfilePictures/5.png)|![](Hangman/Resources/ProfilePictures/6.png)|![](Hangman/Resources/ProfilePictures/7.png)|![](Hangman/Resources/ProfilePictures/8.png)|![](Hangman/Resources/ProfilePictures/9.png)|![](Hangman/Resources/ProfilePictures/10.png)|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|![](Hangman/Resources/ProfilePictures/11.png)|![](Hangman/Resources/ProfilePictures/12.png)|![](Hangman/Resources/ProfilePictures/13.png)|![](Hangman/Resources/ProfilePictures/14.png)|![](Hangman/Resources/ProfilePictures/15.png)|![](Hangman/Resources/ProfilePictures/16.png)|![](Hangman/Resources/ProfilePictures/17.png)|![](Hangman/Resources/ProfilePictures/18.png)|![](Hangman/Resources/ProfilePictures/19.png)|![](Hangman/Resources/ProfilePictures/20.png)|
|![](Hangman/Resources/ProfilePictures/21.png)|![](Hangman/Resources/ProfilePictures/22.png)|![](Hangman/Resources/ProfilePictures/23.png)|![](Hangman/Resources/ProfilePictures/24.png)|![](Hangman/Resources/ProfilePictures/25.png)|![](Hangman/Resources/ProfilePictures/26.png)|![](Hangman/Resources/ProfilePictures/27.png)|![](Hangman/Resources/ProfilePictures/28.png)|![](Hangman/Resources/ProfilePictures/29.png)|![](Hangman/Resources/ProfilePictures/30.png)|

- **Phase-based animated levels**

|![P0](Hangman_Graphics/InGame%20Hangman%20Animation/phase0.png)|![P1](Hangman_Graphics/InGame%20Hangman%20Animation/phase1.png)|![P2](Hangman_Graphics/InGame%20Hangman%20Animation/phase2.png)|![P3](Hangman_Graphics/InGame%20Hangman%20Animation/phase3.png)|
|:-:|:-:|:-:|:-:|
|![P4](Hangman_Graphics/InGame%20Hangman%20Animation/phase4.png)|![P5](Hangman_Graphics/InGame%20Hangman%20Animation/phase5.png)|![P6](Hangman_Graphics/InGame%20Hangman%20Animation/phase6.png)||

## Project Structure
|Structure|Details|
|:-|:-|
|<img src="showcase/project_structure.png" alt="" width="300px">|- **Commands**: WPF Commands<br>- **Models**: used for data manipulation and OOP purposes<br>- **Resources**: static resources and assets used along the project<br>- **ViewModels**: WPF ViewModels where most of the logic is implemented<br>- **Views**: UI views|
