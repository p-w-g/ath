# Async Task Helper

The `ath` CLI currently consists of two main components: a task helper and a configuration file. These components allow you to run commands across multiple folders and customize how the tool behaves in your environment.

## Quickstart

### Install via NuGet

The latest stable release is available as a package on [NuGet][nuget]

To install globally, just run the following:

```sh
dotnet tool install --global ath
```

### Install via GitHub

If you've cloned the project directly from the [GitHub repo][github], follow these steps in the project folder

```sh
dotnet pack ath.generated.sln
dotnet tool install --global --add-source ./nupkg ath
```

This will build the tool locally and install it from the generated package.

### Sanity check

To confirm that everything is working properly, run:

```sh
ath help
```

If you see the help menu, you’re good to go!

### Usage

```sh
ath fep <<command>> [--skip-foo-bar-baz || --only-gris-gras-gräs]
```

## Full documentation

https://ath-docs.netlify.app/

## Uninstall

If you need to uninstall `ath` for any reason, you can do so with:

```sh
dotnet tool uninstall ath -g
```

This will remove the global installation of ath.

[dotnet]: https://dotnet.microsoft.com/en-us/download
[nuget]: https://www.nuget.org/packages/ath/
[github]: https://github.com/p-w-g/ath
