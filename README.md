# Contacts_ASP.NET_MVC
CRUD desenvolvido em ASP.NET Core MVC com EF (Entity Framework) e banco SQL Server (mssql).

## Description
CRUD de contatos, onde é possível adicionar, listar, atualizar e remover os contatos.

## Installation
- SQL Server
	- Baixar o Microsoft SQL Server Management Studio caso não tenha.

- Visual Studio
	- Baixar e instalar o Visual Studio (caso não tenha, recomendado: 2022).
	- Baixar o projeto.
	- Extrair o projeto e abri-lo no Visual Studio.
	- Em "appsettings.json" atualizar os valores da config com os dados do seu banco de dados SQL Server (Server, User Id, Password).
   	- O passo a passo abaixo é encontrado também no arquivo AddMigrations.txt
		- Abrir Tools > NuGet Package Manager > Package Manager Console.
		- Entrar com o comando "add-migration ContactsManagerDB -context DBContext"
			- Espere realizar a criação do Migrations.
		- Entrar com o comando "update-database"
			- Pronto, o database foi criado em seu SQL Server Management Studio.

## Usage
- Compilar o projeto no Visual Studio
	- Clean e Rebuild.
- Rodar o projeto.
