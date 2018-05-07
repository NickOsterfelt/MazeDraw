Before using OpenCVSharp for Unity copy "Plugins" and "StreamingAssets" folders to project root (Assets folder) 
so that the structure is like this:

Assets
|- Plugins
|- StreamingAssets
|- OpenCVSharpForUnity
|- ...

This is needed because Asset Store requires all asset files to be in a sigle sub-folder, 
but Plugins and SreamingAssets directories have to be in project root for them to work properly.
On newer versions of Unity only StreamingAssets folder needs to be moved.