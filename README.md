# RAG using .NET

This project is a simple Retrieval-Augmented Generation (RAG) application built with .NET. It uses OpenAI embeddings and chat models together with MongoDB vector search to answer questions about Sweet Crumbs Bakery.

## Features

- Ask questions in natural language about the bakery
- Uses embeddings to retrieve relevant bakery information
- Uses OpenAI chat models to generate helpful answers
- Stores and searches bakery data in MongoDB

## Technologies Used

- .NET 10 / C#
- OpenAI .NET SDK
- MongoDB.Driver
- MongoDB Atlas Vector Search
- RAG architecture

## Prerequisites

Before running the app, make sure you have:

- .NET 10 SDK installed
- An OpenAI API key
- A MongoDB instance with vector search support (MongoDB Atlas is recommended)

## Setup

### 1. Clone the repository

```bash
git clone "https://github.com/vishnupriyacvijayan/Rag-using-dotnet.git"
cd Rag-using-dotnet
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Set environment variables

On Windows PowerShell:

```powershell
$env:OPENAI_API_KEY="your_openai_api_key"
$env:MONGO_URI="mongodb+srv://<username>:<password>@<cluster-url>/"
```

> The app reads these values from the environment at runtime.

### 4. Prepare MongoDB

The app uses:

- Database: `BakeryDb`
- Collection: `BakeryDataEmbeddings`

You should create a vector search index named `vector_index` on the `embedding` field in the collection.

The app will insert bakery data into MongoDB automatically when it starts and the collection is empty.

## Run the Application

```bash
dotnet run
```

## How to Chat with the App

Once the app starts, it will prompt you with:

```text
Query:
```

Type your question and press Enter. For example:

- What cakes do you offer?
- Do you offer gluten-free products?
- What are your business hours?
- Do you deliver?
- What is your contact number?

To exit the app, type:

```text
quit
```

## Example Questions

- What bakery items are available?
- Do you offer custom cakes?
- What payment methods do you accept?
- What is the delivery fee?
- Are there dairy-free options?

## Notes

- The app is designed to answer only questions related to Sweet Crumbs Bakery.
- If the requested information is not available in the stored bakery context, it will respond that it does not have enough information.
