# Bifrost
CBUS Op-Code definitions and T4 template based code generation.

A .NET Framework library to help with the T4 template generation of C#.NET code for CBUS (https://cbus-traincontrol.com/)

It is a Framework library because T4 templates don't appear to understand anything later.

Multiple data files can be included as embedded resources. They contain the data that describes the CBUS OpCodes together with history, licence, general comments, and supporting data.

The library exposes a Loader class and a Builder class that respectively load the data from the selected data file and build the objects from the loaded data.

There is also a console app that can be used to test and experiment with the objects exposed by the Loader and Builder classes.

Bifrost is intended to be consumed as a NuGet package (in progress), but it can simply be downloaded, compiled and the dll referenced as an assembly in the T4 templates.
