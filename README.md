# Locations Sql Code Generator
Overview
This project is designed to generate SQL scripts that create global location data in a custom table structure. The core objective is to generate SQL INSERT statements that define a hierarchical structure of countries, states, and cities based on a dynamic source of geographical information. This data can be customized based on the requirements of specific projects and used to populate any location-related database tables.

We are loading datas from the following github repo
https://github.com/dr5hn/countries-states-cities-database
Thanks to @dr5hn

Key Features:
Retrieves country, state, and city data from a publicly available JSON source.

Generates SQL INSERT statements for each country, state, and city, maintaining a parent-child relationship.

Customizable table structures for flexible integration into your projects.

Outputs SQL scripts that can be directly used to populate databases.

Supports dynamic hierarchical location data.

Requirements:
.NET 5.0 or higher

C# programming language

Access to the internet to retrieve the JSON data

How to Use:
Clone this repository to your local machine.

Ensure you have a .NET environment set up.

Run the application to generate the SQL script for creating location data.

The generated SQL file (locations.sql) will contain INSERT statements for countries, states, and cities.

Modify the script as needed to fit your custom database structure.

Sample Output:

INSERT INTO "Locations" ("Id", "Title", "ParentId", "CreateDate", "IsDelete") 
VALUES (1, 'United States', NULL, NOW(), FALSE);

INSERT INTO "Locations" ("Id", "Title", "ParentId", "CreateDate", "IsDelete") 
VALUES (2, 'California', 1, NOW(), FALSE);

INSERT INTO "Locations" ("Id", "Title", "ParentId", "CreateDate", "IsDelete") 
VALUES (3, 'Los Angeles', 2, NOW(), FALSE);
License:
This project is open source and available under the MIT License.

Contributing:
Feel free to open an issue if you encounter any bugs or want to suggest improvements. Pull requests are welcome!
