# Changelog

Changelog template based on:
https://keepachangelog.com/en/1.1.0/

## [1.1.2] - 2024-10-23

### Added

- Task Runner Flags
  - sustain - ignore timeout settings for current task execution
  - local - ignore working directory setting for current task execution

### Fixed

- Return early when missing a command and provide feedback

## [1.1.1] - 2024-10-09

### Fixed

- Overhauled error handling. Propagate exceptions and dispose processes.

## [1.1.0] - 2024-09-15

### Added

- Config - added file for storing following configuration:
  - ignore - permanently ignored folders, ie .git, bin.
  - working directory - enforces current working directory as future target, makes possible to work in a single folder and modify all siblings without `cd .. && <...>`

### Changed

- Naming convention for Upper/lowerCase functions and some indendation. Subject to future changes.

## [1.0.3] - 2024-09-10

### Fixed

- Unstable --skip- flag, which didnt skip folders when chained with long names or folders including dot (.) ie .git

## [1.0.2] - 2024-09-07

### Added

- Licence (MIT)

### Fixed

- Readme now is being read in nuget registry correctly

## [1.0.0] - 2024-09-03

### Added

- Core for-each-parallel function
