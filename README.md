### Drinks Project by C# Academy

Project Link: https://www.thecsharpacademy.com/project/15/drinks

## Project Requirements

- You were hired by restaurant to create a solution for their drinks menu.
- Their drinks menu is provided by an external company. All the data about the drinks is in the companies database, accessible through an API.
- Your job is to create a system that allows the restaurant employee to pull data from any drink in the database.
- You don't need SQL here, as you won't be operating the database. All you need is to create an user-friendly way to present the data to the users (the restaurant employees)
- When the users open the application, they should be presented with the Drinks Category Menu and invited to choose a category. Then they'll have the chance to choose a drink and see information about it.
- When the users visualise the drink detail, there shouldn't be any properties with empty values

## Additional Challenges

  - n/a

## Lessons Learned

- First I needed to learn how to send HTTP requests from C# application. During this process, I discovered a convenient library for handling HTTP calls. While the library supports direct deserialization I did not use it so I can try to do it myself.
	
- Before starting the project, I built a standalone application and experimented with the JSONPlaceholder API to make my first API calls and practice deserialization. This helped me understand how to retrieve, read, and deserialize responses to ensure valid data.

- I also explored reflection to map drink details into a reduced object containing only the most important information. While this didn’t significantly enhance the application’s functionality, it served as a useful introduction to reflection and helped streamline relevant data for the user.

## Areas for Improvement

  - A major limitation is that the free API provides a maximum of 100 results. Additionally, it does not support pagination (or at least, I couldn’t find documentation confirming that it does), which restricts the amount of accessible data.

## Main Resources Used
Drink API: https://www.thecocktaildb.com/api.php

JSONPlaceholder API: https://jsonplaceholder.typicode.com/

RestSharp documentation: https://restsharp.dev/docs/intro

Various StackOverFlow posts and MS Documentation
