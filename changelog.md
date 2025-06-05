# Changelog

Changelog template based on:
https://keepachangelog.com/en/1.1.0/

## [2.0.0] - 2025-06-05

### Added

- Launch file for different (debug) cases

### Changed

- Config - Everything config related is now part of `conf` call, config file works the same, but the commands have changed: path, file, to, nto, ignore, heed, here, away are now used instead commands: pfp, pfc, (timeout was not configurable), ignore, unignore, swd, uwd.
- Default timeout for Task Runner was removed, timeout is configurable now as conf command or as a temporary flag. Sustain works as previously.

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
