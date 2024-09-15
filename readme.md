# Async Task Helper

The whole idea is to run tasks in multiple folders concurrently - hence the name.

## "install from repo"

If this was cloned from github

`$ dotnet pack ath.generated.sln; dotnet tool install --global --add-source ./nupkg ath`

then the cli will be available in terminal, run `$ ath help` for starters.

otherwise it should be easily available from nuget

## uninstall

`$ dotnet tool uninstall ath -g`

## Roadmap (...and whatnot)

Would love to add some testing at some point, and tweak exiting processes.
