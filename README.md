MergeSolutionSourceControl
==========================

This tool analyzes source control bindings in visual studio solution files and merges them.

We have 2 solutions and hundreds of projects.

I migrated from VS 2008 SP1 to VS 2010 SP1 and was also receiving the error:

There appears to be a discrepancy between the solution's source control information . . .

I would open solution1, allow it to update the projects, then open solution2, only to get this error again.

I analyzed the solution files and found the following:

Root Cause:

solution1.sln and solution2.sln files are inconsistent with each other in regards to the project source control bindings.

Example:

solution1.sln

SccProjectUniqueName6 = Project1\Project1.csproj 
SccProjectName6 = \u0022$/Project1\u0022,\u0020HSBAAAAA 
SccLocalPath6 = Project1

solution2.sln

SccProjectUniqueName6 = Project1\Project1.csproj 
SccLocalPath6 = . 
SccProjectFilePathRelativizedFromConnection6 = Project1\

Solution:

I fixed this issue by manually modifying the solution files to be consistent in notepad. I copy and pasted the source control info from solution1.sln into solution2.sln for projects they had in common.

I eventually wrote a small utility to automate this.
