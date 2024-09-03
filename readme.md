
## rebuild, install
dotnet pack .\ath.generated.sln; dotnet tool install --global --add-source ./nupkg ath
