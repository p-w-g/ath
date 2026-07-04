# Changelog

Changelog template based on:
https://keepachangelog.com/en/1.1.0/

## [3.0.0] - 2026-07-04

### Added

- Automated test suite (xUnit), covering the argument parser, folder
  filtering, config parsing, and task runner.

### Changed

- **Breaking:** retargeted from .NET 8 to .NET 10 (LTS). .NET 8 reaches
  end of support 2026-11-10; .NET 10 is supported through 2028. Running
  `ath` now requires the .NET 10 runtime (or later, see below).
- Enabled `RollForward=LatestMajor`, so the tool keeps running on future
  major .NET runtimes (net11, net12, ...) without needing to be
  recompiled or republished.
- **Breaking:** `--skip-`/`--only-` now separate multiple folder names
  with a comma instead of a dash (`--skip-foo,bar,baz` instead of
  `--skip-foo-bar-baz`), so a folder name that itself contains a hyphen
  (e.g. `my-project`) can be targeted correctly. Existing scripts using
  the old dash-separated multi-value syntax will behave differently.
- **Breaking:** `ath fep` now exits with a non-zero status if any
  folder's command failed, instead of always exiting 0. Scripts/CI
  relying on `ath`'s exit code should account for this.
- Repeating the same flag (e.g. `--skip-foo --skip-bar`) now merges the
  values instead of crashing.

### Fixed

- `ath cfg`, `ath fep` (with no command), and `ath cfg to` (with no
  duration) no longer crash with an unhandled exception; they print a
  usage message instead.
- `--skip`/`--only`/ignored-folders now match the exact folder name
  instead of a substring of the full path, which previously could
  silently include or exclude unrelated folders depending on where the
  project happened to live on disk.
- A corrupted or invalid `~/.athconfig` file no longer breaks every
  command; it now falls back to default settings with a warning.
- Arguments containing spaces (e.g. a quoted commit message) no longer
  lose their grouping when passed through to the shell.
- `--timeout` now kills the entire process tree, not just the immediate
  shell, so timed-out commands don't leave orphaned child processes
  running in the background.

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
