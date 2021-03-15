# Blazor.SplitPanel
Inspired by [Split-js](https://split.js.org/) 

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

## TODO for release v1.0
- Implement vertical splitpanel, should be just making sure css works.
- Setting up Azure Pipelines CI/CD
- - Set up nuget publish 
- Unit tests
- - Testing nested SplitAreas
- Create Demo

## TODO for further release
- Remove dependency on Majorsoft.Blazor.Components.Common.JsInterop
