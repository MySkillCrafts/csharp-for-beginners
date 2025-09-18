# CSharpLearning.WebUIHelper

A powerful Web UI Helper for C# learning projects that provides interactive web-based presentations for educational content.

## Features

- 🎨 Beautiful, responsive web UI for educational presentations
- 📚 Support for multiple lesson types and formats
- 🚀 Easy integration with existing C# projects
- 📱 Mobile-friendly design
- 🎯 Interactive demonstrations and visualizations

## Installation

```bash
dotnet add package CSharpLearning.WebUIHelper
```

## Quick Start

```csharp
using CSharpLearning.WebUIHelper;

// Start the web server
WebUIHelper.StartServer();

// Show a lesson presentation
WebUIHelper.ShowArrayVsList(array, list, arraySize, listInitialCount, listAfterAdding, listFinalCount);
```

## Available Methods

### Lesson 1: Arrays vs Lists
```csharp
WebUIHelper.ShowArrayVsList(string[] array, List<string> list, int arraySize, int listInitialCount, int listAfterAdding, int listFinalCount)
```

### Lesson 2: Generics
```csharp
WebUIHelper.ShowGenericsDemo<T>(T[] items, string lessonTitle)
```

### Lesson 3: List Operations
```csharp
WebUIHelper.ShowListOperations(List<string> items, string operation, string result)
```

## Configuration

The helper automatically starts a web server on `http://localhost:8080` and opens your default browser.

## License

MIT License - see LICENSE file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
