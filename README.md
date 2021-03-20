# Blazor.SplitPanel
Inspired by [Split-js](https://split.js.org/)

[![Build](https://github.com/bigboystoffe/Blazor.SplitPanel/actions/workflows/dotnet.yml/badge.svg)](https://github.com/bigboystoffe/Blazor.SplitPanel/actions/workflows/dotnet.yml)
[![NuGet version](https://badge.fury.io/nu/Blazor.SplitPanel.svg)](https://badge.fury.io/nu/Blazor.SplitPanel)

## Installation
### In Program.cs
Add following:

````
builder.Services
  .AddSplitPanelJS()
  .AddJsInteropExtensions();
````

Add following using in _Imports.razor
````
@using Blazor.SplitPanel
````

## Usage 
```HTML
<SplitArea GutterSize="8" Direction="SplitDirection.Vertical">
  <SplitPane>
    ...
  </SplitPane>
  <SplitPane>
    ...
  </SplitPane>
</SplitArea>
```

## TODO for further release
- Remove dependency on Majorsoft.Blazor.Components.Common.JsInterop
