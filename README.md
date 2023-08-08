# Introduction
_Add an introduction by explaining what the idea behind the application is_

# Project Details
| Platform | GUI | Timeframe | Database Solution |
|----------|-----|-----------|-------------------|
| Web Application  | Razor Pages | August | SQL Server |

## Dependencies
- [SBO.BlaaBog.Domain]()
- [SBO.BlaaBog.Services]()
- [SBO.BlaaBog.Web]()
    - [Bootstrap 5 (v5.3)](https://getbootstrap.com/docs/5.3/)

See the [Wiki]() for more in depth information about the project.

## Initial Features
- Students
    - Join classes
    - Change email
    - Change password
    - Add comments
    - Add pictures
    - Remove pictures
    - Add description
    - Add speciality

- Teachers
    - Create classes
    - Change email
    - Change password
    - Approve comments
    - Approve pictures
    - Approve description
    - Approve speciality
    - Set student end date

## Entity Relation Diagram
<details><summary>Click to show image(s)</summary>
<!-- ![image](**Insert-Image**) -->
Coming soon™
</details>

## Class Diagrams
<details><summary>Click to show image(s)</summary>
<!-- ![image](**Insert-Image**) -->
Coming soon™
</details>


# SBO Standards
- **Versioning**
  - Version Template: _[_Major_].[_Minor_].[_Patch_]-[StateMod]_.
  - `Major:`
    - Major Changes
    - Changes to Core structure (_Like an UI Switch_)
  - `Minor:`
    - 100 _Minor_ versions = 1 _Major_ version
    - Features
    - Large Code Refactoring (_Ex. If you create a new file when refactoring_)
  - `Patch:`
    - 100 _Patch_ versions = 1 _Minor_ version
    - Hotfixs
    - Revisions
    - Minor Code Refactoring
  - `StateMod:`
    - Can either be _Dev_ or _Rel_ and defines the state of the version. _Dev_ meaning the version is on the Development branch, while _Rel_ means it's on the Release branch and therefore published.
      > Example: v0.10.3-Dev
  - When a _Major_ version is applied it resets the version count, same goes for a _Minor_ version.
      > Example: v0.55.15-Rel -> v1.0.0-Rel | v0.99.56-Dev -> v0.99.0-Dev
- **Source Control**
    - `Features` must be branched out and developed on an isolated branch and merged back into the `Developer` branch when done.
    - `Branches` must be named as follows: *[MajorVersion]/[YouInitials]/[FeatureName]*.
      > Example: v0/MSM/ExampleBranch.
- **Code**
  - `Namespaces` must be constructed as follows: _SBO.[ProjectName].[FolderName]_.
  - `Fields` must be _private_ or _protected_.
  - `Properties` must be _public_, _protected_ or _internal_.
  - `Interfaces` must have their own subfolder, which should never be included in their `namespace`.
  - `Scopes` must have explicit enclosure.

## Change Log
_`Classes`, `structs`, `interfaces` and `enums` etc. are to be linked to the project wiki page, and versions are in descending order_
 - **[v0.0.0]()**
   - **Added**
     - _List the features added with this version_
   - **Modified**
     - _List the areas that were altered in any way in this version_
   - **Fixed**
     - _List the bugs that were fixed in this version_
