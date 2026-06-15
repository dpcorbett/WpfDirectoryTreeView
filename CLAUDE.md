# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a reusable WPF UserControl library (`OutputType=library`) targeting .NET 4.5. It exposes a `DirectoryTreeViewControl` that consumers embed in their WPF windows to display a recursive directory/file tree.

## Build

Open `WpfDirectoryTreeView.sln` in Visual Studio, or build from the command line:

```
msbuild WpfDirectoryTreeView.sln /p:Configuration=Debug
msbuild WpfDirectoryTreeView.sln /p:Configuration=Release
```

Output DLLs land in `bin\Debug\` or `bin\Release\`. There are no tests.

## Architecture

The control follows a three-layer structure:

**Model** (`Model/`)
- `Item` — base class with `Name` and `Path` string properties
- `DirectoryItem : Item` — adds a `List<Item> Items` child collection (used by `HierarchicalDataTemplate` for tree nesting)
- `FileItem : Item` — leaf node; no additional members

**DataAccess** (`DataAccess/ItemProvider.cs`)
- `ItemProvider` implements `INotifyPropertyChanged`
- Setting `strPath` fires `PropertyChanged`, which the control listens to for refresh
- `GetItems(string path)` recursively walks the filesystem via `DirectoryInfo`, building a `List<Item>` of mixed `DirectoryItem`/`FileItem` nodes; silently skips inaccessible directories

**View** (`DirectoryTreeView.xaml` / `.xaml.cs`)
- `DirectoryTreeViewControl` is the public WPF `UserControl`
- Its constructor wires `_itemProvider.PropertyChanged` → `populateTreeView`, which sets `DataContext` to the returned item list
- The XAML uses `HierarchicalDataTemplate` for `DirectoryItem` (binds `ItemsSource` to `Items`) and `DataTemplate` for `FileItem`; both show `Name` as text and `Path` as tooltip

**Important design note:** `OnPropertyChanged` is called with the string literal `"strPath"` rather than the actual path value, so `populateTreeView` receives `e.PropertyName == "strPath"` and passes that string to `GetItems`. Callers that need to trigger a tree refresh should call `populateTreeView` directly with the desired path, or set `strPath` only after accounting for this behaviour.
